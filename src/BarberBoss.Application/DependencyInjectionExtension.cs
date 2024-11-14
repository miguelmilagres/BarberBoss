using BarberBoss.Application.Services.Register;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection service)
        {
            service.AddScoped<IRegisterServiceUseCase, RegisterServiceUseCase>();
        }
    }
}
