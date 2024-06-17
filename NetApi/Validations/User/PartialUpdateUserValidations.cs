using FluentValidation;
using NetApi.Models.Request;

namespace NetApi.Validations.User
{
    public class PartialUpdateUserValidations : AbstractValidator<UserRequest>
    {
        public PartialUpdateUserValidations()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id es requerido")
                .NotNull().WithMessage("Id no puede ser nulo")
                .GreaterThan(0).WithMessage("Id no puede ser 0");
        }
    }

}