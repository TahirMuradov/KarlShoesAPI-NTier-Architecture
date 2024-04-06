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
                  .Must(BeTwoDigits)
                .GreaterThanOrEqualTo(10).WithMessage("Ölçü 0 ilə başlaya ve 10-dan kiçik ola bilməz!.")
                .GreaterThan(0);
        }
        private bool BeAnInteger(int sizeNumber)
        {
            return int.TryParse(sizeNumber.ToString(), out _);
        }
        private bool BeTwoDigits(int sizeNumber)
        {
           
            return sizeNumber >= 10 && sizeNumber < 100;
        }
    }
}
