using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Entites.DTOs.PictureDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Abstarct
{
    public interface IPictureService
    {
        public IResult DeletePicture(string pictureId);
        public Task<IResult> AddPictureAsync(PictureAddDTO pictureAddDTO);
        public IDataResult<PictureGetDTO> GetPicture(string pictureId, string langCode);
        public IDataResult<List<PictureGetDTO>> GetAllPicture(string langCode);
        public IDataResult<List<PictureGetDTO>> GetProductPictures(string langCode, string productId);
    }
}
