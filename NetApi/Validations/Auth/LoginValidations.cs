using FluentValidation;
using NetApi.Models.Request;

namespace NetApi.Validations.Auth
{
    public class LoginValidations : AbstractValidator<LoginRequest>
    {
        public LoginValidations()
        {
            RuleFor(l => l.Password)
                .NotEmpty().
                WithMessage("La Contraseña no puede ser vacia");
            RuleFor(l => l.UserName)
                .NotEmpty()
                .WithMessage("El Nombre de usuario es oblogatorio");
        }
    }
}