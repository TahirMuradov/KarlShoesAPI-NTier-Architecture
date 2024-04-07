using KarlShoes.Core.Entities.Concrete;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Entites;
using KarlShoes.Entites.DTOs.AuthDTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Abstarct
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(RegisterDTO registerDTO);
        Task<IResult> AssignRoleToUserAsnyc(string userId, string[] role);
        Task<IDataResult<string>> UpdateRefreshToken(string refreshToken, AppUser user);
        Task<IResult> RemoveRoleFromUserAsync(string userId, string role);
        Task<IDataResult<Token>> LoginAsync(LoginDTO loginDTO);
        Task<IResult> LogOutAsync(string userId);
        //Task<IDataResult<AppUser>> GetAllUser();
        //Task<IDataResult<AppUser>> GetUser(string userId);
        
    }
}
