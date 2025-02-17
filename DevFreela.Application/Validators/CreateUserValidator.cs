using DevFreela.Application.Commands.InsertUser;
using FluentValidation;

namespace DevFreela.Application.Validators
{
    public class CreateUserValidator : AbstractValidator<InsertUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.Email)
                .EmailAddress()
                    .WithMessage("E-mail inválido.");

            RuleFor(u => u.BrithDate)
                .Must(d => d < DateTime.Now.AddYears(-18))
                .WithMessage("O usuário deve ser maior de idade.");

        }
    }
}
