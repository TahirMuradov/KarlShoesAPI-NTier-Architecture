using FluentValidation;
using KarlShoes.Entites.DTOs.SizeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.FluentValidation.SIizeDTOValidator
{
    public class SizeUpdateDTOValidator:AbstractValidator<SizeUpdateDTO>
    {
        public SizeUpdateDTOValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.NewSizeNumber)
                .GreaterThan(0)
                .Must(HaveAtMostOneTrailingZero).WithName("Ölçü");
        }
        private bool HaveAtMostOneTrailingZero(int size)
        {
            string sizeString = size.ToString();
            int zeroCount = 0;

            for (int i = sizeString.Length - 1; i >= 0; i--)
            {
                if (sizeString[i] == '0')
                {
                    zeroCount++;
                }
                else
                {
                    break;
                }
            }

            return zeroCount <= 1;
        }
    }
}
