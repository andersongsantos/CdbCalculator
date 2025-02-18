using CdbCalculator.Application.Shared;

namespace CdbCalculator.Application.Services.Interfaces;

public interface ICdbService
{
    Result<(decimal GrossYield, decimal NetYield)> CalculateYield(decimal initialValue, int months);
}
