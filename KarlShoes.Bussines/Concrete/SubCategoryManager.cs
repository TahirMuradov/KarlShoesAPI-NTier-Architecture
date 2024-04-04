using KarlShoes.Bussines.Abstarct;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.Entites.DTOs.SubCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Concrete
{
    public class SubCategoryManager : ISubCategoryService
    {
        private readonly ISubCategoryDAL _subCategoryDAL;

        public SubCategoryManager(ISubCategoryDAL subCategoryDAL)
        {
            _subCategoryDAL = subCategoryDAL;
        }

        public IResult SubCategoryAdd(SubCategoryAddDTO subCategoryAddDTO)
        {
          return _subCategoryDAL.SubCategoryAdd(subCategoryAddDTO);
        }

        public IDataResult<List<SubCategoryGetDTO>> SubCategoryAllGet(string LangCode)
        {
           return _subCategoryDAL.SubCategoryAllGet(LangCode);
        }

        public IResult SubCategoryDelete(string id)
        {
            return _subCategoryDAL.SubCategoryDelete(id);
        }

        public IDataResult<SubCategoryGetDTO> SubCategoryGet(string id, string LangCode)
        {
          return  _subCategoryDAL.SubCategoryGet(id, LangCode);
        }

        public IResult SubCategoryUpdate(SubCategoryUpdateDTO subCategoryUpdateDTO)
        {
            return _subCategoryDAL.SubCategoryUpdate(subCategoryUpdateDTO);
        }
    }
}
