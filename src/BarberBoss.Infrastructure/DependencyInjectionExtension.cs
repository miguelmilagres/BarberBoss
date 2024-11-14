using BarberBoss.Domain.Repositories;
using BarberBoss.Infrastructure.DataAccess;
using BarberBoss.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IServicesReadOnlyRepository, ServiceRepository>();
            services.AddScoped<IServicesWriteOnlyRepository, ServiceRepository>();
            services.AddScoped<IServicesUpdateOnlyRepository, ServiceRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Connection");

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 39));

            services.AddDbContext<BarberBossDbContext>(config => config.UseMySql(connectionString, serverVersion));
        }
    }
}
