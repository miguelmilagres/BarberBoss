using AutoMapper;
using BarberBoss.Communication.Requests;
using BarberBoss.Domain.Repositories;
using BarberBoss.Exception;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.Services.Update;
public class UpdateServiceUseCase : IUpdateServiceUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IServicesUpdateOnlyRepository _repository;

    public UpdateServiceUseCase(IUnitOfWork unitOfWork, IMapper mapper, IServicesUpdateOnlyRepository repository)
    {
        _unitOfWork = unitOfWork;  
        _mapper = mapper;
        _repository = repository;
    }
    public async Task Execute(long id, RequestServiceJson request)
    {
        Validate(request);

        var service = await _repository.GetById(id);

        if (service is null)
            throw new NotFoundException(ResourceErrorMessages.SERVICE_NOT_FOUND);

        _mapper.Map(request, service);

        _repository.Update(service);

        await _unitOfWork.Commit();
    }

    private void Validate(RequestServiceJson request)
    {
        var validator = new ServiceValidator();
        var result = validator.Validate(request);
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}