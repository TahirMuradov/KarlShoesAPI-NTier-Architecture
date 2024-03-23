using KarlShoes.Bussines.Abstarct;
using KarlShoes.Bussines.FluentValidation.RegisterUserValidator;
using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Core.Utilities.Results.Concrete.ErrorResults;
using KarlShoes.Core.Utilities.Results.Concrete.SuccessResults;
using KarlShoes.Entites;
using KarlShoes.Entites.DTOs.UserDTOs;
using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Concrete
{
    public class UserManager : IUserServices
    {
        private readonly UserManager<User> _userManager;

        public UserManager(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IResult> Register(RegisterDTO registerDTO)
        {
            var validator = new RegisterDTOValidator();
            var validationResult= validator.Validate(registerDTO); 
            if (!validationResult.IsValid) return new ErrorResult(message:validationResult.Errors.ToString(),statusCode:HttpStatusCode.BadRequest);
            var checkedEmail = _userManager.FindByEmailAsync(registerDTO.Email);
            if (checkedEmail is not null)
                return new ErrorResult(statusCode:HttpStatusCode.BadRequest);
            User user = new User()
            {
                FirstName=registerDTO.Firstname,
                LastName=registerDTO.Lastname,
                Email=registerDTO.Email,
               PhoneNumber=registerDTO.PhoneNumber,
               UserName=registerDTO.UserName,
            };
            IdentityResult result = await _userManager.CreateAsync(user, registerDTO.Password);
          return result.Succeeded? new SuccessResult(statusCode:HttpStatusCode.Created):new ErrorResult(statusCode:HttpStatusCode.BadRequest);

        }
   
    
    }
}
