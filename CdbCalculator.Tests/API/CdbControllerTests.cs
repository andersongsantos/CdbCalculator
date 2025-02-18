using CdbCalculator.API.Controllers;
using CdbCalculator.Application.Services.Interfaces;
using CdbCalculator.Application.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;

namespace CdbCalculator.Tests.API;

public class CdbControllerTests
{
    private readonly Mock<ICdbService> _mockCdbService;
    private readonly CdbController _controller;

    public CdbControllerTests()
    {
        _mockCdbService = new Mock<ICdbService>();
        _controller = new CdbController(_mockCdbService.Object);
    }

    [Fact]
    public void Calculate_ReturnsBadRequest_WhenServiceReturnsFailure()
    {
        // Arrange
        decimal initialValue = 1000m;
        int months = 12;
        var result = Result<(decimal GrossYield, decimal NetYield)>.Failure(["Invalid data"]);

        _mockCdbService.Setup(s => s.CalculateYield(initialValue, months)).Returns(result);

        // Act
        var actionResult = _controller.Calculate(initialValue, months);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
        var errors = badRequestResult.Value as IEnumerable<string>;
        Assert.NotNull(errors);
        Assert.Contains("Invalid data", errors);
    }

    [Fact]
    public void Calculate_ReturnsOkResult_WhenServiceReturnsSuccess()
    {
        // Arrange
        decimal initialValue = 1000m;
        int months = 12;
        var expectedGrossYield = 1050m;
        var expectedNetYield = 1020m;
        var result = Result<(decimal GrossYield, decimal NetYield)>.Success((expectedGrossYield, expectedNetYield));

        _mockCdbService.Setup(s => s.CalculateYield(initialValue, months)).Returns(result);

        // Act
        var actionResult = _controller.Calculate(initialValue, months);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);

        // Convertendo o valor para string JSON e depois para JObject
        var jsonString = System.Text.Json.JsonSerializer.Serialize(okResult.Value);
        var resultValue = JObject.Parse(jsonString);

        // Verificando a nulidade antes de converter
        Assert.True(resultValue.ContainsKey("GrossYield"));
        Assert.True(resultValue.ContainsKey("NetYield"));

        Assert.NotNull(resultValue["GrossYield"]);
        Assert.NotNull(resultValue["NetYield"]);

        Assert.Equal(Math.Round(expectedGrossYield, 2, MidpointRounding.ToEven), (decimal?)resultValue["GrossYield"]);
        Assert.Equal(Math.Round(expectedNetYield, 2, MidpointRounding.ToEven), (decimal?)resultValue["NetYield"]);
    }
}
