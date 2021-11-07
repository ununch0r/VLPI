using AutoMapper;
using Core.Entities;
using Vlpi.Web.ViewModels.TaskViewModels;
using Vlpi.Web.ViewModels.UserViewModels;
using Vlpi.Web.ViewModels.UtilViewModels;

namespace Vlpi.Web.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Requirement, RequirementViewModel>();
            CreateMap<TaskTip, TaskTipViewModel>();
            CreateMap<Task, TaskViewModel>();
            CreateMap<ExecutionMode, ExecutionModeViewModel>();
            CreateMap<RequirementType, RequirementTypeViewModel>();

            CreateMap<CreateRequirementViewModel, Requirement>();
            CreateMap<CreateTaskTipViewModel, TaskTip>();
            CreateMap<CreateTaskViewModel, Task>();

            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
        }
    }
}
