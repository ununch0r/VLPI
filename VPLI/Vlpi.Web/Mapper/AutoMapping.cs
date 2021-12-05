using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Core.Entities;
using Core.Entities.Custom.Answer;
using Core.Entities.Custom.AnswerResult;
using Core.Entities.Custom.AnswerTemplates;
using Core.Entities.Custom.Statistic;
using Core.Entities.Custom.Task;
using Newtonsoft.Json;
using Vlpi.Web.ViewModels.AnswerResultViewModels;
using Vlpi.Web.ViewModels.AnswerViewModels;
using Vlpi.Web.ViewModels.StatisticViewModels;
using Vlpi.Web.ViewModels.TaskViewModels;
using Vlpi.Web.ViewModels.UserViewModels;
using Vlpi.Web.ViewModels.UtilViewModels;
using WrittenRequirementTemplateAnswer = Core.Entities.Custom.AnswerResult.WrittenRequirementTemplateAnswer;

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
            CreateMap<User, UserViewModel>().ForMember(dest => dest.Roles,
                opt => opt.MapFrom(src => src.UserRole.Select(ur => ur.Role.Name)));
            CreateMap<AnalysisTaskResult, AnalysisTaskResultViewModel>();
            CreateMap<WritingTaskResult, WritingTaskResultViewModel>();
            CreateMap<WrittenRequirementTemplateAnswer, WrittenRequirementTemplateAnswerViewModel>();
            CreateMap<TaskStatistic, TaskStatisticViewModel>();
            CreateMap<GenericUserStatistic, GenericUserStatisticViewModel>();
            CreateMap<UserStatistic, UserStatisticViewModel>();
            CreateMap<UserTaskStatistic, UserTaskStatisticViewModel>();
            CreateMap<WrongRequirementDisplay, WrongRequirementDisplayViewModel>();
            CreateMap<TaskType, TaskTypeViewModel>();
            CreateMap<Task, TaskCustomModel>().ForMember(dest => dest.Explanation,
                opt => opt.MapFrom(src => src.Requirement.Select(req => req.Explanation)));
            CreateMap<TaskCustomModel, TaskViewModel>();
            CreateMap<Explanation, ExplanationViewModel>();
            CreateMap<Task, AnalysisTask>().ForMember(dest => dest.StandardAnswer,
                opt => opt.MapFrom(src => JsonConvert.DeserializeObject<RequirementsAnalysisTaskTemplateAnswer>(src.StandardAnswer)));
            CreateMap<Task, WritingTask>().ForMember(dest => dest.StandardAnswer,
                opt => opt.MapFrom(src => JsonConvert.DeserializeObject<WritingRequirementsTaskTemplateAnswer>(src.StandardAnswer)));


            CreateMap<CreateRequirementViewModel, Requirement>()
                .ForMember(dest => dest.Explanation,
                    opt => opt.MapFrom(src => src.Explanation == null ? null : new Explanation { Content = src.Explanation }))
                .ForMember(dest => dest.Continuation,
                    opt => opt.MapFrom(src => src.Continuation == null ? null : new Continuation { Content = src.Continuation }))
                .ForMember(dest => dest.TypeId,
                    opt => opt.MapFrom(src => src.TypeId));
            CreateMap<CreateTaskTipViewModel, TaskTip>();
            CreateMap<CreateTaskViewModel, Task>();
            CreateMap<UpdateTaskViewModel, Task>();
            CreateMap<AnalysisAnswerViewModel, AnalysisAnswer>();
            CreateMap<AnswerViewModel, Answer>();
            CreateMap<WrongRequirementViewModel, WrongRequirement>();
            CreateMap<WritingAnswerViewModel, WritingAnswer>();
            CreateMap<WritingRequirementViewModel, WritingRequirement>();
            CreateMap<UserViewModel, User>();
            CreateMap<CreateAnalysisTaskViewModel, CreateAnalysisTaskModel>();
            CreateMap<CreateWritingTaskViewModel, CreateWritingTaskModel>();
            CreateMap<CreateAnalysisTaskModel, Task>();
            CreateMap<CreateWritingTaskModel, Task>();
            CreateMap<CreateUserViewModel, User>();
            CreateMap<ModuleStatistic, ModuleStatisticsViewModel>();
            CreateMap<ModuleStatisticsViewModel, ModuleStatistic>();
        }
    }
}
