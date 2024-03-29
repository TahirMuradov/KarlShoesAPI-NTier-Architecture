using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Entites.DTOs.SubCategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Abstarct
{
    public interface ISubCategoryServices
    {
        public IResult SubCategoryDelete(string id);
        public IDataResult<SubCategoryGetDTO> SubCategoryGet(string id, string LangCode);
        public IDataResult<List<SubCategoryGetDTO>> SubCategoryAllGet(string LangCode);
        public IResult SubCategoryAdd(SubCategoryAddDTO subCategoryAddDTO);
        public IResult SubCategoryUpdate (SubCategoryUpdateDTO subCategoryUpdateDTO);
    }
}
