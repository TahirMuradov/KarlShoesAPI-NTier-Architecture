using FluentValidation;
using KarlShoes.Entites.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.FluentValidation.CategoryDTOValidator
{
    public class CategoryUpdateDTOValidatior:AbstractValidator<CategoryUpdateDTO>
    {
        public CategoryUpdateDTOValidatior()
        {
            RuleFor(dto => dto.CategoryId)
                  .NotEmpty();

          
            RuleFor(dto => dto.CategoryNames)
                .NotEmpty().WithName("Kateqoriya Adı")
                .Must(names => names != null && names.Count > 0);

            When(dto => dto.CategoryNames != null, () =>
            {
                RuleForEach(dto => dto.CategoryNames)
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
