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

            //managers
            services.AddTransient<ITaskManager, TaskManager>();
        }
    }
}
