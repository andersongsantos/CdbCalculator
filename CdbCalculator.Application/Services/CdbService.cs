using CdbCalculator.Application.Calculators.Interfaces;
using CdbCalculator.Application.Services.Interfaces;
using CdbCalculator.Application.Shared;
using CdbCalculator.Domain.Validators;
using Microsoft.Extensions.Logging;

namespace CdbCalculator.Application.Services;

public class CdbService(ICdbYieldCalculator cdbYieldCalculator, ILogger<CdbService> logger) : ICdbService
{
    private readonly ICdbYieldCalculator _cdbYieldCalculator = cdbYieldCalculator;
    private readonly CdbCalculationValidator _validator = new();
    private readonly ILogger<CdbService> _logger = logger;

    public Result<(decimal GrossYield, decimal NetYield)> CalculateYield(decimal initialValue, int months)
    {
        var validationResult = _validator.Validate((initialValue, months));
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage);
            return Result<(decimal, decimal)>.Failure(errors);
        }

        try
        {
            var result = _cdbYieldCalculator.CalculateCdbYield(initialValue, months);
            return Result<(decimal, decimal)>.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao calcular rendimento do CDB");
            return Result<(decimal, decimal)>.Failure(["Erro interno ao calcular rendimento."]);
        }
    }
}
