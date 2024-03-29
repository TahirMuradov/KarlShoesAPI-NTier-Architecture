using KarlShoes.Entites.DTOs.ProductDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarlShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost("[action]")]
        public IActionResult ProductAdd([FromForm]ProductAddDTO productAddDTO,[FromHeader] List<IFormFile> FormFiles )
        {
            return Ok();
        }
    }
}
