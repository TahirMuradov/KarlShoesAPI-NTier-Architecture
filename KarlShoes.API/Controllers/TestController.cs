using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KarlShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
      
        [HttpGet("[action]")]
        public IActionResult Test()
        {
            var a = JsonConvert.SerializeObject(new object[] { "salam", 1, 5 });
            
            return Ok(a);
        }
    }
}
