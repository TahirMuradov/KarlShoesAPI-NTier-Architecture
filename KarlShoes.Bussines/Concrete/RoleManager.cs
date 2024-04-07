using KarlShoes.Bussines.Abstarct;
using KarlShoes.Core.Entities.Concrete;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Core.Utilities.Results.Concrete.ErrorResults;
using KarlShoes.Core.Utilities.Results.Concrete.SuccessResults;
using KarlShoes.Entites.DTOs.RoleDTOs;
using MailKit;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleManager(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IResult> CreateRoleAsync(string rolName)
        {
            try
            {

                var result = await _roleManager.CreateAsync(new AppRole
                {
                    Name = rolName,
                });
                if (!result.Succeeded) return new ErrorResult(statusCode: System.Net.HttpStatusCode.BadRequest);
                return new SuccessResult(System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.InnerException?.Message);
                return new ErrorResult(message: ex.Message, statusCode: System.Net.HttpStatusCode.BadRequest);
            }

        }

        public async Task<IResult> DeleteRoleAsync(string RoleId)
        {
            try
            { 
                var role= await _roleManager.FindByIdAsync(RoleId);
                if (role == null) return new ErrorResult(message:"Role is Not Found",statusCode: System.Net.HttpStatusCode.NotFound);
                var result = await _roleManager.DeleteAsync(role);
                return new SuccessResult(statusCode: System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex )
            {
                await Console.Out.WriteLineAsync(ex.InnerException?.Message);
                return new ErrorResult(message: ex.Message, statusCode: System.Net.HttpStatusCode.BadRequest);

            }
        }

        public async Task<IDataResult<List<GetRoleDTO>>> GetAllRoleAsync()
        {
            try
            {
                var data = _roleManager.Roles.Select(x => new GetRoleDTO
                {
                    RoleId = x.Id,
                    RoleName = x.Name
                }).ToList();
                return new SuccessDataResult<List<GetRoleDTO>>(data: data, statusCode: System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {

                await Console.Out.WriteLineAsync(ex.InnerException?.Message);
                return new ErrorDataResult<List<GetRoleDTO>>(message: ex.Message, statusCode: System.Net.HttpStatusCode.BadRequest);

            }
        }

        public async Task<IDataResult<GetRoleDTO>> GetRoleAsync(string roleId)
        {
            try
            {

                var data = await _roleManager.FindByIdAsync(roleId);
                if (data == null) return new ErrorDataResult<GetRoleDTO>(message: "Role is Not Found", statusCode: System.Net.HttpStatusCode.NotFound);
                return new SuccessDataResult<GetRoleDTO>(data: new GetRoleDTO
                {
                    RoleId = data.Id,
                    RoleName = data.Name
                }
                ,
                statusCode:System.Net.HttpStatusCode.OK);
            }
            catch (Exception ex)
            {


                await Console.Out.WriteLineAsync(ex.InnerException?.Message);
                return new ErrorDataResult<GetRoleDTO>(message: ex.Message, statusCode: System.Net.HttpStatusCode.BadRequest);
            }
        }
    }
}
