
using FluentValidation;
using NetApi.Models.Request;

namespace NetApi.Validations
{
    public class CreateProductValidations : AbstractValidator<ProductRequest>
    {
        public CreateProductValidations()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Nombre es requerido")
                .MaximumLength(100).WithMessage("Nombre no puede ser mayor de 100 caracteres")
                .MinimumLength(5).WithMessage("Nombre no puede ser menor de 5 caracteres");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Descripción es requerida")
                .MaximumLength(255).WithMessage("Descripción no puede ser mayor de 255 caracteres")
                .MinimumLength(10).WithMessage("Descripción no puede ser menor de 10 caracteres");

            RuleFor(x => x.Cost)
                .NotEmpty().WithMessage("Cost is required")
                .GreaterThanOrEqualTo(1).WithMessage("El Costo no puede ser 0");

            RuleFor(x => x.SalePrice)
                .NotEmpty().WithMessage("SalePrice is required")
                .GreaterThanOrEqualTo(1).WithMessage("El Precio Venta no puede ser 0");

            RuleFor(x => x.Stock)
                .NotEmpty().WithMessage("El stock es requerido")
                .GreaterThanOrEqualTo(1).WithMessage("El Stock no puede ser 0");

            RuleFor(x => x.CreatedBy)
                .NotEmpty().WithMessage("El usuario creador es requerido")
                .NotNull().WithMessage("El usuario creador no puede ser nulo");
        }
    }
}