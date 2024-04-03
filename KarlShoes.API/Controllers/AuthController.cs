using KarlShoes.Bussines.Abstarct;
using KarlShoes.Bussines.FluentValidation.AuthDTOValidator;
using KarlShoes.Entites.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace KarlShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            var validator = new RegisterUserValidation();
            var ResultValidator=validator.Validate(registerDTO);
            if (!ResultValidator.IsValid) return BadRequest(ResultValidator.Errors);
            var result = await _authService.RegisterAsync(registerDTO);
            if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<IActionResult> AssignRoleToUser(string userId, string[] role)
        {
            var result = await _authService.AssignRoleToUserAsnyc(userId, role);
            if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        [Authorize]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string role)
        {
            var result = await _authService.RemoveRoleFromUserAsync(userId, role);
            if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var validation=new LoginUserValidation();
            var resultValidator=validation.Validate(loginDTO);
            if (!resultValidator.IsValid)return BadRequest(resultValidator.Errors);
            var result = await _authService.LoginAsync(loginDTO);
            if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }

        [Authorize]
        [HttpPut("[action]")]
        public async Task<IActionResult> LogOut()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _authService.LogOutAsync(userId);
            if (result.StatusCode == HttpStatusCode.BadRequest)
                return BadRequest(result);
            return Ok(result);
        }
    }
}
