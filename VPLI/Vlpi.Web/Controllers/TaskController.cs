using System.Threading.Tasks;
using AutoMapper;
using Core.Managers;
using Microsoft.AspNetCore.Mvc;
using Vlpi.Web.ViewModels;

namespace Vlpi.Web.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskManager _taskManager;
        private readonly IMapper _mapper;

        public TaskController(ITaskManager taskManager, IMapper mapper)
        {
            _taskManager = taskManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var taskEntity = await _taskManager.GetAsync(id);
            var taskViewModel = _mapper.Map<TaskViewModel>(taskEntity);
            return Ok(taskViewModel);
        }
    }
}
