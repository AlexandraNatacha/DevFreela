using DevFreela.Application.Commands.InsertProject;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class InsertProjectValidator : AbstractValidator<InsertProjectCommand>
    {
        public InsertProjectValidator()
        {
            RuleFor(p => p.Title)
                .NotEmpty()
                    .WithMessage("Não pode ser vazio.")
                .MaximumLength(100)
                    .WithMessage("Tamanho máximo é 100 caracteres.");

            RuleFor(p => p.TotalCost)
                .GreaterThanOrEqualTo(1000)
                    .WithMessage("O projeto deve custar pelo menos R$ 1.000.");
        }
    }
}
