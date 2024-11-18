using BarberBoss.Application.Services;
using BarberBoss.Communication.Enums;
using BarberBoss.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.Test.Services.Register;
public class RegisterServiceValidatorTests
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new ServiceValidator();
        var request = RequestRegisterServiceJsonBuilder.Build();

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("         ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        //Arrange
        var validator = new ServiceValidator();
        var request = RequestRegisterServiceJsonBuilder.Build();
        request.Title = string.Empty;
        request.Title = title;

        //Act
        var result = validator.Validate(request);

        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
    }
    [Fact]
    public void Error_Date_Future()
    {
        //Arrange
        var validator = new ServiceValidator();
        var request = RequestRegisterServiceJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.SERVICES_CANNOT_FOR_THE_FUTURE));
    }
    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        //Arrange
        var validator = new ServiceValidator();
        var request = RequestRegisterServiceJsonBuilder.Build();
        request.PaymentType = (PaymentType)700;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
    }
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-2)]
    [InlineData(-7)]
    public void Error_Price_Invalid(decimal price)
    {
        //Arrange
        var validator = new ServiceValidator();
        var request = RequestRegisterServiceJsonBuilder.Build();
        request.Price = price;
        //Act
        var result = validator.Validate(request);
        //Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PRICE_MUST_BE_GREATER_THAN_ZERO));
    }
}