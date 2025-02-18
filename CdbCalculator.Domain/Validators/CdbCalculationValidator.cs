using FluentValidation;

namespace CdbCalculator.Domain.Validators
{
    public class CdbCalculationValidator : AbstractValidator<(decimal InitialValue, int Months)>
    {
        public CdbCalculationValidator()
        {
            RuleFor(x => x.InitialValue)
                .GreaterThan(0).WithMessage("O valor informado deve ser maior que zero.");

            RuleFor(x => x.Months)
                .GreaterThan(1).WithMessage("O prazo em meses deve ser maior que 1.");

            RuleFor(x => x.InitialValue)
                .LessThan(10000000000000.0m).WithMessage("O valor deve ser menor que R$ 10.000.000.000.000,00.");

            RuleFor(x => x.Months)
                .LessThan(1000).WithMessage("O prazo em meses deve ser menor que 1000 meses.");
        }
    }
}
