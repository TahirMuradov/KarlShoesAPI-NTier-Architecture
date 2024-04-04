using Entities.DTOs.ShippingMethods;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.FluentValidation.ShippingMethodDTOValidator
{
    public class ShippingMethodAddValidator:AbstractValidator<AddShippingMethodDTO>
    {
        public ShippingMethodAddValidator()
        {
            RuleFor(dto => dto.ShippingContent)
            .NotNull().WithName("Göndərmə məzmunu")
            .NotEmpty().WithName("Göndərmə məzmunu");

            RuleFor(dto => dto.DeliveryPrice)
                .NotNull().WithName("Çatdırılma qiyməti");
        }
    }
}
