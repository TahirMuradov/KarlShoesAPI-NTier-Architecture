using FluentValidation;
using KarlShoes.Entites.DTOs.ProductDTOs;

namespace KarlShoes.Bussines.FluentValidation.ProductDTOValidator
{
    public class ProductUpdateDTOValidator:AbstractValidator<ProductUpdateDTO>
    {
        public ProductUpdateDTOValidator()
        {
            RuleFor(dto => dto.ProductId).NotEmpty().WithName("Məhsul İD");
        
        }
    }
}
