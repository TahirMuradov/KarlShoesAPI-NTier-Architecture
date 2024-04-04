using KarlShoes.Bussines.Abstarct;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.Entites.DTOs.SizeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Concrete
{
    public class SizeManager : ISizeService
    {
        private readonly ISizeDAL _sizeDAL;

        public SizeManager(ISizeDAL sizeDAL)
        {
            _sizeDAL = sizeDAL;
        }

        public IResult SizeAdd(SizeAddDTO sizeAddDTO)
        {
            return _sizeDAL.SizeAdd(sizeAddDTO);
        }

        public IDataResult<List<SizeGetDTO>> SizeAllGet()
        {
          return _sizeDAL.SizeAllGet();
        }

        public IDataResult<SizeGetDTO> SizeGet(string SizeId)
        {
           return _sizeDAL.SizeGet(SizeId);
        }

        public IResult SizeRemove(string SizeId)
        {
           return _sizeDAL.SizeRemove(SizeId);
        }

        public IResult SizeUpdate(SizeUpdateDTO sizeUpdateDTO)
        {
           return _sizeDAL.SizeUpdate(sizeUpdateDTO);
        }
    }
}
