using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Core.Utilities.Results.Concrete.ErrorResults;
using KarlShoes.Core.Utilities.Results.Concrete.SuccessResults;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.DataAccess.Concrete.SQLServer;
using KarlShoes.Entites;
using KarlShoes.Entites.DTOs.SubCategoryDTOs;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace KarlShoes.DataAccess.Concrete
{
    public class EFSubCategoryDAL : ISubCategoryDAL
    {
        public IResult SubCategoryAdd(SubCategoryAddDTO subCategoryAddDTO)
        {
            try
            {
                using (var context = new AppDBContext())
                {


                    var checekCategory = context.Categories.Include(x => x.SubCategory).ThenInclude(x => x.subCategoryLaunguages).FirstOrDefault(x => x.Id == subCategoryAddDTO.CategoryId);
                    if (checekCategory == null) return new ErrorResult(message: "Category Is Notfound", statusCode: HttpStatusCode.BadRequest);
                    var checekSubCategory = checekCategory.SubCategory.Select(x => x.subCategoryLaunguages.FirstOrDefault(y => subCategoryAddDTO.SubCategoryName.ContainsKey(y.LangCode) && y.SubcategoryName == subCategoryAddDTO.SubCategoryName[y.LangCode])); ;
                    if (checekSubCategory == null) return new ErrorResult(message: "Data Is not empty!", statusCode: HttpStatusCode.BadRequest);
                    SubCategory subCategory = new SubCategory()
                    {
                        CategoryId = checekCategory.Id,
                        

                    };
                    context.subCategories.Add(subCategory);
                    context.SaveChanges();
                    foreach (var subCategoryLangCode in subCategoryAddDTO.SubCategoryName)
                    {
                        SubCategoryLaunguage subCategoryLaunguage = new SubCategoryLaunguage()
                        {
                            LangCode=subCategoryLangCode.Key,
                            SubcategoryName=subCategoryLangCode.Value,
                            SubCategoryId=subCategory.Id,
                            SubCategory=subCategory
                        };
                        context.subCategoryLaunguages.Add(subCategoryLaunguage);

                    }
                        context.SaveChanges();


                    return new SuccessResult(statusCode: HttpStatusCode.OK);
                }
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<List<SubCategoryGetDTO>> SubCategoryAllGet(string LangCode)
        {
            try
            {
                using var context = new AppDBContext();

                return new SuccessDataResult<List<SubCategoryGetDTO>>(
                    data:
                    context.subCategories
                    .Include(y => y.Category)
                    .ThenInclude(y => y.CategoryLanguages)
                    .Include(z => z.subCategoryLaunguages)
                    .Select(x => new SubCategoryGetDTO
                    {
                        Id = x.Id.ToString(),
                        CategoryName = x.Category.CategoryLanguages.FirstOrDefault(x => x.LangCode == LangCode).CategoryName,
                        SubCategoryName = x.subCategoryLaunguages.FirstOrDefault(x => x.LangCode.ToLower() == LangCode.ToLower()).SubcategoryName

                    }).ToList()
                    ,
                    statusCode: HttpStatusCode.OK
                    );


            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<SubCategoryGetDTO>>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult SubCategoryDelete(string id)
        {
            try
            {
                using (var context = new AppDBContext())
                {
                    var checekdData = context.subCategories.FirstOrDefault(x => x.Id.ToString() == id);
                    if (checekdData == null) return new ErrorResult(statusCode: HttpStatusCode.BadRequest);
                    context.Remove(checekdData);
                    context.SaveChanges();

                }
                return new SuccessResult(statusCode: HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return new ErrorResult(statusCode: HttpStatusCode.BadRequest, message: ex.Message);
            }
        }

        public IDataResult<SubCategoryGetDTO> SubCategoryGet(string id, string LangCode)
        {
            try
            {

                using var context = new AppDBContext();

                var checekData = context.subCategories
                    .Include(x => x.Category)
                    .ThenInclude(x => x.CategoryLanguages)
                    .FirstOrDefault(x => x.Id.ToString() == id.ToLower());
                if (checekData is null) return new ErrorDataResult<SubCategoryGetDTO>(message: "Data is empty", statusCode: HttpStatusCode.BadRequest);
                return new SuccessDataResult<SubCategoryGetDTO>(
                    data:
                    new SubCategoryGetDTO
                    {
                        SubCategoryName = checekData.subCategoryLaunguages is not null ? checekData.subCategoryLaunguages.FirstOrDefault(x => x.LangCode.ToLower() == LangCode.ToLower()).SubcategoryName : null,
                        Id = checekData.Id.ToString(),
                        CategoryName = checekData.Category.CategoryLanguages.FirstOrDefault(x => x.LangCode == LangCode).CategoryName
                    }
                    ,
                    statusCode: HttpStatusCode.OK

                    );

            }
            catch (Exception ex)
            {

                return new ErrorDataResult<SubCategoryGetDTO>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult SubCategoryUpdate(SubCategoryUpdateDTO subCategoryUpdateDTO)
        {
            try
            {

                using (var context = new AppDBContext())
                {
                    var checekdData = context.subCategories.Where(x => x.Id.ToString() == subCategoryUpdateDTO.SubCategoryId).ToList();
                    if (checekdData is null || checekdData.Count == 0) return new ErrorResult(message: "There is no data matching the provided ID.", statusCode: HttpStatusCode.BadRequest);
                    if (!string.IsNullOrEmpty(subCategoryUpdateDTO.NewCategoryId))
                    {
                        var checkedCategory = context.Categories.FirstOrDefault(x => x.Id.ToString() == subCategoryUpdateDTO.NewCategoryId);
                        if (checkedCategory is not null)
                        {

                            checekdData.ForEach(x => { x.CategoryId = Guid.Parse(subCategoryUpdateDTO.NewCategoryId.ToLower()); });
                        }
                    }
                    if (subCategoryUpdateDTO.SubCategoriesName.Count > 0)
                    {
                        foreach (var category in subCategoryUpdateDTO.SubCategoriesName)
                        {
                            var data = checekdData.FirstOrDefault(x => x.subCategoryLaunguages.Select(x => x.LangCode).ToString() == category.Key);
                            if (data is null)

                            {
                                SubCategory subCategory = new SubCategory()
                                {
                                    CategoryId = checekdData.FirstOrDefault().CategoryId,


                                };
                                context.subCategories.Add(subCategory);
                            }
                            else
                            {

                                data.subCategoryLaunguages.FirstOrDefault(z => z.LangCode == category.Key).SubcategoryName = category.Value;
                            }
                        }
                    }

                    context.UpdateRange(checekdData);
                    context.SaveChanges();

                }
                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }
    }
}
