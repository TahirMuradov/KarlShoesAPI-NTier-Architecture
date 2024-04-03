using KarlShoes.Bussines.Abstarct;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.Entites.DTOs.PictureDTOs;

namespace KarlShoes.Bussines.Concrete
{
    public class PictureManager : IPictureServices
    {
        private readonly IPictureDAL _pictureDAL;

        public PictureManager(IPictureDAL pictureDAL)
        {
            _pictureDAL = pictureDAL;
        }

        public async Task<IResult> AddPictureAsync(PictureAddDTO pictureAddDTO)
        {
            return await _pictureDAL.AddPictureAsync(pictureAddDTO);
        }

        public IResult DeletePicture(string pictureId)
        {
            return _pictureDAL.DeletePicture(pictureId);
        }

        public IDataResult<List<PictureGetDTO>> GetAllPicture(string langCode)
        {
            return _pictureDAL.GetAllPicture(langCode);
        }

        public IDataResult<PictureGetDTO> GetPicture(string pictureId, string langCode)
        {
            return _pictureDAL.GetPicture(pictureId, langCode);
        }

        public IDataResult<List<PictureGetDTO>> GetProductPictures(string langCode, string productId)
        {
            return _pictureDAL.GetProductPictures(langCode:langCode, productId: productId);
        }
    }
}
