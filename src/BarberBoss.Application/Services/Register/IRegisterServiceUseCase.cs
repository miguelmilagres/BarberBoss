using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.Services.Register
{
    public interface IRegisterServiceUseCase
    {
        public ResponseRegisteredServiceJson Execute(RequestRegisterServiceJson request);
    }
}
