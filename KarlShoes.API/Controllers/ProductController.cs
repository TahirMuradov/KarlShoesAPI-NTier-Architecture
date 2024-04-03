using KarlShoes.Bussines.Abstarct;
using KarlShoes.Bussines.FluentValidation.ProductDTOValidator;
using KarlShoes.Entites;
using KarlShoes.Entites.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarlShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productServices;

        public ProductController(IProductServices productServices)
        {
            _productServices = productServices;
        }

        [HttpPost("ProductAddAsync")]
        public async Task<IActionResult> ProductAddAsync(ProductAddDTO productAddDTO)
        {
            List<IFormFile> formFiles = new List<IFormFile>();
            var validator = new ProductAddDTOValidator();
            var resultValiodator = validator.Validate(productAddDTO);
            if (!resultValiodator.IsValid) return BadRequest(resultValiodator.Errors);
            var result = await _productServices.ProductAddAsync(productAddDTO: productAddDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult ProductGet(string ProductId,string LangCode)
        {
            if (string.IsNullOrEmpty(ProductId) || string.IsNullOrEmpty(LangCode))
                return BadRequest("ProductId Or LangCode is Empty or Null!");
            var result=_productServices.ProductGet(ProductId,LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpGet("[action]")]
        public IActionResult ProductGetAll(string LangCode)
        {
            if ( string.IsNullOrEmpty(LangCode))
                return BadRequest("LangCode is Empty or Null!");
            var result = _productServices.ProductGetAll(LangCode);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
        [HttpPut("[action]")]
        public async Task<IActionResult> ProductUpdateAsync([FromBody] ProductUpdateDTO productUpdateDTO)
        {
          
            var validator=new ProductUpdateDTOValidator();
            var ResultValidator=validator.Validate(productUpdateDTO);
            if (!ResultValidator.IsValid) return BadRequest(ResultValidator.Errors);
            var result = await _productServices.ProductUpdateAsync(productUpdateDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("[action]")]
        public IActionResult ProductDelete(string ProductId)
        {
            if (string.IsNullOrEmpty(ProductId)) return BadRequest("Id Is Null OR Empty!");
            var result=_productServices.ProductRemove(ProductId);
            return result.IsSuccess ? Ok(result) : NotFound(result);

        }
    
    }
}
