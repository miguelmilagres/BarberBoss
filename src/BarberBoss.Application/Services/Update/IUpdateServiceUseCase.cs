using BarberBoss.Communication.Requests;

namespace BarberBoss.Application.Services.Update
{
    public interface IUpdateServiceUseCase
    {
        Task Execute(long id, RequestServiceJson request);
    }
}
