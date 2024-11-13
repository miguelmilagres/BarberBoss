using BarberBoss.Application.Services.Register;
using BarberBoss.Exception;
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

    [Fact]
    public void Error_Title_Empty()
    {
        //Arrange
        var validator = new RegisterServiceValidator();
        var request = RequestRegisterServiceJsonBuilder.Build();
        request.Title = string.Empty;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
    }
}
