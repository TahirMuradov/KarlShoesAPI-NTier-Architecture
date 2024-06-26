﻿using KarlShoes.Core.Entities.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Core.Security.Abstarct
{
    public interface ITokenService
    {
        Task<Token> CreateAccessTokenAsync(AppUser User, List<string> roles);
        string CreateRefreshToken();
    }
}
