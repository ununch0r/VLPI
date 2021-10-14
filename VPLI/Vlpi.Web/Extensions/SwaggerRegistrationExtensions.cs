using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Vlpi.Web.Extensions
{
    public static class SwaggerRegistrationExtensions
    {
        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "VLPI API",
                    Version = "v1",
                });
            });
		}
    }
}
