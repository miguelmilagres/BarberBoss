using BarberBoss.Domain.Repositories;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IServicesRepository, ServiceRepository>();
        }
    }
}
