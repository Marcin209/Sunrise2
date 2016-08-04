using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Sunrise.Energy.Testing
{
     public class ModelDTO
    {
        public double Latidute { get; set; }
        public DateTime Date { get; set; }
        public double Wats { get; set; }
    }

    public class CalculateValidator : AbstractValidator<ModelDTO>
    {
        public CalculateValidator()
        {
            RuleFor(calculator => calculator.Latidute)
                .NotEmpty()
                .WithMessage("value is required")
                .GreaterThanOrEqualTo(-90.0)
                .WithMessage("Podaj wartośc powyżej -90 stopni")
                .LessThanOrEqualTo(90.0)
                .WithMessage("Podaj wartość poniżej 90 stopni");

            RuleFor(calculate => calculate.Date)
                .NotEmpty()
                .WithMessage("Date is required")
                .NotNull()
                .WithMessage("Nie może być Null");

            RuleFor(calculate => calculate.Wats).GreaterThanOrEqualTo(0.0)
                .WithMessage("Waty powinny być większe 0")
                .LessThanOrEqualTo(1.2)
                .WithMessage("Waty powinny być mniejsze od 1.2 ");
        }
    }
}
