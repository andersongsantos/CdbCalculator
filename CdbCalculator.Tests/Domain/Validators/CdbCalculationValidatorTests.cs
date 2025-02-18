using CdbCalculator.Domain.Validators;

namespace CdbCalculator.Tests.Domain.Validators;

public class CdbCalculationValidatorTests
{
    [Fact]
    public void CdbCalculationValidator_ValidateValues()
    {
        var _validator = new CdbCalculationValidator();
        var validationResult = _validator.Validate((1, 2));

        Assert.NotNull(validationResult);
        Assert.True(validationResult.IsValid);
    }

    [Fact]
    public void CdbCalculationValidator_InvalidInitialValue()
    {
        var _validator = new CdbCalculationValidator();
        var validationResult = _validator.Validate((0, 2));

        Assert.NotNull(validationResult);
        Assert.False(validationResult.IsValid);
        Assert.NotNull(validationResult.Errors);
        Assert.NotEmpty(validationResult.Errors);
        Assert.Equal("O valor informado deve ser maior que zero.", validationResult.Errors[0].ErrorMessage);
    }

    [Fact]
    public void CdbCalculationValidator_InvalidInitialMonths()
    {
        var _validator = new CdbCalculationValidator();
        var validationResult = _validator.Validate((1, 1));

        Assert.NotNull(validationResult);
        Assert.False(validationResult.IsValid);
        Assert.NotNull(validationResult.Errors);
        Assert.NotEmpty(validationResult.Errors);
        Assert.Equal("O prazo em meses deve ser maior que 1.", validationResult.Errors[0].ErrorMessage);
    }

    [Fact]
    public void CdbCalculationValidator_InvalidInitialValueMustLessThanLimit()
    {
        var _validator = new CdbCalculationValidator();
        var validationResult = _validator.Validate((10000000000000.0m, 2));

        Assert.NotNull(validationResult);
        Assert.False(validationResult.IsValid);
        Assert.NotNull(validationResult.Errors);
        Assert.NotEmpty(validationResult.Errors);
        Assert.Equal("O valor deve ser menor que R$ 10.000.000.000.000,00.", validationResult.Errors[0].ErrorMessage);
    }

    [Fact]
    public void CdbCalculationValidator_InvalidInitialMonthsMustLessThanLimit()
    {
        var _validator = new CdbCalculationValidator();
        var validationResult = _validator.Validate((1, 1000));

        Assert.NotNull(validationResult);
        Assert.False(validationResult.IsValid);
        Assert.NotNull(validationResult.Errors);
        Assert.NotEmpty(validationResult.Errors);
        Assert.Equal("O prazo em meses deve ser menor que 1000 meses.", validationResult.Errors[0].ErrorMessage);
    }
}
