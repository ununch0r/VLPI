using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Vlpi.Web.Models;
using Vlpi.Web.ViewModels.UserViewModels;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Vlpi.Web.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOptions<AuthOptions> _authOptions;
        private readonly IUserManager _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(
            IMapper mapper,
            IOptions<AuthOptions> authOptions,
            IHttpContextAccessor httpContextAccessor,
            IUserManager userManager
            )
        {
            _mapper = mapper;
            _authOptions = authOptions;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
        {
            var user = await AuthenticateUser(request.Email, request.Password);

            if (user != null)
            {
                var token = GenerateJwt(user);
                return Ok(new { access_token = token });
            }

            return Unauthorized();
        }

        [Route("user")]
        [HttpGet]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var user = await _userManager.GetAsync(userId);
            var userViewModel = _mapper.Map<UserViewModel>(user);
            return Ok(userViewModel);
        }

        [Route("user/{id}/roles")]
        [HttpPost]
        public async Task<IActionResult> AddUserRoles(int userId, [FromBody] string[] roles)
        {
            await _userManager.AddUserRolesAsync(userId, roles);
            return Ok();
        }

        [Route("user/{id}/roles")]
        [HttpDelete]
        public async Task<IActionResult> RemoveUserRoles(int userId, [FromBody] string[] roles)
        {
            await _userManager.RemoveUserRolesAsync(userId, roles);
            return Ok();
        }

        [Route("register")]
        [HttpPost]
        public async Task<IActionResult> RegisterUserAsync([FromBody] CreateUserViewModel userViewModel)
        {
            var user = _mapper.Map<User>(userViewModel);
            await _userManager.AddAsync(user);

            var createdUser = await AuthenticateUser(userViewModel.Email, userViewModel.Password);

            if (user != null)
            {
                var token = GenerateJwt(createdUser);
                return Ok(new { access_token = token });
            }

            return Unauthorized();
        }

        private async Task<UserViewModel> AuthenticateUser(string email, string password)
        {
            var user = await _userManager.AuthenticateUserAsync(email, password);
            var userViewModel = _mapper.Map<UserViewModel>(user);
            return userViewModel;
        }

        private string GenerateJwt(UserViewModel user)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
                    new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString())
                };

            if (user.Roles != null)
            {
                claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }

            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifetime),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
