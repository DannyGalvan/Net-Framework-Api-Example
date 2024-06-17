
using FluentValidation;

namespace NetApi.Validations.User
{
    public class UpdateUserValidations : CreateUserValidations
    {
        public UpdateUserValidations() { 

            RuleFor(u => u.Id)
                .NotEmpty().WithMessage("El id es requerido")
                .NotNull().WithMessage("El id no puede ser nulo")
                .GreaterThan(0).WithMessage("El id no puede ser 0");
        }
    }
}