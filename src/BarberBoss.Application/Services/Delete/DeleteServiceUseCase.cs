using AutoMapper;
using BarberBoss.Domain.Repositories;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.Services.Delete
{
    public class DeleteServiceUseCase : IDeleteServiceUseCase
    {
        private readonly IServicesWriteOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteServiceUseCase(IServicesWriteOnlyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task Execute(long id)
        {
            var result = await _repository.Delete(id);

            if (!result)
                throw new NotFoundException(ResourceErrorMessages.SERVICE_NOT_FOUND);

            await _unitOfWork.Commit();
        }
    }
}
