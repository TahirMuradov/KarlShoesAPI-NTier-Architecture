using FluentValidation;
using KarlShoes.Entites.DTOs.ProductDTOs;

namespace KarlShoes.Bussines.FluentValidation.ProductDTOValidator
{
    public class ProductAddDTOValidator : AbstractValidator<ProductAddDTO>
    {
        public ProductAddDTOValidator()

        {
            RuleFor(x => x.LangCodeAndProductName)
                .NotEmpty().WithName("DilKoduVeMehsulAdi")
                .Must(dic => dic != null && dic.Values.All(value => !string.IsNullOrEmpty(value)));

            RuleFor(x => x.LangCodeAndProductDescription)
                .NotEmpty().WithName("DilKoduVəMəhsulTəsviri")
                .Must(dic => dic != null && dic.Values.All(value => !string.IsNullOrEmpty(value)));


            RuleFor(x => x.SizeAndCount)
                .NotEmpty().WithName("OlcuVeSay")
                .Must(dic => dic != null && dic.Count > 0 && dic.Values.All(value => value > 0));

            RuleFor(x => x.CatgeoryId)
                .NotEmpty().WithName("KateqoriyaId")
                .Must(list => list != null && list.Count > 0 && list.All(value => !string.IsNullOrEmpty(value)));

            RuleFor(x => x.SubCategoryID)
                .Must(list => list == null || list.All(value => !string.IsNullOrEmpty(value)));

            RuleFor(x => x.color)
                .NotEmpty().WithName("Rəng")
                .MaximumLength(100).WithName("Rəng");

            RuleFor(x => x.DisCount)
                .GreaterThanOrEqualTo(0).WithName("Endirim");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithName("Qiymət");

            RuleFor(x => x.isFeatured).NotNull().WithName("XüsusiMehsul");
        }
    }
}
