﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites.DTOs.AuthDTOs
{
    public class LoginDTO
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }
}
