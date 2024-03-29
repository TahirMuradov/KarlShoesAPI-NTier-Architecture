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
                .NotNull()
                .NotEmpty()
                .Must((dto, categoryName) => dto.LangCode != null && categoryName.Count == dto.LangCode.Count)
                    
                .ForEach(category => category.MaximumLength(255));

            RuleFor(dto => dto.LangCode)
                .NotNull()
                .NotEmpty()
                .ForEach(langCode => langCode.Length(2));
        }
    }
}
