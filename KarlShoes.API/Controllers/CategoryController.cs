using KarlShoes.Bussines.Abstarct;
using KarlShoes.Bussines.FluentValidation.CategoryDTOValidator;
using KarlShoes.Entites.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KarlShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryServices;
        public CategoryController(ICategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> AddCategory(CategoryAddDTO categoryAddDTO)
        {
            var validator = new CategoryAddDTOValidator();
           var ValidationResult= validator.Validate(categoryAddDTO);
          if (!ValidationResult.IsValid)
                return BadRequest(ValidationResult.Errors);
          var data = await _categoryServices.AddCategoryAsync(categoryAddDTO);
            return data.IsSuccess ? Ok(data) : BadRequest(data);
        }
        [HttpGet("[action]")]
        public IActionResult GetAllCatigories(string LangCode)
        {
            if (string.IsNullOrEmpty(LangCode)) return BadRequest();
            var data = _categoryServices.GetAllCategory(LangCode);
            return data.IsSuccess?Ok(data):BadRequest(data);
        }

        [HttpGet("[action]")]
        public IActionResult GetCategory(string id,string LangCode) 
        {
            if (string.IsNullOrEmpty(LangCode)|| string.IsNullOrEmpty(id)) return BadRequest();
            var data=_categoryServices.GetCategory(LangCode: LangCode,id: id);
            return data.IsSuccess?Ok(data): BadRequest(data);
        
       
        }
        [HttpDelete("[action]")]
public IActionResult DeleteCategory(string id) 
        {

            if (string.IsNullOrEmpty(id)) return BadRequest();
            var result= _categoryServices.DeleteCategory(id);
            return result.IsSuccess? Ok(result):BadRequest(result);
        }
        [HttpPut("[action]")]

        public IActionResult UpdateCategory([FromBody] CategoryUpdateDTO categoryUpdateDTO)
        {
            var validator = new CategoryUpdateDTOValidatior();
            var ValidationResult = validator.Validate(categoryUpdateDTO);
            if (!ValidationResult.IsValid) return BadRequest(ValidationResult.Errors);
            var result=_categoryServices.UpdateCatgeory(categoryUpdateDTO);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
            
        }
    }
}
