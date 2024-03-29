using KarlShoes.Core.Helper;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Core.Utilities.Results.Concrete.ErrorResults;
using KarlShoes.Core.Utilities.Results.Concrete.SuccessResults;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.DataAccess.Concrete.SQLServer;
using KarlShoes.Entites;
using KarlShoes.Entites.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace KarlShoes.DataAccess.Concrete
{
    public class EFProductDAL : IProductDAL
    {
        public async Task<IResult> ProductAddAsync(ProductAddDTO productAddDTO, List<IFormFile> formFiles)
        {

            try
            {
                if (productAddDTO.LangCodeAndProductDescription.Count != productAddDTO.LangCodeAndProductName.Count)
                    return new ErrorResult(message: "The product name or product description did not appear correctly", statusCode: HttpStatusCode.BadRequest);
                using (var context = new AppDBContext())
                {
                    foreach (var categoryId in productAddDTO.CatgeoryId)
                    {
                        var chekedCategory = context.Categories.FirstOrDefault(x => x.Id.ToString() == categoryId);
                        if (chekedCategory is null) return new ErrorResult(message: "There is no category with this id", HttpStatusCode.BadRequest);
                    }
                    if (productAddDTO.SubCategoryID is not null)
                    {

                        foreach (var subCategoryId in productAddDTO.SubCategoryID)
                        {

                            var chekedSubCategory = context.subCategories.FirstOrDefault(x => x.Id.ToString() == subCategoryId);
                            if (chekedSubCategory is null) return new ErrorResult(message: "There is no subcategory with this id", HttpStatusCode.BadRequest);

                        }
                    }




                    Product product = new Product()
                    {
                        Color = productAddDTO.color,
                        CreatedDate = DateTime.Now,
                        isFeatured = productAddDTO.isFeatured,
                        Price = productAddDTO.Price,
                        DisCount = productAddDTO.DisCount,


                    };
                    context.Products.Add(product);
                    context.SaveChanges();

                    foreach (IFormFile file in formFiles)
                    {
                        var url = await FileHelper.SaveFileAsync(file, wwwrootGetPath.GetwwwrootPath);
                        Picture picture = new Picture()
                        {
                            ProductId = product.Id,
                            url = url
                        };
                        context.Pictures.Add(picture);
                    }
                    context.SaveChanges();
                    foreach (var category in productAddDTO.CatgeoryId)
                    {

                        CategoryProduct categoryProduct = new CategoryProduct()
                        {
                            CategoryId = Guid.Parse(category),
                            ProductId = product.Id

                        };
                        context.CategoryProducts.Add(categoryProduct);
                    }
                    context.SaveChanges();
                    if (productAddDTO.SubCategoryID is not null)
                    {

                        foreach (var subCategory in productAddDTO.SubCategoryID)
                        {
                            SubCategoryProduct subCategoryProduct = new SubCategoryProduct()
                            {
                                ProductId = product.Id,
                                SubCategoryId = Guid.Parse(subCategory)
                            };
                            context.SubCategoriesProduct.Add(subCategoryProduct);
                        }
                        context.SaveChanges();
                    }

                    foreach (var productName in productAddDTO.LangCodeAndProductName)
                    {

                        ProductLanguage productLanguage = new ProductLanguage()
                        {
                            ProductName = productName.Value,
                            Description = productAddDTO.LangCodeAndProductDescription[productName.Key],
                            LangCode = productName.Key,
                            ProductId = product.Id,
                            SeoUrl = SeoUrlHelper.ReplaceInvalidChars(productName.Value)

                        };
                        context.ProductLanguages.Add(productLanguage);
                    }
                    context.SaveChanges();




                }
                return new SuccessResult(statusCode: System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IDataResult<ProductGetDTO> ProductGet(string ProductId, string LangCode)
        {
            using (var context=new AppDBContext())
            {
               var product = context.Products
                .Include(p => p.productLanguages)
                .Include(p => p.Pictures)
                .Include(p => p.ProductCategories)
                    .ThenInclude(pc => pc.Category.CategoryLanguages)
                .Include(p => p.SubCategories)
                    .ThenInclude(sp => sp.SubCategory.subCategoryLaunguages)
                .FirstOrDefault(p => p.Id.ToString() == ProductId);
                 
                   

            }
            return new SuccessDataResult<ProductGetDTO>(statusCode: HttpStatusCode.OK);
        }

        public IDataResult<List<ProductGetDTO>> ProductGetAll(string LangCode)
        {
            throw new NotImplementedException();
        }

        public IResult ProductRemove(string ProductID)
        {
            try
            {
                using (var context = new AppDBContext())
                {
                    var product = context.Products.FirstOrDefault(x => x.Id.ToString() == ProductID);
                    if (product == null) return new ErrorResult(message: "There is no product with this id", statusCode: HttpStatusCode.BadRequest);
                  
                    var ProductLaung = context.ProductLanguages.Where(x => x.ProductId == product.Id);
                    context.ProductLanguages.RemoveRange(ProductLaung);
                    var ProductSize = context.ProductSizes.Where(x => x.ProductId == product.Id);
                    context.ProductSizes.RemoveRange(ProductSize);
                    var ProductCategory = context.CategoryProducts.Where(x => x.ProductId == product.Id);
                    context.CategoryProducts.RemoveRange(ProductCategory);
                    var ProductSubCategory = context.SubCategoriesProduct.Where(x => x.ProductId == product.Id);
                    context.SubCategoriesProduct.RemoveRange(ProductSubCategory);
                    var pictures = context.Pictures.Where(x => x.ProductId == product.Id);
                    foreach (var picture in pictures)
                    {

                        FileHelper.RemoveFile(picture.url);

                    }
                    context.Pictures.RemoveRange(pictures);
                    context.SaveChanges();
                    return new SuccessResult(statusCode: HttpStatusCode.OK);

                }
            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult ProductUpdate(ProductUpdateDTO productUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
