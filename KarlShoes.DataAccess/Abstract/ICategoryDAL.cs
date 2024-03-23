using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Entites.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.DataAccess.Abstract
{
    public interface ICategoryDAL
    {
        public Task<IResult> AddCategoryAsync(CategoryAddDTO categoryAddDTO);
        public IResult DeleteCategory(string id);
        public IDataResult<CategoryGetDTO> GetCategory(string id,string LangCode);
    }
}
