using AutoMapper;
using Core.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vlpi.Web.ViewModels.UtilViewModels;

namespace Vlpi.Web.Controllers
{
    [ApiController]
    [Route("api/util")]
    [Produces("application/json")]
    public class UtilsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IExecutionModeManager _executionModeManager;
        private readonly IRequirementManager _requirementManager;

        public UtilsController(
            IMapper mapper,
            IExecutionModeManager executionModeManager,
            IRequirementManager requirementManager
            )
        {
            _mapper = mapper;
            _executionModeManager = executionModeManager;
            _requirementManager = requirementManager;
        }

        [HttpGet]
        [Route("execution-modes")]
        public async Task<IActionResult> GetExecutionModesAsync()
        {
            var executionModeModels = await _executionModeManager.GetExecutionModesAsync();
            var executionModeViewModels = _mapper.Map<IEnumerable<ExecutionModeViewModel>>(executionModeModels);
            return Ok(executionModeViewModels);
        }

        [HttpGet]
        [Route("requirement-types")]
        public async Task<IActionResult> GetRequirementTypesAsync()
        {
            var requirementTypeModels = await _requirementManager.GetRequirementTypesAsync();
            var requirementTypeViewModels = _mapper.Map<IEnumerable<RequirementTypeViewModel>>(requirementTypeModels);
            return Ok(requirementTypeViewModels);
        }
    }
}