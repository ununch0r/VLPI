﻿using AutoMapper;
using Core.Managers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Vlpi.Web.ViewModels.TaskViewModels;
using Task = Core.Entities.Task;

namespace Vlpi.Web.Controllers
{
    [ApiController]
    [Route("api/task")]
    [Produces("application/json")]
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
        public async Task<IActionResult> GetAsync(int id)
        {
            var taskEntity = await _taskManager.GetAsync(id);
            var taskViewModel = _mapper.Map<TaskViewModel>(taskEntity);
            return Ok(taskViewModel);
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllAsync()
        {
            var tasks = await _taskManager.ListAsync();
            var taskViewModels = _mapper.Map<IEnumerable<TaskViewModel>>(tasks);
            return Ok(taskViewModels);
        }


        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateTaskAsync([FromBody] [Required] CreateTaskViewModel createTaskViewModel)
        {
            var taskEntity = _mapper.Map<Task>(createTaskViewModel);
            await _taskManager.AddAsync(taskEntity);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _taskManager.DeleteAsync(id);
            return NoContent();
        }
    }
}
