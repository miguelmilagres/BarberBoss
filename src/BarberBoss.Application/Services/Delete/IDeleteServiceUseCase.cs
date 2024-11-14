namespace BarberBoss.Application.Services.Delete
{
    public interface IDeleteServiceUseCase
    {
        Task Execute(long id);
    }
}
