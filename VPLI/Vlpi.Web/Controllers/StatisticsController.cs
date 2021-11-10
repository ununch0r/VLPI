using AutoMapper;
using Core.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public StatisticsController(
            IMapper mapper, IStatisticManager statisticManager)
        {
            _mapper = mapper;
            _statisticManager = statisticManager;
        }

        [HttpGet]
        [Route("task/{id}")]
        public async Task<IActionResult> GetStatisticByTaskAsync(int id)
        {
            var taskStatistic = await _statisticManager.GetStatisticByTaskAsync(id);
            var taskStatisticViewModel = _mapper.Map<TaskStatisticViewModel>(taskStatistic);
            return Ok(taskStatisticViewModel);
        }

        [HttpGet]
        [Route("task")]
        public async Task<IActionResult> GetStatisticByModuleAsync()
        {
            var tasksStatistic = await _statisticManager.GetStatisticByModuleAsync();
            var taskStatisticViewModels = _mapper.Map<ICollection<TaskStatisticViewModel>>(tasksStatistic);
            return Ok(taskStatisticViewModels);
        }
    }
}