using KarlShoes.Bussines.Abstarct;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.Entites.DTOs.CategoryDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Concrete
{
    public class CategoryManager : ICategoryServices
    {
        private readonly ICategoryDAL _categoryDAL;

        public CategoryManager(ICategoryDAL categoryDAL)
        {
            _categoryDAL = categoryDAL;
        }

        public async Task<IResult> AddCategoryAsync(CategoryAddDTO categoryAddDTO)
        {
           
            return await _categoryDAL.AddCategoryAsync(categoryAddDTO);
        }

        public IResult DeleteCategory(string id)
        {
          return _categoryDAL.DeleteCategory(id);
        }

        public IDataResult<List<CategoryGetDTO>> GetAllCategory(string LangCode)
        {
            return _categoryDAL.GetAllCategory(LangCode);
        }

        public IDataResult<CategoryGetDTO> GetCategory(string id, string LangCode)
        {
           return _categoryDAL.GetCategory(id, LangCode);
        }

        public IResult UpdateCatgeory(CategoryUpdateDTO categoryUpdateDTO)
        {
           return _categoryDAL.UpdateCatgeory(categoryUpdateDTO);
        }
    }
}
