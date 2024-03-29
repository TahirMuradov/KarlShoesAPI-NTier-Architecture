using FluentValidation;
using KarlShoes.Entites.DTOs.SubCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.FluentValidation.SubCategoryDTOValidator
{
    public class SubCategoryAddDTOValidator:AbstractValidator<SubCategoryAddDTO>
    {
        public SubCategoryAddDTOValidator()
        {
            RuleFor(dto => dto.CategoryId).NotEmpty().WithName("Kateqoriya İd");

            RuleFor(dto => dto.SubCategoryName).NotEmpty().WithName("Alt Kateqoriya Adı")
                .Must(dic => dic != null && dic.Count > 0);

            RuleForEach(dto => dto.SubCategoryName.Values)
                .NotEmpty()
                .MaximumLength(100);
        }

    }
}
