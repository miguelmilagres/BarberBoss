using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.Services.GetAll
{
    public interface IGetAllServiceUseCase
    {
        Task<ResponseServicesJson> Execute();
    }
}
