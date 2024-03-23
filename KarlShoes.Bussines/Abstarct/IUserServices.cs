using KarlShoes.Core.Utilities.Results.Abtsract;
using KarlShoes.Entites.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.Abstarct
{
    public interface IUserServices
    {
        public Task<IResult> Register(RegisterDTO registerDTO);
    }
}
