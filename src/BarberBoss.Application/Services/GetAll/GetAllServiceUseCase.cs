using AutoMapper;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Repositories;

namespace BarberBoss.Application.Services.GetAll
{
    public class GetAllServiceUseCase : IGetAllServiceUseCase
    {
        private readonly IServicesReadOnlyRepository _repository;
        private readonly IMapper _mapper;

        public GetAllServiceUseCase(IServicesReadOnlyRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseServicesJson> Execute()
        {
            var result = await _repository.GetAll();

            return new ResponseServicesJson
            {
                Services = _mapper.Map<List<ResponseShortServiceJson>>(result)
            };

        }
    }
}
