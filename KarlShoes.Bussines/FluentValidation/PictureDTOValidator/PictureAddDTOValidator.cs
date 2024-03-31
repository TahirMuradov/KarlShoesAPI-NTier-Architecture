using FluentValidation;
using KarlShoes.Entites.DTOs.PictureDTOs;

namespace KarlShoes.Bussines.FluentValidation.PictureDTOValidator
{
    public class PictureAddDTOValidator:AbstractValidator<PictureAddDTO>
    {
        public PictureAddDTOValidator()
        {
            RuleFor(dto => dto.FormFiles)
           .NotNull().WithName("Şəskil")
           .NotEmpty().WithName("Şəkil");

            RuleFor(dto => dto.ProductId)
                .NotEmpty().WithName("MəhsulId")
                .NotNull().WithName("MəhsulId");
        }
    }
}
