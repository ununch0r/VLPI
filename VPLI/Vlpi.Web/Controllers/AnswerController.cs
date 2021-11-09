using AutoMapper;
using Core.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vlpi.Web.ViewModels.AnswerViewModels;
using Vlpi.Web.ViewModels.UtilViewModels;

namespace Vlpi.Web.Controllers
{
    [ApiController]
    [Route("api/answer")]
    [Produces("application/json")]
    public class AnswerController : ControllerBase
    {
        private readonly IMapper _mapper;

        public AnswerController(
            IMapper mapper
            )
        {
            _mapper = mapper;
        }

        [HttpPost]
        [Route("writing")]
        public async Task<IActionResult> VerifyWritingAnswerAsync([FromBody] WritingAnswerViewModel writingAnswer)
        {
            return Ok();
        }

        [HttpPost]
        [Route("analysis")]
        public async Task<IActionResult> VerifyAnalysisAnswerAsync([FromBody] AnalysisAnswerViewModel analysisAnswer)
        {
            return Ok();
        }
    }
}