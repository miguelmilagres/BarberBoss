using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;
using BarberBoss.Domain.Entities;
using BarberBoss.Exception.ExceptionBase;

namespace BarberBoss.Application.Services.Register
{
    public class RegisterServiceUseCase
    {
        public ResponseRegisteredServiceJson Execute(RequestRegisterServiceJson request)
        {
            Validate(request);

            var entity = new Service
            {
                Title = request.Title,
                Comment = request.Comment,
                Date = request.Date,
                Price = request.Price,
                PaymentType = (Domain.Enums.PaymentType)request.PaymentType
            };

            return new ResponseRegisteredServiceJson();
        }

        private void Validate(RequestRegisterServiceJson request)
        {
            var validator = new RegisterServiceValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
