using CdbCalculator.Application.Calculators;

namespace CdbCalculator.Tests.Application.Calculators
{
    public class CdbYieldCalculatorTests
    {
        [Theory]
        [InlineData(10000, 06, 10597.56, 10463.11)] // 06 meses do investimento
        [InlineData(10000, 12, 11230.82, 10984.66)] // 12 meses do investimento
        [InlineData(10000, 24, 12613.13, 12155.84)] // 24 meses do investimento
        [InlineData(10000, 36, 14165.58, 13540.75)] // 36 meses do investimento
        public void CalculateCdbYield_ReturnsExpectedValues(decimal initialValue, int months, decimal expectedGross, decimal expectedNet)
        {
            var (grossYield, netYield) = new CdbYieldCalculator().CalculateCdbYield(initialValue, months);

            Assert.Equal(expectedGross, Math.Round(grossYield, 2, MidpointRounding.ToEven));
            Assert.Equal(expectedNet, Math.Round(netYield, 2, MidpointRounding.ToEven));
        }

        [Theory]
        [InlineData(1000, 06, 1059.76)] // Teste para 6 meses
        [InlineData(1000, 12, 1123.08)] // Teste para 12 meses
        [InlineData(1000, 24, 1261.31)] // Teste para 24 meses
        [InlineData(1000, 36, 1416.56)] // Teste para mais de 24 meses
        public void CalculateCdbValue_ReturnsExpectedFinalValue(decimal initialValue, int months, decimal expectedFinal)
        {
            //Esse método acessa o método estático da classe que calcula o cdb antes de calcular o imposto.
            var finalValue = typeof(CdbYieldCalculator)
                .GetMethod("CalculateCdbValue", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.Invoke(null, [initialValue, months]);

            //Assert dos valores ainda sem o cálculo do imposto.
            Assert.NotNull(finalValue);
            Assert.Equal(expectedFinal, Math.Round((decimal)finalValue, 2, MidpointRounding.ToEven));
        }

        [Theory]
        [InlineData(100, 6, 22.5)]  // Imposto sobre rendimento de 100 por 6 meses (22.5%)
        [InlineData(100, 12, 20.0)] // Imposto sobre rendimento de 100 por 12 meses (20%)
        [InlineData(100, 24, 17.5)] // Imposto sobre rendimento de 100 por 24 meses (17.5%)
        [InlineData(100, 36, 15.0)] // Imposto sobre rendimento de 100 acima de 24 meses (15%)
        public void CalculateIncomeTax_ReturnsExpectedTax(decimal grossYield, int months, decimal expectedTax)
        {
            //Esse método acessa o método estático da classe que calcula o imposto.
            var tax = typeof(CdbYieldCalculator)
                .GetMethod("CalculateIncomeTax", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static)
                ?.Invoke(null, [grossYield, months]);

            //Assert se a taxa está de acordo com os meses.
            Assert.NotNull(tax);
            Assert.Equal(expectedTax, Math.Round((decimal)tax, 2));
        }
    }
}