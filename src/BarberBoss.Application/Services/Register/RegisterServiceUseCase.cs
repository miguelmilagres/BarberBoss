using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Requests;
using BarberBoss.Communication.Responses;

namespace BarberBoss.Application.Services.Register
{
    public class RegisterServiceUseCase
    {
        public ResponseRegisteredServiceJson Execute(RequestRegisterServiceJson request)
        {
            Validate(request);

            return new ResponseRegisteredServiceJson();
        }

        private void Validate(RequestRegisterServiceJson request)
        {
            var titleIsEmpty = string.IsNullOrEmpty(request.Title);
            if (titleIsEmpty)
            {
                throw new ArgumentException("The title is required.");
            }

            if (request.Price < 0)
            {
                throw new ArgumentException("The Price must be greater than zero.");
            }

            var result = DateTime.Compare(request.Date, DateTime.UtcNow);
            if (result < 0)
            {
                throw new ArgumentException("Services cannot be for the future");
            }

            var paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);
            if (paymentTypeIsValid == false)
            {
                throw new ArgumentException("Payment Type is not valid.");
            }
        }
    }
}
