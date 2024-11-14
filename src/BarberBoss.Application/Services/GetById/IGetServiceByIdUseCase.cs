using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.Services.GetById
{
    public interface IGetServiceByIdUseCase
    {
        Task<ResponseServiceJson> Execute(long id);
    }
}
