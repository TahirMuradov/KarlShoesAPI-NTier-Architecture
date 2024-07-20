using KarlShoes.Core.Helper;
using KarlShoes.Core.Helper.FileHelper;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Core.Utilities.Results.Concrete.ErrorResults;
using KarlShoes.Core.Utilities.Results.Concrete.SuccessResults;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.DataAccess.Concrete.SQLServer;
using KarlShoes.Entites;
using KarlShoes.Entites.DTOs.PictureDTOs;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace KarlShoes.DataAccess.Concrete
{
    public class EFpictureDAL : IPictureDAL
    {
        private readonly AppDBContext _context;

        public EFpictureDAL(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IResult> AddPictureAsync(PictureAddDTO pictureAddDTO)
        {
            try
            {

                var product = _context.Products.FirstOrDefault(x => x.Id.ToString() == pictureAddDTO.ProductId);
                if (product == null) return new ErrorResult(message: "Product Is NotFound", statusCode: HttpStatusCode.NotFound);
                foreach (var picture in pictureAddDTO.FormFiles)
                {
                    string url = await FileHelper.SaveFileAsync(picture, wwwrootGetPath.GetwwwrootPath);
                    Picture picture1 = new Picture()
                    {
                        ProductId = product.Id,
                        url = url
                    };
                    _context.Pictures.Add(picture1);
                }
                _context.SaveChanges();


                return new SuccessResult(statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IResult DeletePicture(string pictureId)
        {
            try
            {

                var picture = _context.Pictures.FirstOrDefault(x => x.Id.ToString() == pictureId);
                if (picture is null) return new ErrorResult(message: "Picture Is NotFound", statusCode: HttpStatusCode.BadRequest);
                bool result = FileHelper.RemoveFile(picture.url);
                if (result)
                {
                    _context.Pictures.Remove(picture);
                    _context.SaveChanges();
                    return new SuccessResult(statusCode: HttpStatusCode.OK);
                }
                else
                {
                    return new ErrorResult(message: "UPS!", statusCode: HttpStatusCode.BadRequest);
                }





            }
            catch (Exception ex)
            {

                return new ErrorResult(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<List<PictureGetDTO>> GetAllPicture(string langCode)
        {
            try
            {

                return new SuccessDataResult<List<PictureGetDTO>>(data:
                    _context.Pictures.Include(x => x.Product).ThenInclude(x => x.productLanguages).Select(x => new PictureGetDTO
                    {
                        PictureUrl = x.url,
                        PictureId = x.Id.ToString(),
                        ProductId = x.ProductId.ToString(),
                        ProductName = x.Product.productLanguages.FirstOrDefault(y => y.LangCode == langCode).ProductName


                    }).ToList()


                    , statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<PictureGetDTO>>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<PictureGetDTO> GetPicture(string pictureId, string langCode)
        {
            try
            {

                return new SuccessDataResult<PictureGetDTO>(data:
                    _context.Pictures.Include(x => x.Product).ThenInclude(x => x.productLanguages).Select(x => new PictureGetDTO
                    {
                        PictureUrl = x.url,
                        PictureId = x.Id.ToString(),
                        ProductId = x.ProductId.ToString(),
                        ProductName = x.Product.productLanguages.FirstOrDefault(y => y.LangCode == langCode).ProductName


                    }).FirstOrDefault(x => x.PictureId == pictureId)


                    , statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<PictureGetDTO>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }

        public IDataResult<List<PictureGetDTO>> GetProductPictures(string langCode, string productId)
        {
            try
            {

                return new SuccessDataResult<List<PictureGetDTO>>(data:
                    _context.Pictures.Include(x => x.Product).ThenInclude(x => x.productLanguages).Select(x => new PictureGetDTO
                    {
                        PictureUrl = x.url,
                        PictureId = x.Id.ToString(),
                        ProductId = x.ProductId.ToString(),
                        ProductName = x.Product.productLanguages.FirstOrDefault(y => y.LangCode == langCode).ProductName


                    }).Where(x => x.ProductId == productId).ToList()


                    , statusCode: HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                return new ErrorDataResult<List<PictureGetDTO>>(message: ex.Message, statusCode: HttpStatusCode.BadRequest);
            }
        }
    }
}
