using FluentValidation;
using KarlShoes.Entites.DTOs.SizeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.FluentValidation.SIizeDTOValidator
{
    public class SizeAddDTOValidator:AbstractValidator<SizeAddDTO>
    {
        public SizeAddDTOValidator()
        {
            RuleFor(dto => dto.SizeNumber)
                .NotEmpty().WithName("Ölçü")
                 .Must(BeAnInteger).WithMessage("Ölçüyə Ancaq Rəqəm Yazılabilər!")
                .GreaterThan(0);
        }
        private bool BeAnInteger(int sizeNumber)
        {
            return int.TryParse(sizeNumber.ToString(), out _);
        }
    }
}
