using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Entities;
using Core.Entities.Custom.Answer;
using Core.Entities.Custom.AnswerResult;
using Core.Entities.Custom.Statistic;
using Core.Entities.Custom.Task;
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
            CreateMap<TaskType, TaskTypeViewModel>();
            CreateMap<Task, TaskCustomModel>().ForMember(dest => dest.Explanation,
                opt => opt.MapFrom(src => src.Requirement.Select(req => req.Explanation)));
            CreateMap<TaskCustomModel, TaskViewModel>();
            CreateMap<Explanation, ExplanationViewModel>();


            CreateMap<CreateRequirementViewModel, Requirement>().ForMember(dest => dest.Explanation,
                opt => opt.MapFrom(src => src.Explanation == null?null:new Explanation{Content = src.Explanation}));
            CreateMap<CreateTaskTipViewModel, TaskTip>();
            CreateMap<CreateTaskViewModel, Task>();
            CreateMap<AnalysisAnswerViewModel, AnalysisAnswer>();
            CreateMap<AnswerViewModel, Answer>();
            CreateMap<WrongRequirementViewModel, WrongRequirement>();
            CreateMap<WritingAnswerViewModel, WritingAnswer>();
            CreateMap<WritingRequirementViewModel, WritingRequirement>();
            CreateMap<UserViewModel, User>();
            CreateMap<CreateAnalysisTaskViewModel, CreateAnalysisTaskModel>();
            CreateMap<CreateAnalysisTaskModel, Task>();
            CreateMap<CreateUserViewModel, User>();
        }
    }
}
