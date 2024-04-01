using FluentValidation;
using KarlShoes.Entites.DTOs.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.FluentValidation.AuthDTOValidator
{
    public class LoginUserValidation:AbstractValidator<LoginDTO>
    {
        public LoginUserValidation()
        {
            RuleFor(x => x.EmailOrUsername).NotNull().NotEmpty().WithName("İstifadəçi adı və ya E-poçt");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithName("Şifrə");
        }
    }
}
