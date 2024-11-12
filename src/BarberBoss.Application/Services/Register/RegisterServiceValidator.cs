using BarberBoss.Communication.Requests;
using FluentValidation;

namespace BarberBoss.Application.Services.Register;
public class RegisterServiceValidator : AbstractValidator<RequestRegisterServiceJson>
{
    public RegisterServiceValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty().WithMessage("The title is required.");
        RuleFor(expense => expense.Price).GreaterThan(0).WithMessage("The Price must be greater than zero.");
        RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Expenses cannot be for the future");
        RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage("Payment Type is not valid.");
    }
}
