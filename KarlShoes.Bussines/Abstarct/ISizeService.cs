using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Entites.DTOs.SizeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Abstarct
{
    public interface ISizeService
    {
        public IResult SizeAdd(SizeAddDTO sizeAddDTO);
        public IResult SizeRemove(string SizeId);
        public IDataResult<SizeGetDTO> SizeGet(string SizeId);
        public IDataResult<List<SizeGetDTO>> SizeAllGet();
        public IResult SizeUpdate(SizeUpdateDTO sizeUpdateDTO);
    }
}
