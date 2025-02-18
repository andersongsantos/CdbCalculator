namespace CdbCalculator.Application.Calculators.Interfaces;

public interface ICdbYieldCalculator
{
    (decimal GrossYield, decimal NetYield) CalculateCdbYield(decimal initialValue, int months);
}
