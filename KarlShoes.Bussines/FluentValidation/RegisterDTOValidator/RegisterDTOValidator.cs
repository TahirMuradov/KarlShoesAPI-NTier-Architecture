using FluentValidation;
using KarlShoes.Entites.DTOs.UserDTOs;

namespace KarlShoes.Bussines.FluentValidation.RegisterUserValidator
{
    public class RegisterDTOValidator:AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
         
                RuleFor(x => x.Firstname).NotEmpty().NotNull();
            RuleFor(x => x.UserName).NotEmpty().NotNull();
                RuleFor(x => x.Lastname).NotEmpty();
                RuleFor(x => x.Email).NotEmpty()
                    .EmailAddress();
                RuleFor(x => x.PhoneNumber).NotEmpty();
                RuleFor(x => x.Password).NotEmpty()
                    .MinimumLength(8);
                RuleFor(x => x.ConfirmPassword).NotEmpty()
                    .Equal(x => x.Password) ;
            
        }
    }
}
