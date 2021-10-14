using System.Threading.Tasks;
using Core.Managers;
using Microsoft.AspNetCore.Mvc;

namespace Vlpi.Web.Controllers
{
    [ApiController]
    [Route("api/task")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskManager _taskManager;

        public TaskController(ITaskManager taskManager)
        {
            _taskManager = taskManager;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var taskEntity = await _taskManager.GetAsync(id);//add mapper
            return Ok(taskEntity);
        }
    }
}
