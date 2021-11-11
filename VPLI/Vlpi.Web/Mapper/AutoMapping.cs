using AutoMapper;
using Core.Entities;
using Core.Entities.Custom.Answer;
using Core.Entities.Custom.AnswerResult;
using Core.Entities.Custom.Statistic;
using Vlpi.Web.ViewModels.AnswerResultViewModels;
using Vlpi.Web.ViewModels.AnswerViewModels;
using Vlpi.Web.ViewModels.StatisticViewModels;
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
            CreateMap<User, UserViewModel>();
            CreateMap<AnalysisTaskResult, AnalysisTaskResultViewModel>();
            CreateMap<WritingTaskResult, WritingTaskResultViewModel>();
            CreateMap<WrittenRequirementTemplateAnswer, WrittenRequirementTemplateAnswerViewModel>();
            CreateMap<TaskStatistic, TaskStatisticViewModel>();
            CreateMap<GenericUserStatistic, GenericUserStatisticViewModel>();
            CreateMap<UserStatistic, UserStatisticViewModel>();
            CreateMap<UserTaskStatistic, UserTaskStatisticViewModel>();


            CreateMap<CreateRequirementViewModel, Requirement>();
            CreateMap<CreateTaskTipViewModel, TaskTip>();
            CreateMap<CreateTaskViewModel, Task>();
            CreateMap<AnalysisAnswerViewModel, AnalysisAnswer>();
            CreateMap<AnswerViewModel, Answer>();
            CreateMap<ModifiedRequirementViewModel, ModifiedRequirement>();
            CreateMap<WritingAnswerViewModel, WritingAnswer>();
            CreateMap<WritingRequirementViewModel, WritingRequirement>();
            CreateMap<UserViewModel, User>();
        }
    }
}
