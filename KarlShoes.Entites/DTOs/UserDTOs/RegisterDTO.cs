﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Entites.DTOs.UserDTOs
{
    public class RegisterDTO
    {
        public string Firstname { get; set; }
        public string UserName { get; set; }


        public string Lastname { get; set; }



        public string Email { get; set; }


        public string PhoneNumber { get; set; }


        public string Password { get; set; }


        public string ConfirmPassword { get; set; }
    }
}
