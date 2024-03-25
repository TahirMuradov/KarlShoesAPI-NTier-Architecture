using KarlShoes.Bussines.Concrete;
using KarlShoes.Entites.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KarlShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AuthController : ControllerBase
    {

        private readonly UserManager _userManager;

        public AuthController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
          

            var result= await _userManager.Register(registerDTO);

            return result.IsSuccess?Ok(result):BadRequest(result);
        }

    }
}
