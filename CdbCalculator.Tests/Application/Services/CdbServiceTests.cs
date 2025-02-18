using CdbCalculator.Application.Calculators.Interfaces;
using CdbCalculator.Application.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace CdbCalculator.Tests.Application.Services;

public class CdbServiceTests
{
    private readonly Mock<ICdbYieldCalculator> _cdbYieldCalculatorMock = new();
    private readonly Mock<IValidator<(decimal, int)>> _validatorMock = new();
    private readonly Mock<ILogger<CdbService>> _loggerMock = new(); // Mock do logger

    [Fact]
    public void CdbService_ReturnsExpectedValues()
    {
        decimal initialValue = 10000;
        int months = 6;
        decimal expectedGross = 10597.56m;
        decimal expectedNet = 10463.11m;

        _cdbYieldCalculatorMock.Setup(x => x.CalculateCdbYield(initialValue, months))
            .Returns((expectedGross, expectedNet));

        var cdbService = new CdbService(_cdbYieldCalculatorMock.Object, _loggerMock.Object);

        var result = cdbService.CalculateYield(initialValue, months);

        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
        Assert.Equal(expectedGross, result.Value.GrossYield);
        Assert.Equal(expectedNet, result.Value.NetYield);

        VerifyMocks();
    }

    [Fact]
    public void CdbService_ReturnsFailure_WhenInitialValueInvalid()
    {
        decimal initialValue = 0; // Inválido
        int months = 6;

        var cdbService = new CdbService(_cdbYieldCalculatorMock.Object, _loggerMock.Object);

        var result = cdbService.CalculateYield(initialValue, months);

        Assert.NotNull(result);
        Assert.NotNull(result.Errors);
        Assert.False(result.IsSuccess);
        Assert.Contains("O valor informado deve ser maior que zero.", result.Errors);

        VerifyMocks();
    }

    [Fact]
    public void CdbService_ReturnsFailure_WhenMonthsInvalid()
    {
        decimal initialValue = 10000;
        int months = 0; // Inválido

        var cdbService = new CdbService(_cdbYieldCalculatorMock.Object, _loggerMock.Object);

        var result = cdbService.CalculateYield(initialValue, months);

        Assert.NotNull(result);
        Assert.NotNull(result.Errors);
        Assert.False(result.IsSuccess);
        Assert.Contains("O prazo em meses deve ser maior que 1.", result.Errors);

        VerifyMocks();
    }

    [Fact]
    public void CdbService_ReturnsFailure_WhenCalculationFails()
    {
        decimal initialValue = 10000;
        int months = 6;

        // Faz o mock do calculador para lançar uma exceção
        _cdbYieldCalculatorMock.Setup(x => x.CalculateCdbYield(initialValue, months))
            .Throws(new Exception("Erro interno ao calcular rendimento"));

        // Instancia o CdbService com o mock do logger
        var cdbService = new CdbService(_cdbYieldCalculatorMock.Object, _loggerMock.Object);

        var result = cdbService.CalculateYield(initialValue, months);

        _loggerMock.Verify(
               x => x.Log(
                   LogLevel.Error,
                   It.IsAny<EventId>(),
                   It.Is<It.IsAnyType>((o, t) => string.Equals("Erro ao calcular rendimento do CDB", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                   It.IsAny<Exception>(),
                   It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
               Times.Once);

        Assert.NotNull(result);
        Assert.NotNull(result.Errors);
        Assert.False(result.IsSuccess);
        Assert.Contains("Erro interno ao calcular rendimento.", result.Errors); // Verifica a mensagem de erro
        VerifyMocks();
    }

    private void VerifyMocks()
    {
        _cdbYieldCalculatorMock.VerifyAll();
        _validatorMock.VerifyAll();
        _loggerMock.VerifyAll();

        _cdbYieldCalculatorMock.VerifyNoOtherCalls();
        _validatorMock.VerifyNoOtherCalls();
        _loggerMock.VerifyNoOtherCalls();
    }
}
