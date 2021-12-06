using AutoMapper;
using Core.Managers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Vlpi.Web.ViewModels.StatisticViewModels;

namespace Vlpi.Web.Controllers
{
    [ApiController]
    [Route("api/statistics")]
    [Produces("application/json")]
    public class StatisticsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IStatisticManager _statisticManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StatisticsController(IMapper mapper, IStatisticManager statisticManager, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _statisticManager = statisticManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        [Route("task/{id}")]
        public async Task<IActionResult> GetStatisticByTaskAsync(int id)
        {
            var taskStatistic = await _statisticManager.GetStatisticByTaskAsync(id);
            if (taskStatistic == null)
            {
                return BadRequest("data is incorrect");
            }
            var taskStatisticViewModel = _mapper.Map<TaskStatisticViewModel>(taskStatistic);
            return Ok(taskStatisticViewModel);
        }

        [HttpGet]
        [Route("task")]
        public async Task<IActionResult> GetStatisticByModuleAsync()
        {
            var tasksStatistic = await _statisticManager.GetStatisticByModuleAsync();
            return Ok(tasksStatistic);
        }

        [HttpGet]
        [Route("admin/user/{id}")]
        public async Task<IActionResult> GetGenericUserStatisticForAdminAsync(int id)
        {
            var userStatistic = await _statisticManager.GetGenericUserStatisticAsync(id);
            var userStatisticViewModel = _mapper.Map<GenericUserStatisticViewModel>(userStatistic);
            return Ok(userStatisticViewModel);
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetUserStatisticAsync()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userStatisticAsync = await _statisticManager.GetUserStatisticAsync(userId);
            var userTaskStatisticViewModels = _mapper.Map<ICollection<UserTaskStatisticViewModel>>(userStatisticAsync);
            return Ok(userTaskStatisticViewModels);
        }

        [HttpGet]
        [Route("user/generic")]
        public async Task<IActionResult> GetGenericUserStatistic()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userStatistic = await _statisticManager.GetGenericUserStatisticAsync(userId);
            var userStatisticViewModel = _mapper.Map<GenericUserStatisticViewModel>(userStatistic);
            return Ok(userStatisticViewModel);
        }

        [HttpGet]
        [Route("user/generic/{id}")]
        public async Task<IActionResult> GetGenericUserStatisticById(int id)
        {
            var userStatistic = await _statisticManager.GetGenericUserStatisticAsync(id);
            var userStatisticViewModel = _mapper.Map<GenericUserStatisticViewModel>(userStatistic);
            return Ok(userStatisticViewModel);
        }
    }
}