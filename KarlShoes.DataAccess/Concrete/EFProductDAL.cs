using KarlShoes.Core.Helper;
using KarlShoes.Core.Helper.FileHelper;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Core.Utilities.Results.Concrete.ErrorResults;
using KarlShoes.Core.Utilities.Results.Concrete.SuccessResults;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.DataAccess.Concrete.SQLServer;
using KarlShoes.Entites;
using KarlShoes.Entites.DTOs.ProductDTOs;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace KarlShoes.DataAccess.Concrete
{
    public class EFProductDAL : IProductDAL
    {
        public async Task<IResult> ProductAddAsync(ProductAddDTO productAddDTO)
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
                        if (chekedCategory is null) return new ErrorResult(message: "Category is NotFound", HttpStatusCode.NotFound);
                    }
                    if (productAddDTO.SubCategoryID is not null)
                    {

                        foreach (var subCategoryId in productAddDTO.SubCategoryID)
                        {

                            var chekedSubCategory = context.subCategories.FirstOrDefault(x => x.Id.ToString() == subCategoryId);
                            if (chekedSubCategory is null) return new ErrorResult(message: "SubCategory Is NotFound", HttpStatusCode.NotFound);

                        }
                    }




                    Product product = new Product()
                    {

                        Color = productAddDTO.color,
                        CreatedDate = DateTime.Now,
                        isFeatured = productAddDTO.isFeatured,
                        Price = productAddDTO.Price,
                        DisCount = productAddDTO.DisCount,
                        ProductCode = productAddDTO.ProductCode


                    };

                    context.Products.Add(product);
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
                                Product = product,
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

                    foreach (var size in productAddDTO.SizeAndCount)
                    {
                        var checekSize = context.Sizes.FirstOrDefault(x => x.NumberSize == size.Key);
                        if (checekSize is null)
                        {
                            Size size1 = new Size()
                            {
                                CreatedDate = DateTime.Now,
                                NumberSize = size.Key,

                            };
                            context.Sizes.Add(size1);
                            context.SaveChanges();
                            ProductSize productSize = new ProductSize() 
                            { 
                                
                                ProductId = product.Id,
                                SizeId = size1.Id,
                                SizeStockCount=size.Value
                            };
                            context.ProductSizes.Add(productSize);
                            context.SaveChanges();
                        }
                        else
                        {
                            ProductSize productSize = new ProductSize()
                            {

                                ProductId = product.Id,
                                SizeId = checekSize.Id,
                                SizeStockCount=size.Value
                            };
                            context.ProductSizes.Add(productSize);
                            context.SaveChanges();
                        }


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
            try
            {
                using (var context = new AppDBContext())
                {
                    var product = context.Products
                     .Include(p => p.productLanguages)
                     .Include(p => p.Pictures)
                     .Include(p => p.ProductCategories)
                         .ThenInclude(pc => pc.Category.CategoryLanguages)
                     .Include(p => p.SubCategories)
                         .ThenInclude(sp => sp.SubCategory.subCategoryLaunguages)
                     .FirstOrDefault(p => p.Id.ToString() == ProductId);

                    return product is null ? new ErrorDataResult<ProductGetDTO>(message: "Product Is NotFound", statusCode: HttpStatusCode.NotFound) :
                     new SuccessDataResult<ProductGetDTO>(data: new ProductGetDTO
                     {
                         Id = product.Id,
                         Category = product.ProductCategories.Select(pc => new KeyValuePair<string, string>(pc.Category.Id.ToString(), pc.Category.CategoryLanguages.FirstOrDefault(x => x.LangCode == LangCode).CategoryName)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                         SubCategory = product.SubCategories?.Select(sp => new KeyValuePair<string, string>(sp.SubCategory.Id.ToString(), sp.SubCategory.subCategoryLaunguages.FirstOrDefault(x => x.LangCode == LangCode).SubcategoryName)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                         SizeAndCount = product.ProductSizes?.Select(ps => new KeyValuePair<int, int>(ps.Size.NumberSize, ps.SizeStockCount)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                         Description = product.productLanguages.FirstOrDefault(x => x.LangCode == LangCode).Description,
                         Name = product.productLanguages.FirstOrDefault(x => x.LangCode == LangCode).ProductName,
                         PictureUrls = product.Pictures.Select(x => x.url).ToList(),
                         DisCount = product.DisCount,
                         IsFeatured = product.isFeatured,
                         Price = product.Price,
                         color = product.Color

                     }, statusCode: HttpStatusCode.OK);

                }

            }
            catch (Exception ex)
            {

                return new ErrorDataResult<ProductGetDTO>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }

        }

        public IDataResult<List<ProductGetDTO>> ProductGetAll(string LangCode)
        {
            try
            {
                using var context = new AppDBContext();

                var products = context.Products
                    .Include(p => p.productLanguages)
                    .Include(p => p.Pictures)
                    .Include(p => p.ProductCategories)
                        .ThenInclude(pc => pc.Category.CategoryLanguages)
                    .Include(p => p.SubCategories)
                        .ThenInclude(sp => sp.SubCategory.subCategoryLaunguages)
                        .Include(ps=>ps.ProductSizes)
                        .ThenInclude(s=>s.Size)
                    .ToList() // Veritabanı sorgusunu çalıştır ve sonucu belleğe al
                    .Select(product => new ProductGetDTO
                    {
                        Id = product.Id,
                        color = product.Color,
                        DisCount = product.DisCount,
                        IsFeatured = product.isFeatured,
                        Price = product.Price,
                        Description = product.productLanguages.FirstOrDefault(x => x.LangCode == LangCode)?.Description,
                        Name = product.productLanguages.FirstOrDefault(x => x.LangCode == LangCode)?.ProductName,
                        PictureUrls = product.Pictures?.Select(x => x.url).ToList(),
                        Category = product.ProductCategories
                            .Select(pc => new KeyValuePair<string, string>(
                                pc.CategoryId.ToString(),
                                pc.Category.CategoryLanguages.FirstOrDefault(x => x.LangCode == LangCode)?.CategoryName ?? ""))
                            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                        SubCategory = product.SubCategories?.Select(sbp => new KeyValuePair<string, string>(sbp.SubCategoryId.ToString(), sbp.SubCategory.subCategoryLaunguages.FirstOrDefault(x => x.LangCode == LangCode).SubcategoryName)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                        SizeAndCount = product.ProductSizes?.Select(ps => new KeyValuePair<int, int>(ps.Size?.NumberSize ?? 0, ps.SizeStockCount))
                            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value),
                    }).ToList();

                return new SuccessDataResult<List<ProductGetDTO>>(data: products, statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<ProductGetDTO>>(message: ex.Message, HttpStatusCode.BadRequest);
            }
        }

        public IResult ProductRemove(string ProductID)
        {
            try
            {
                using (var context = new AppDBContext())
                {
                    var product = context.Products.FirstOrDefault(x => x.Id.ToString() == ProductID);
                    if (product == null) return new ErrorResult(message: "Product is NotFound", statusCode: HttpStatusCode.NotFound);

                    var ProductLaung = context.ProductLanguages.Where(x => x.ProductId == product.Id);
                    context.ProductLanguages.RemoveRange(ProductLaung);
                    var ProductSize = context.ProductSizes.Where(x => x.ProductId == product.Id);
                    context.ProductSizes.RemoveRange(ProductSize);
                    var ProductCategory = context.CategoryProducts.Where(x => x.ProductId == product.Id);
                    context.CategoryProducts.RemoveRange(ProductCategory);
                    var ProductSubCategory = context.SubCategoriesProduct.Where(x => x.ProductId == product.Id);
                    context.SubCategoriesProduct.RemoveRange(ProductSubCategory);
                    context.Products.Remove(product);
                    context.SaveChanges();
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

        public async Task<IResult> ProductUpdateAsync(ProductUpdateDTO productUpdateDTO)
        {
            try
            {
                using (var context = new AppDBContext())
                {
                    var product = context.Products
                  .Include(p => p.productLanguages)
                  .Include(p => p.Pictures)
                  .Include(p => p.ProductCategories)
                      .ThenInclude(pc => pc.Category.CategoryLanguages)
                  .Include(p => p.SubCategories)
                      .ThenInclude(sp => sp.SubCategory.subCategoryLaunguages)
                  .FirstOrDefault(p => p.Id.ToString() == productUpdateDTO.ProductId);

                    if (product is null)
                        return new ErrorResult(message: "Product is NotFound", statusCode: HttpStatusCode.NotFound);





                    foreach (var kvp in productUpdateDTO.LangCodeAndProductName)
                    {
                        var languageProduct = context.ProductLanguages.FirstOrDefault(pl => pl.ProductId == product.Id && pl.LangCode == kvp.Key);
                        if (languageProduct != null)
                        {
                            languageProduct.ProductName = kvp.Value is not null ? kvp.Value : languageProduct.ProductName;
                         }

                        context.ProductLanguages.Update(languageProduct);

                    }
                    foreach (var des in productUpdateDTO.LangCodeAndProductDescription)
                    {
                        var languageProduct = context.ProductLanguages.FirstOrDefault(pl => pl.ProductId == product.Id && pl.LangCode == des.Key);
                        if (languageProduct != null)
                        {
                            languageProduct.Description = des.Value;

                        }

                        context.ProductLanguages.Update(languageProduct);
                    }
              

                    foreach (var kvp in productUpdateDTO.SizeAndCount)
                    {
                        var Productsize = context.ProductSizes.Include(x => x.Size).FirstOrDefault(x => x.ProductId == product.Id && int.Parse(kvp.Key) == x.Size.NumberSize);
                        if (Productsize is not null)
                        {

                            Productsize.SizeStockCount = int.Parse(kvp.Value);
                            if (Productsize.SizeStockCount==0)
                            {
                                context.ProductSizes.Remove(Productsize);
                            }
                            else
                            {

                            context.ProductSizes.Update(Productsize);
                            }
                        }
                        else
                        {
                            var size = context.Sizes.FirstOrDefault(x => x.NumberSize == int.Parse(kvp.Key));
                            if (size is null ||int.Parse( kvp.Value)==0) continue; ;
                            ProductSize productSize = new ProductSize()
                            {
                                ProductId = product.Id,
                                SizeId = size.Id,
                                SizeStockCount = int.Parse(kvp.Value),
                            };
                            context.ProductSizes.Add(productSize);
                            context.SaveChanges();
                        }
                        context.ProductSizes.RemoveRange(context.ProductSizes.Where(x => x.ProductId == product.Id && x.SizeStockCount == 0));


                    }


                    context.CategoryProducts.RemoveRange(context.CategoryProducts.Where(x => x.ProductId == product.Id));
                   foreach(var categoryId in productUpdateDTO.CatgeoryId)
                    {
                        var checkedCategory = context.Categories.FirstOrDefault(x => x.Id.ToString()== categoryId);
                        if (checkedCategory is not null)
                        {
                          
                          
                                
                            CategoryProduct categoryProduct = new CategoryProduct()
                            {
                                CategoryId=Guid.Parse( categoryId),
                                ProductId=product.Id,
                            };
                                context.CategoryProducts.Add(categoryProduct);
                         

                        }

                    }


                    context.SubCategoriesProduct.RemoveRange(context.SubCategoriesProduct.Where(x => x.ProductId == product.Id));
                    foreach (var subCategoryId in productUpdateDTO.SubCategoryID)
                    {
                        var checkedSubCategory = context.subCategories.FirstOrDefault(x => x.Id.ToString() == subCategoryId);
                        if (checkedSubCategory is not null)
                        {
                          

                                SubCategoryProduct SubcategoryProduct = new SubCategoryProduct()
                                {
                                    SubCategoryId = Guid.Parse(subCategoryId),
                                    ProductId = product.Id,
                                };
                                context.SubCategoriesProduct.Add(SubcategoryProduct);
                            

                        }

                    }
                 
                    if (productUpdateDTO.DisCount != 0)
                        product.DisCount = productUpdateDTO.DisCount ?? product.DisCount;
                    if (!string.IsNullOrEmpty(productUpdateDTO.color))
                        product.Color = productUpdateDTO.color;
                    if (productUpdateDTO.Price != 0)
                        product.Price = productUpdateDTO.Price ?? product.Price;

                    product.isFeatured = productUpdateDTO.isFeatured ?? product.isFeatured;


                    context.SaveChanges();


                }
                return new SuccessResult(statusCode: HttpStatusCode.OK);

            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.OK);
            }
        }
    }
}
