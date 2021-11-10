using Business.Managers;
using Core.Managers;
using Core.Repositories;
using DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Vlpi.Web.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            //repositories
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAnswerRepository, AnswerRepository>();
            services.AddTransient<IExecutionModeRepository, ExecutionModeRepository>();
            services.AddTransient<IRequirementRepository, RequirementRepository>();


            //managers
            services.AddTransient<ITaskManager, TaskManager>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<IAnswerManager, AnswerManager>();
            services.AddTransient<IExecutionModeManager, IExecutionModeManager>();
            services.AddTransient<IRequirementManager, RequirementManager>();
            services.AddTransient<IStatisticManager, StatisticManager>();

        }
    }
}
