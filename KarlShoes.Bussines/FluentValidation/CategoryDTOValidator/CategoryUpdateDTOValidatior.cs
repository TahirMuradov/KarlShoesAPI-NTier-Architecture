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
                  .NotEmpty().NotNull().WithName("Kateqoriya Id boş ola bilməz! ");

          
            
        }
    }
}
