using FluentValidation;
using KarlShoes.Entites.DTOs.SubCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.FluentValidation.SubCategoryDTOValidator
{
    public class SubCategoryUpdateDTOValidation:AbstractValidator<SubCategoryUpdateDTO>
    {
        public SubCategoryUpdateDTOValidation()
        {
            RuleFor(dto => dto.SubCategoryId)
               .NotEmpty().NotNull().WithName("Alt kateqoriya İd");
        }
    }
}
