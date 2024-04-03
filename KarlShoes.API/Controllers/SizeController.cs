using KarlShoes.Bussines.Abstarct;
using KarlShoes.Bussines.FluentValidation.SIizeDTOValidator;
using KarlShoes.Entites.DTOs.SizeDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarlShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SizeController : ControllerBase
    {
        private readonly ISizeServices _sizeServices;

        public SizeController(ISizeServices sizeServices)
        {
            _sizeServices = sizeServices;
        }

        [HttpGet("[action]")]
        public IActionResult GetSizeAll()
        {
            var result = _sizeServices.SizeAllGet();


            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetSize(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id Is Null or Empty");
            var result = _sizeServices.SizeGet(id);

            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpDelete("[action]")]
        public IActionResult RemoveSize(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id Is Null or Empty");
            var result = _sizeServices.SizeRemove(id);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPost("[action]")]
        public IActionResult AddSize(SizeAddDTO sizeAddDTO)
        {
            var validation = new SizeAddDTOValidator();
            var ResultValidator = validation.Validate(sizeAddDTO);
            if (!ResultValidator.IsValid) return BadRequest(ResultValidator.Errors);
            var result = _sizeServices.SizeAdd(sizeAddDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }
        [HttpPut("[action]")]
        public IActionResult UpdateSize(SizeUpdateDTO sizeUpdateDTO)
        {
         
            var validator=new SizeUpdateDTOValidator();
            var ValidatorResult=validator.Validate(sizeUpdateDTO);
            if (!ValidatorResult.IsValid) return BadRequest(ValidatorResult.Errors);           
            var result= _sizeServices.SizeUpdate(sizeUpdateDTO);
            return result.IsSuccess? Ok(result):BadRequest(result);
        }

    }
}
