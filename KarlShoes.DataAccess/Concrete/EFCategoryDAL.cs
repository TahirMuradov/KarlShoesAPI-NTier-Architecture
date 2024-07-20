using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Core.Utilities.Results.Concrete.ErrorResults;
using KarlShoes.Core.Utilities.Results.Concrete.SuccessResults;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.DataAccess.Concrete.SQLServer;
using KarlShoes.Entites;
using KarlShoes.Entites.DTOs.CategoryDTOs;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace KarlShoes.DataAccess.Concrete
{
    public class EFCategoryDAL : ICategoryDAL
    {
        private readonly AppDBContext _context;

        public EFCategoryDAL(AppDBContext context)
        {
            _context = context;
        }

        public IDataResult<List<CategoryGetDTO>> GetAllCategory(string LangCode)
        {
            try
            {


                var category = _context.Categories
                    .Include(x => x.CategoryLanguages)

                    .Include(x => x.SubCategory)
                    .ThenInclude(x => x.subCategoryLaunguages)

                    .Select(x => new CategoryGetDTO
                    {
                        Id = x.Id,
                        IsFetured = x.IsFeatured,
                        CategoryName = x.CategoryLanguages.FirstOrDefault(x => x.LangCode == LangCode).CategoryName,
                        SubCategoryName = x.SubCategory.Select(y => y.subCategoryLaunguages.FirstOrDefault(x => x.LangCode.ToLower() == LangCode.ToLower()).SubcategoryName).ToList(),



                    })
                    .ToList();

                return new SuccessDataResult<List<CategoryGetDTO>>(data: category, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<CategoryGetDTO>>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }
        public async Task<IResult> AddCategoryAsync(CategoryAddDTO categoryAddDTO)
        {
            try
            {


                Category category = new Category()
                {
                    IsFeatured = categoryAddDTO.IsFeatured,
                    CreatedDate = DateTime.Now,

                };
                _context.Categories.Add(category);
                _context.SaveChanges();
                foreach (var item in categoryAddDTO.CategoryName)
                {
                    CategoryLanguage language = new CategoryLanguage()
                    {
                        CategoryName = item.Value,
                        LangCode = item.Key,
                        CategoryId = category.Id
                    };
                    _context.CategoryLanguages.Add(language);

                }
                _context.SaveChanges();

                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult DeleteCategory(string id)
        {
            try
            {


                var category = _context.Categories.FirstOrDefault(x => x.Id.ToString() == id);
                if (category == null) return new ErrorResult(statusCode: HttpStatusCode.NotFound);
                var categoryLaunge = _context.CategoryLanguages.Where(x => x.CategoryId == category.Id);
                _context.RemoveRange(categoryLaunge);
                var categoryProduct = _context.CategoryProducts.Where(x => x.CategoryId == category.Id);
                _context.RemoveRange(categoryProduct);
                var categorySubCategory = _context.subCategories.Where(x => x.CategoryId == category.Id);
                _context.subCategories.RemoveRange(categorySubCategory);
                _context.Categories.Remove(category);
                _context.SaveChanges();

                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<CategoryGetDTO> GetCategory(string id, string LangCode)
        {
            try
            {


                var categories = _context.Categories
                    .Include(x => x.CategoryLanguages)
                    .Include(x => x.SubCategory)
                    .ThenInclude(x => x.subCategoryLaunguages);


                var category = categories.FirstOrDefault(x => x.Id.ToString() == id);



                return category is not null ? new SuccessDataResult<CategoryGetDTO>(data: new CategoryGetDTO
                {
                    CategoryName = category.CategoryLanguages.FirstOrDefault(x => x.LangCode.ToLower() == LangCode.ToLower()).CategoryName,
                    Id = category.Id,
                    IsFetured = category.IsFeatured,
                    SubCategoryName = category.SubCategory?.Select(x => x.subCategoryLaunguages.FirstOrDefault(y => y.LangCode.ToLower() == LangCode.ToLower()).SubcategoryName).ToList()

                },

                statusCode: HttpStatusCode.OK)
                    :
                    new ErrorDataResult<CategoryGetDTO>(statusCode: HttpStatusCode.NotFound);



            }
            catch (Exception ex)
            {

                return new ErrorDataResult<CategoryGetDTO>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult UpdateCatgeory(CategoryUpdateDTO categoryUpdateDTO)
        {
            try
            {

                var category = _context.Categories.FirstOrDefault(x => x.Id.ToString().ToLower() == categoryUpdateDTO.CategoryId.ToLower());
                if (category is null) return new ErrorResult(statusCode: HttpStatusCode.NotFound);
                var categoryLaunguages = _context.CategoryLanguages.Where(x => x.CategoryId.ToString() == categoryUpdateDTO.CategoryId);
                category.IsFeatured = categoryUpdateDTO.IsFeatured;
                _context.Categories.Update(category);

                foreach (var categoryLaunguage in categoryLaunguages)
                {
                    if (categoryUpdateDTO.CategoryNames.ContainsKey(categoryLaunguage.LangCode))
                    {
                        categoryLaunguage.CategoryName = categoryUpdateDTO.CategoryNames[categoryLaunguage.LangCode];
                    }


                }
                _context.CategoryLanguages.UpdateRange(categoryLaunguages);




                _context.SaveChanges();


                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }
    }
}
