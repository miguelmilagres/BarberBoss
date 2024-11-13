using BarberBoss.Application.Services.Register;
using BarberBoss.Communication.Enums;
using BarberBoss.Communication.Requests;

namespace Validators.Test.Services.Register;
public class RegisterServiceValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterServiceValidator();
        var request = new RequestRegisterServiceJson
        {
            Title = "Test",
            Date = DateTime.Now.AddDays(-1),
            Comment = "Comment",
            PaymentType = PaymentType.DebitCard,
            Price = 177
        };

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }
}
