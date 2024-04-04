using KarlShoes.Bussines.Abstarct;
using KarlShoes.Bussines.FluentValidation.SubCategoryDTOValidator;
using KarlShoes.Entites.DTOs.SubCategoryDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Prng;

namespace KarlShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryServices;

        public SubCategoryController(ISubCategoryService subCategoryServices)
        {
            _subCategoryServices = subCategoryServices;
        }
        [HttpGet("[action]")]
        public IActionResult SubCategoryGet(string id, string LangCode)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(LangCode)) return BadRequest();
            var data = _subCategoryServices.SubCategoryGet(id, LangCode);
            return data.IsSuccess ? Ok(data) : BadRequest(data);
        }
        [HttpGet("[action]")]
        public IActionResult SubCategoryAllGet(string LangCode)
        {
            if ( string.IsNullOrEmpty(LangCode)) return BadRequest();
            var data=_subCategoryServices.SubCategoryAllGet(LangCode);
            return data.IsSuccess ? Ok(data) : BadRequest(data);
        }
        [HttpDelete("[action]")]
        public IActionResult SubCategoryDelete(string Id)
        {
            if (string.IsNullOrEmpty(Id)) return BadRequest();
            var data=_subCategoryServices.SubCategoryDelete(Id);
            return data.IsSuccess ? Ok(data) : BadRequest(data);

        }
        [HttpPost("[action]")]
        public IActionResult SubCategoryAdd(SubCategoryAddDTO subCategoryAddDTO)
        {
            var validator=new SubCategoryAddDTOValidator();
            var ValidatorResult=validator.Validate(subCategoryAddDTO);
            if (!ValidatorResult.IsValid) return BadRequest(ValidatorResult.Errors);
            var result=_subCategoryServices.SubCategoryAdd(subCategoryAddDTO);
            return result.IsSuccess? Ok(result) : BadRequest(result);
            
        }
        [HttpPut("[action]")]
        public IActionResult SubCategoryUpdate([FromBody] SubCategoryUpdateDTO subCategoryUpdateDTO)
        {
            var validation=new SubCategoryUpdateDTOValidation();
            var validationResult=validation.Validate(subCategoryUpdateDTO);
            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);
            var result=_subCategoryServices.SubCategoryUpdate(subCategoryUpdateDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
