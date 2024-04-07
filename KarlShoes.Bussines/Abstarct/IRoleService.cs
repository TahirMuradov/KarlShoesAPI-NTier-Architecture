using KarlShoes.Core.Entities.Concrete;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Entites.DTOs.RoleDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Abstarct
{
    public interface IRoleService
    {
        Task<IDataResult<List<GetRoleDTO>>> GetAllRoleAsync();
        Task<IDataResult<GetRoleDTO>> GetRoleAsync(string roleId);
        Task<IResult> CreateRoleAsync(string rolName);
        Task<IResult> DeleteRoleAsync(string RoleId);
    }
}
