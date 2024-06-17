﻿using FluentValidation;

namespace NetApi.Validations.Product
{
    public class UpdateProductValidations : CreateProductValidations
    {
        public UpdateProductValidations()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id es requerido")
                .NotNull().WithMessage("Id no puede ser nulo")
                .GreaterThan(0).WithMessage("Id no puede ser 0");           
            RuleFor(x => x.UpdatedBy)
                .NotEmpty().WithMessage("El usuario actualizador es requerido")
                .NotNull().WithMessage("El usuario actualizador no puede ser nulo");
            RuleFor(x => x.CreatedBy)
                .Null().WithMessage("El usuario creador no puede ser actualizado");
        }
    }
}