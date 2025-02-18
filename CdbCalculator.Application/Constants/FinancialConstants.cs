using System.Collections.Immutable;

namespace CdbCalculator.Application.Constants;

public static class FinancialConstants
{
    public const decimal BankRate = 1.08m;
    public const decimal CdiRate = 0.009m;

    public static readonly ImmutableDictionary<int, decimal> CdbIrpfRates = ImmutableDictionary.CreateRange(
    [
        KeyValuePair.Create(6, 0.225m),
        KeyValuePair.Create(12, 0.20m),
        KeyValuePair.Create(24, 0.175m),
        KeyValuePair.Create(int.MaxValue, 0.15m)
    ]);
}
