using BarberBoss.Domain.Entities;

namespace BarberBoss.Domain.Repositories
{
    public interface IServicesReadOnlyRepository
    {
        Task<List<Service>> GetAll();
        Task<Service?> GetById(long id);
        Task<List<Service>> FilterByMonth(DateOnly date);
    }
}
