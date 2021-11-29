using System;
using AutoMapper;
using Core.Entities;
using Core.Entities.Custom.AnswerTemplates;
using Core.Entities.Custom.Task;
using Core.Managers;
using Core.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Custom.Answer;

namespace Business.Managers
{
    public class TaskManager : ITaskManager
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IRequirementManager _requirementManager;
        private readonly IMapper _mapper;

        public TaskManager(ITaskRepository taskRepository, IMapper mapper, IRequirementManager requirementManager)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _requirementManager = requirementManager;
        }

        public async System.Threading.Tasks.Task AddAsync(Task task)
        {
            await _taskRepository.AddAsync(task);
        }

        public async System.Threading.Tasks.Task AddAnalysisAsync(CreateAnalysisTaskModel task)
        {

            var taskModel = _mapper.Map<Task>(task);
            var addedTask = await _taskRepository.AddAsync(taskModel);

            foreach (var correctRequirement in task.CorrectRequirements)
            {
                correctRequirement.TaskId = addedTask.Id;
            }

            var correctRequirements = await _requirementManager.AddBulkAsync(task.CorrectRequirements);

            foreach (var wrongRequirement in task.WrongRequirements)
            {
                wrongRequirement.TaskId = addedTask.Id;
            }

            var wrongRequirements = await _requirementManager.AddBulkAsync(task.WrongRequirements);

            var standardAnswer = new RequirementsAnalysisTaskTemplateAnswer
            {
                CorrectRequirementIds = correctRequirements.Select(requirement => requirement.Id),
                WrongRequirements = wrongRequirements.Select(requirement => new WrongRequirement
                {
                    RequirementId = requirement.Id,
                    ExplanationId = requirement.ExplanationId.Value
                })
            };

            var serializedStandardAnswer = JsonConvert.SerializeObject(standardAnswer);
            await _taskRepository.AddStandardAnswerAsync(addedTask.Id, serializedStandardAnswer);
        }

        public async System.Threading.Tasks.Task UpdateAsync(int taskId, Task task)
        {
            foreach (var requirement in task.Requirement)
            {
                requirement.TaskId = taskId;
            }

            foreach (var taskTip in task.TaskTip)
            {
                taskTip.TaskId = taskId;
            }

            await _taskRepository.UpdateAsync(taskId, task);
        }

        public async System.Threading.Tasks.Task<Task> GetAsync(int id)
        {
            return await _taskRepository.GetAsync(id);
        }

        public async System.Threading.Tasks.Task<AnalysisTask> GetAnalysisTaskAsync(int id)
        {
            var task = await _taskRepository.GetAsync(id);
            return _mapper.Map<AnalysisTask>(task);
        }

        public async System.Threading.Tasks.Task<IList<TaskCustomModel>> ListAsync()
        {
            var tasks = await _taskRepository.ListAsync();

            var customTasks = _mapper.Map<IList<TaskCustomModel>>(tasks).OrderBy(task => task.Complexity).ToList();
            var order = 1;

            foreach (var taskViewModel in customTasks)
            {
                taskViewModel.Explanation = taskViewModel.Explanation.Where(e => e != null).ToList();
                taskViewModel.Order = order;
                order++;
            }

            return customTasks;
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }
    }
}
