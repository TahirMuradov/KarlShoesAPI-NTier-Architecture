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
        private readonly AppDBContext _context;

        public EFSubCategoryDAL(AppDBContext context)
        {
            _context = context;
        }

        public IResult SubCategoryAdd(SubCategoryAddDTO subCategoryAddDTO)
        {
            try
            {
               


                    var checekCategory = _context.Categories.Include(x => x.SubCategory).ThenInclude(x => x.subCategoryLaunguages).FirstOrDefault(x => x.Id == subCategoryAddDTO.CategoryId);
                    if (checekCategory == null) return new ErrorResult(message: "Category Is Notfound", statusCode: HttpStatusCode.NotFound);
                    var checekSubCategory = checekCategory.SubCategory.Select(x => x.subCategoryLaunguages.FirstOrDefault(y => subCategoryAddDTO.SubCategoryName.ContainsKey(y.LangCode) && y.SubcategoryName == subCategoryAddDTO.SubCategoryName[y.LangCode])); ;
                    if (checekSubCategory == null) return new ErrorResult(message: "SubCategory Is Notfound!", statusCode: HttpStatusCode.NotFound);
                    SubCategory subCategory = new SubCategory()
                    {
                        CategoryId = checekCategory.Id,
                        

                    };
                    _context.subCategories.Add(subCategory);
                    _context.SaveChanges();
                    foreach (var subCategoryLangCode in subCategoryAddDTO.SubCategoryName)
                    {
                        SubCategoryLaunguage subCategoryLaunguage = new SubCategoryLaunguage()
                        {
                            LangCode=subCategoryLangCode.Key,
                            SubcategoryName=subCategoryLangCode.Value,
                            SubCategoryId=subCategory.Id,
                            SubCategory=subCategory
                        };
                        _context.subCategoryLaunguages.Add(subCategoryLaunguage);

                    }
                        _context.SaveChanges();


                
                    return new SuccessResult(statusCode: HttpStatusCode.OK);
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
                

                return new SuccessDataResult<List<SubCategoryGetDTO>>(
                    data:
                    _context.subCategories
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
               
                    var checekdData = _context.subCategories.FirstOrDefault(x => x.Id.ToString() == id);
                    if (checekdData == null) return new ErrorResult(message:"SubCategory Is Notfound!", statusCode: HttpStatusCode.NotFound);



                    _context.subCategoryLaunguages.RemoveRange(_context.subCategoryLaunguages.Where(x => x.SubCategoryId == checekdData.Id));
                    _context.SubCategoriesProduct.RemoveRange(_context.SubCategoriesProduct.Where(x => x.SubCategoryId == checekdData.Id));
                    _context.subCategories.Remove(checekdData);
                    _context.SaveChanges();

               
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

                

                var checekData = _context.subCategories
                    .Include(x=>x.subCategoryLaunguages)
                   
                    .Include(x => x.Category)
                    .ThenInclude(x => x.CategoryLanguages)
                    .FirstOrDefault(x => x.Id.ToString() == id.ToLower());
                if (checekData is null) return new ErrorDataResult<SubCategoryGetDTO>(message: "SubCategory Is Notfound!", statusCode: HttpStatusCode.NotFound);
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

              
                    var checekdData = _context.subCategoryLaunguages.Include(x=>x.SubCategory).Where(x => x.SubCategoryId.ToString() == subCategoryUpdateDTO.SubCategoryId).ToList();
                    if (checekdData is null || checekdData.Count == 0) return new ErrorResult(message: "SubCategory Is Notfound!", statusCode: HttpStatusCode.NotFound);
                    if (!string.IsNullOrEmpty(subCategoryUpdateDTO.NewCategoryId))
                    {
                        var checkedCategory = _context.Categories.FirstOrDefault(x => x.Id.ToString() == subCategoryUpdateDTO.NewCategoryId);
                        if (checkedCategory is not null)
                        {
                            var subCategory = _context.subCategories.FirstOrDefault(x => x.Id ==Guid.Parse( subCategoryUpdateDTO.SubCategoryId));
                            subCategory.CategoryId = checkedCategory.Id;
                            _context.subCategories.Update(subCategory);
                          
                        }
                    }
                  
                    if (subCategoryUpdateDTO.SubCategoriesName.Count > 0)
                    {
                        foreach (var category in subCategoryUpdateDTO.SubCategoriesName)
                        {
                            var data = checekdData.FirstOrDefault(x => x.LangCode == category.Key);
                            if (data is null)

                            {
                                continue;
                            }

                            data.SubcategoryName = category.Value;


                            //context.subCategoryLaunguages.Update(data);
                        }
                    
                    }

                    _context.subCategoryLaunguages.UpdateRange(checekdData);
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
