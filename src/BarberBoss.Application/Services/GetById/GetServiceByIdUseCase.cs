using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;

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

            return _mapper.Map<ResponseServiceJson>(result);
        }
    }
}
