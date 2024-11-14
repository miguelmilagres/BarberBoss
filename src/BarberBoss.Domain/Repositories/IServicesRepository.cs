using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories
{
    public interface IServicesRepository
    {
        Task Add(Service service);
        Task<List<Service>> GetAll();
    }
}
