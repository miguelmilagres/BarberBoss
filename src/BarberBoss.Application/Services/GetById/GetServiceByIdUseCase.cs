using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.Services.GetById
{
    public class GetServiceByIdUseCase : IGetServiceByIdUseCase
    {
        private readonly IServicesRepository _repository;
        private readonly IMapper _mapper;

        public GetServiceByIdUseCase(IServicesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ResponseServiceJson> Execute(long id)
        {
            var result = await _repository.GetById(id);

            if (result is null)
                throw new NotFoundException(ResourceErrorMessages.SERVICE_NOT_FOUND);

            return _mapper.Map<ResponseServiceJson>(result);
        }
    }
}
