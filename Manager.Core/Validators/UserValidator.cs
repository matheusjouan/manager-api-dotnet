using FluentValidation;
using Manager.Core.Entities;

namespace Manager.Core.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        // FluentValidation funciona através do construtor
        public UserValidator()
        {
            // Validação da Entidade
            RuleFor(x => x)
              .NotEmpty()
              .WithMessage("Entity cannot be empty.")
              .NotNull()
              .WithMessage("Entity cannot be null.");

            // Validação para campo específico
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name cannot be null.")
                .NotNull()
                .WithMessage("Name cannot be null.")
                .MinimumLength(6)
                .WithMessage("Name must be at least 6 characters.")
                .MaximumLength(80)
                .WithMessage("Name must have a maximum 12 characters.");

            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("Email cannot be null.")
                .NotNull()
                .WithMessage("Email cannot be null.")
                .MinimumLength(10)
                .WithMessage("Email must be at least 10 characters.")
                .MaximumLength(180)
                .WithMessage("Email must have a maximum 180 characters.")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("E-mail must be valid");

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("Password cannot be null.")
                .NotNull()
                .WithMessage("Password cannot be null.")
                .MinimumLength(3)
                .WithMessage("Password must be at least 3 characters.")
                .MaximumLength(100)
                .WithMessage("Password must have a maximum 100 characters.");
        }
    }
}
