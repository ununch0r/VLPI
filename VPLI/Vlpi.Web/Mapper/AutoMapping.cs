using AutoMapper;
using Core.Entities;
using Vlpi.Web.ViewModels;
using Vlpi.Web.ViewModels.TaskViewModels;

namespace Vlpi.Web.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Requirement, RequirementViewModel>();
            CreateMap<TaskTip, TaskTipViewModel>();
            CreateMap<Task, TaskViewModel>();

            CreateMap<CreateRequirementViewModel, Requirement>();
            CreateMap<CreateTaskTipViewModel, TaskTip>();
            CreateMap<CreateTaskViewModel, Task>();
        }
    }
}
