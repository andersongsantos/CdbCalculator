using CdbCalculator.Application.Calculators.Interfaces;
using CdbCalculator.Application.Constants;

namespace CdbCalculator.Application.Calculators;

public class CdbYieldCalculator : ICdbYieldCalculator
{
    /// <summary>
    /// Calcula o valor bruto e líquido do valor investido no CDB no período de meses informados
    /// </summary>
    /// <param name="initialValue">O valor inicial investido no CDB.</param>
    /// <param name="months">Meses investidos.</param>
    /// <returns>Tupla do valor bruto e liquido investido</returns>
    public (decimal GrossYield, decimal NetYield) CalculateCdbYield(decimal initialValue, int months)
    {
        decimal finalGrossValue = CalculateCdbValue(initialValue, months);

        decimal grossYield = finalGrossValue - initialValue;

        decimal tax = CalculateIncomeTax(grossYield, months);

        decimal finalNetValue = finalGrossValue - tax;

        return (finalGrossValue, finalNetValue);
    }

    /// <summary>
    /// Calcula o valor final de um CDB aplicando juros compostos mensalmente, sem arredondamentos intermediários.
    /// Utiliza o tipo decimal para garantir a máxima precisão nos cálculos, evitando erros de arredondamento que poderiam distorcer o valor final.
    /// </summary>
    /// <param name="initialValue">O valor inicial investido no CDB.</param>
    /// <param name="months">Meses investidos.</param>
    /// <returns>Valor final calculado do CDB antes de calcular o imposto.</returns>
    private static decimal CalculateCdbValue(decimal initialValue, int months)
    {
        decimal finalValue = initialValue;

        for (int i = 0; i < months; i++)
            finalValue *= (1 + (FinancialConstants.CdiRate * FinancialConstants.BankRate));

        return finalValue;
    }

    /// <summary>
    /// Calcula o valor do imposto de renda sobre o rendimento bruto do investimento em CDB.
    /// Utiliza uma tabela de taxas de IRPF para encontrar a alíquota correta, que varia de acordo com o prazo do investimento.
    /// </summary>
    /// <param name="grossYield">Valor total dos ganhos</param>
    /// <param name="months">Meses investidos.</param>
    /// <returns>Valor total com impostos calculados</returns>
    private static decimal CalculateIncomeTax(decimal grossYield, int months)
    {
        decimal taxRate = FinancialConstants.CdbIrpfRates
            .Where(x => months <= x.Key)
            .OrderBy(x => x.Key)
            .First().Value;

        return grossYield * taxRate;
    }
}
