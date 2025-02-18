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
        }
    }
}
