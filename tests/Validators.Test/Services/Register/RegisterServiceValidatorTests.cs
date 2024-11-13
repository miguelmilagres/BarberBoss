using BarberBoss.Application.Services.Register;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Test.Services.Register;
public class RegisterServiceValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new RegisterServiceValidator();
        var request = RequestRegisterServiceJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }
}
