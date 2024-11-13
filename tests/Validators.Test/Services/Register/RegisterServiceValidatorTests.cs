using BarberBoss.Application.Services.Register;
using CommonTestUtilities.Requests;

namespace Validators.Test.Services.Register;
public class RegisterServiceValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterServiceValidator();
        var request = RequestRegisterServiceJsonBuilder.Build();

        var result = validator.Validate(request);

        Assert.True(result.IsValid);
    }
}
