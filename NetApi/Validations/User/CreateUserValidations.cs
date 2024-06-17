using FluentValidation;
using NetApi.Models.Request;


namespace NetApi.Validations.User
{
    public class CreateUserValidations : AbstractValidator<UserRequest>
    {
        public CreateUserValidations()
        {
            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("El email es requerido");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("La contraseña es requerida")
                .NotNull().WithMessage("La contraseña no puede ser nula")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z0-9]).{8,15}$").WithMessage("La contraseña debe contener al menos 8 caracteres, una letra mayúscula, una letra minúscula, un número y un caracter especial");

            RuleFor(u => u.Name)
                .NotEmpty().WithMessage("El nombre es requerido")
                .MaximumLength(100).WithMessage("El nombre no puede ser mayor de 100 caracteres")
                .MinimumLength(5).WithMessage("El nombre no puede ser menor de 5 caracteres");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("El apellido es requerido")
                .MaximumLength(100).WithMessage("El apellido no puede ser mayor de 100 caracteres")
                .MinimumLength(5).WithMessage("El apellido no puede ser menor de 5 caracteres");

            RuleFor(u => u.UserName)
                .NotEmpty().WithMessage("El nombre de usuario es requerido")
                .MaximumLength(100).WithMessage("El nombre de usuario no puede ser mayor de 100 caracteres")
                .MinimumLength(5).WithMessage("El nombre de usuario no puede ser menor de 5 caracteres");

            RuleFor(u => u.Active)
                .NotEmpty().WithMessage("El estado es requerido");

            RuleFor(u => u.Number)
                .NotEmpty().WithMessage("El número es requerido")
                .NotNull().WithMessage("El número no puede ser nulo")
                .MinimumLength(8).WithMessage("El número no puede ser menor de 8 caracteres")
                .MaximumLength(15).WithMessage("El número no puede ser mayor de 15 caracteres")
                .Matches(@"^[0-9]*$").WithMessage("El número solo puede contener números");
        }
    }
}