using Entities.DTOs.PaymentMethodDTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.FluentValidation.PaymentMethodDTOValidator
{
    public class PaymentMethodAddValidator:AbstractValidator<AddPaymentMethodsDTO>
    {
        public PaymentMethodAddValidator()
        {
            RuleFor(dto => dto.Content)
               .NotNull()
               .NotEmpty().WithName("Məzmun");

            RuleFor(dto => dto.Api)
                .NotNull().WithName("API");
        }
    }
}
