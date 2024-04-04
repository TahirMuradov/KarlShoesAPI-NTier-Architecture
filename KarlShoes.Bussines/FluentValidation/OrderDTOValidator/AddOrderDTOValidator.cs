using FluentValidation;
using KarlShoes.Entites.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.FluentValidation.OrderDTOValidator
{
    public class AddOrderDTOValidator:AbstractValidator<AddOrderDTO>
    {

        public AddOrderDTOValidator()
        {
            RuleFor(x => x.FirstName)
              .NotEmpty().WithMessage("Ad boş ola bilməz")
              .WithName("Ad");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad boş ola bilməz")
                .WithName("Soyad");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon nömrəsi boş ola bilməz")
                .WithName("Telefon Nömrəsi")
                .Matches(@"^\+994\d{9}$").WithMessage("Telefon nömrəsi +994 ilə başlamalı və 9 rəqəm olmalıdır");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-poçt boş ola bilməz")
                .EmailAddress().WithMessage("Düzgün e-poçt formatı daxil edilməlidir")
                .WithName("E-poçt");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Ünvan boş ola bilməz")
                .WithName("Ünvan");

            RuleFor(x => x.OrderNumber)
                .NotEmpty().WithMessage("Sifariş nömrəsi boş ola bilməz")
                .WithName("Sifariş Nömrəsi");

            RuleFor(x => x.Message)
                .MaximumLength(250).WithMessage("Mesaj maksimum 250 simvoldan ibarət olmalıdır")
                .WithName("Mesaj");

            RuleFor(x => x.OrderPDfUrl)
                .NotEmpty().WithMessage("Sifariş PDF URL boş ola bilməz")
                .WithName("Sifariş PDF URL");

            RuleFor(x => x.OrderProducts)
                .NotEmpty().WithMessage("Sifariş məhsulları boş ola bilməz")
                .WithName("Sifariş Məhsulları");

            RuleFor(x => x.PaymentMethodId)
                .NotEmpty().WithMessage("Ödəniş metod İD boş ola bilməz")
                .WithName("Ödəniş Metod İD");

            RuleFor(x => x.ShippingMethodId)
                .NotEmpty().WithMessage("Göndərilmə metod İD boş ola bilməz")
                .WithName("Göndərilmə Metod İD");
        }
    }
}
