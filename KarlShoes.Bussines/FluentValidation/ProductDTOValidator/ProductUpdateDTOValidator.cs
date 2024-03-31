using FluentValidation;
using KarlShoes.Entites.DTOs.ProductDTOs;

namespace KarlShoes.Bussines.FluentValidation.ProductDTOValidator
{
    public class ProductUpdateDTOValidator:AbstractValidator<ProductUpdateDTO>
    {
        public ProductUpdateDTOValidator()
        {
            RuleFor(dto => dto.ProductId).NotEmpty().WithName("Məhsul İD");
            RuleFor(dto => dto.LangCodeAndProductName).NotNull().NotEmpty().WithName("Məhsul adı");
            RuleForEach(dto => dto.LangCodeAndProductName.Values).NotEmpty().When(dto => dto.LangCodeAndProductName != null).WithName("Məhsul adı");
            RuleFor(dto => dto.LangCodeAndProductDescription).NotNull().NotEmpty().WithName("Məhsul təsviri");
            RuleForEach(dto => dto.LangCodeAndProductDescription.Values).NotEmpty().When(dto => dto.LangCodeAndProductDescription != null).WithName("Məhsul təsviri");
            RuleFor(dto => dto.SizeAndCount).NotNull().WithName("Ölçü və sayı");
            RuleForEach(dto => dto.SizeAndCount.Keys).GreaterThan(0).WithMessage("Ölçü 0-dan böyük olmalıdır").When(dto => dto.SizeAndCount != null).WithName("Ölçü və sayı");
            RuleForEach(dto => dto.CatgeoryId).NotEmpty().When(dto => dto.CatgeoryId != null).WithName("Kateqoriya");
            RuleFor(dto => dto.color).NotEmpty().WithName("Rəng");
            RuleFor(dto => dto.DisCount).GreaterThanOrEqualTo(0).WithName("Endirim");
            RuleFor(dto => dto.Price).GreaterThan(0).WithName("Qiymət");
        }
    }
}
