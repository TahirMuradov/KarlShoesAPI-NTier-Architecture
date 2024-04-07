using KarlShoes.Bussines.Abstarct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KarlShoes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet("[action]")]
       public async Task<IActionResult> GetAllRole()
        {
            var result = await _roleService.GetAllRoleAsync();


            return result.IsSuccess? Ok(result):BadRequest();
        }
        [HttpGet("[action]")]
        public async Task<IActionResult> GetRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return BadRequest("Id is Null or Empty");
            var result=await _roleService.GetRoleAsync(id);
            return result.IsSuccess? Ok(result):BadRequest(result);
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> CreateRole(string RoleName)
        {
            if (string.IsNullOrEmpty(RoleName)) return BadRequest("RoleName is Null or Empty!");
            var chekedRole = await _roleService.GetAllRoleAsync();
            if (chekedRole.Data.Any(x => x.RoleName == RoleName)) return BadRequest("There is this role ");
            var result = await _roleService.CreateRoleAsync(RoleName);
            return result.IsSuccess? Ok(result): BadRequest(result);
         
        }
        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteRole(string RoleId)
        {
            if (string.IsNullOrEmpty(RoleId)) return BadRequest("RoleId is Null Or Empty");
            var result=await _roleService.DeleteRoleAsync(RoleId);
            return result.IsSuccess? Ok(result):BadRequest(result);
        }
    }
}
