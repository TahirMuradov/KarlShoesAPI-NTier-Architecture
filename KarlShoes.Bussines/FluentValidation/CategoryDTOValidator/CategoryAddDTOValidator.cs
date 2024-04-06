using FluentValidation;
using KarlShoes.Entites.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.FluentValidation.CategoryDTOValidator
{
    public class CategoryAddDTOValidator:AbstractValidator<CategoryAddDTO>
    {
        public CategoryAddDTOValidator()
        {
          

            RuleFor(dto => dto.CreatorUserId)
                .NotEmpty()
                .MaximumLength(255)
                .WithName("Yaradan Istifadeci");
            RuleFor(dto => dto.CategoryName)
        .NotEmpty().WithName("Kateqoriya Adı")
        .Must(names => names != null && names.Count > 0);

            When(dto => dto.CategoryName != null, () =>
            {
                RuleForEach(dto => dto.CategoryName)
                    .ChildRules(names =>
                    {
                        names.RuleFor(pair => pair.Key)
                            .NotEmpty().WithMessage("Kateqoriya dil codu boş ola bilməz!");

                        names.RuleFor(pair => pair.Value)
                            .NotEmpty().WithMessage("Kateqoriya adı boş ola bilməz");
                    });
            });

        }
    }
}
