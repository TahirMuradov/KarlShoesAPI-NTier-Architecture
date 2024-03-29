using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarlShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public IActionResult ProductAdd()
        {
            return Ok();
        }
    }
}
