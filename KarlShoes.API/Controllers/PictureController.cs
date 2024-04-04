using KarlShoes.Bussines.Abstarct;
using KarlShoes.Bussines.FluentValidation.PictureDTOValidator;
using KarlShoes.Entites;
using KarlShoes.Entites.DTOs.PictureDTOs;
using KarlShoes.Entites.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarlShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PictureController : ControllerBase
    {
        private readonly IPictureService _pictureServices;

        public PictureController(IPictureService pictureServices)
        {
            _pictureServices = pictureServices;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> PictureAdd([FromForm] PictureAddDTO pictureAddDTO)
        {
            var validator = new PictureAddDTOValidator();
            var ValidatorResult=validator.Validate(pictureAddDTO);
            if (!ValidatorResult.IsValid) return BadRequest(ValidatorResult.Errors);
            var result= await _pictureServices.AddPictureAsync(pictureAddDTO);
             return result.IsSuccess? Ok(result):BadRequest(result);
        }
        [HttpDelete("[action]")]
        public IActionResult PictureDelete(string PictureId)
        {
            if (string.IsNullOrEmpty(PictureId)) return BadRequest("Picture Id null or empty!");
            var result=_pictureServices.DeletePicture(PictureId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult PictureGet (string PictureId,string langCode)
        {
            if (string.IsNullOrEmpty(PictureId)||string.IsNullOrEmpty(langCode)) return BadRequest("Picture Id or langcode null or empty!");
            var result = _pictureServices.GetPicture(PictureId,langCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);


        }
        [HttpGet("[action]")]
        public IActionResult PictureGetAll(string LangCode)
        {
            if ( string.IsNullOrEmpty(LangCode)) return BadRequest("LangCode null or empty!");
            var result = _pictureServices.GetAllPicture(LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetPictureProduct(string ProductId,string langCode)
        {
            if (string.IsNullOrEmpty(ProductId) || string.IsNullOrEmpty(langCode)) return BadRequest("Product Id or langCode null or empty!");

            var result=_pictureServices.GetProductPictures(productId: ProductId,langCode: langCode);
            return result.IsSuccess?Ok(result):BadRequest(result);
        }
    }
}
