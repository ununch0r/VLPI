using System;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Entities.Custom.Answer;
using Core.Managers;
using Microsoft.AspNetCore.Http;
using Vlpi.Web.ViewModels.AnswerResultViewModels;
using Vlpi.Web.ViewModels.AnswerViewModels;

namespace Vlpi.Web.Controllers
{
    [ApiController]
    [Route("api/answer")]
    [Produces("application/json")]
    public class AnswerController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IAnswerManager _answerManager;

        public AnswerController(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IAnswerManager answerManager
        )
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _answerManager = answerManager;
        }

        [HttpPost]
        [Route("writing")]
        public async Task<IActionResult> VerifyWritingAnswerAsync(
            [FromBody] WritingAnswerViewModel writingAnswerViewModel)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var writingAnswerModel = _mapper.Map<WritingAnswer>(writingAnswerViewModel);
            var result = await _answerManager.VerifyWritingAnswerAsync(userId, writingAnswerModel);
            var resultViewModel = _mapper.Map<WritingTaskResultViewModel>(result);
            return Ok(resultViewModel);
        }

        [HttpPost]
        [Route("analysis")]
        public async Task<IActionResult> VerifyAnalysisAnswerAsync(
            [FromBody] AnalysisAnswerViewModel analysisAnswerViewModel)
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var analysisAnswerModel = _mapper.Map<AnalysisAnswer>(analysisAnswerViewModel);

            var result = await _answerManager.VerifyAnalysisAnswerAsync(userId, analysisAnswerModel);

            var resultViewModel = _mapper.Map<AnalysisTaskResultViewModel>(result);
            return Ok(resultViewModel);
        }
    }
}