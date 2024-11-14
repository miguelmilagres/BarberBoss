using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.Services.Register
{
    public interface IRegisterServiceUseCase
    {
        Task<ResponseRegisteredServiceJson> Execute(RequestServiceJson request);
    }
}
