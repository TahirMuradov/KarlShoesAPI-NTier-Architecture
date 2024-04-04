using Entities.DTOs.ShippingMethods;
using KarlShoes.Bussines.Abstarct;
using KarlShoes.Bussines.FluentValidation.ShippingMethodDTOValidator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Prng;

namespace KarlShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippingMethodController : ControllerBase
    {
        private readonly IShippingMethodService _shippingMethodService;

        public ShippingMethodController(IShippingMethodService shippingMethodService)
        {
            _shippingMethodService = shippingMethodService;
        }
        [HttpPost("[action]")]
        public IActionResult ShippingMethodAdd(AddShippingMethodDTO addShippingMethodDTO)
        {
            var validator=new ShippingMethodAddValidator(); 
            var validatorResult=validator.Validate(addShippingMethodDTO);
            if (!validatorResult.IsValid) return BadRequest(validatorResult.Errors);
        var result=_shippingMethodService.AddShipping(addShippingMethodDTO);  
            return result.IsSuccess? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult GetShippingMethod(string Id,string langCode)
        {
            if (string.IsNullOrEmpty(Id)||string.IsNullOrEmpty(langCode)) return BadRequest("Id or LangCode is Null or Empty!");
            var result=_shippingMethodService.GetShipping(Id,langCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);

        }
        [HttpGet("[action]")]
        public IActionResult GetAllShippingMethod(string LangCode) 
        {

            if ( string.IsNullOrEmpty(LangCode)) return BadRequest("LangCode is Null or Empty!");

            var result = _shippingMethodService.GetShippingAll(LangCode);
            return result.IsSuccess? Ok(result) : BadRequest(result);

        }
        [HttpDelete("[action]")]
        public IActionResult DeleteShippingMethod(string Id)
        {
            if (string.IsNullOrEmpty(Id) ) return BadRequest("Id is Null or Empty!");
            var result=_shippingMethodService.DeleteShipping(Id);
            return result.IsSuccess? Ok(result) : BadRequest(result);


        }

    }
}
