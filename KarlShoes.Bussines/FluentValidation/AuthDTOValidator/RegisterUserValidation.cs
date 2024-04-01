using FluentValidation;
using KarlShoes.Entites.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.FluentValidation.AuthDTOValidator
{
    public class RegisterUserValidation:AbstractValidator<RegisterDTO>
    {
        public RegisterUserValidation()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().WithName("E-poçt");
            RuleFor(x => x.Username).NotEmpty().NotNull().WithName("İstifadəçi adı").MinimumLength(6).WithMessage("İstifadəçi adınız 6 simvoldan az olmamalıdır!");
            RuleFor(x => x.Firstname).NotEmpty().NotNull().WithName("Ad").Must(NotContainDigits).WithMessage("Sadəcə hərflərdən istifadə edə bilərsiniz!");
            RuleFor(x => x.Lastname).NotEmpty().NotNull().WithName("Soyad").Must(NotContainDigits).WithMessage("Sadəcə hərflərdən istifadə edə bilərsiniz!");
            RuleFor(x => x.Password).NotNull().NotEmpty().Equal(x => x.ConfirmPassword).WithName("Şifrə").WithMessage("Şifrəniz təsdiq şifrəsinə bərabır olmalıdır!");
            RuleFor(x => x.ConfirmPassword).NotEmpty().NotNull().Equal(x => x.Password).WithName("Şifrə təsdiqi").WithMessage("Şifrəniz təsdiq şifrəsinə bərabır olmalıdır!");
        }
        private bool NotContainDigits(string value)
        {
            return !string.IsNullOrEmpty(value) && !value.Any(char.IsDigit);
        }
    }
}
