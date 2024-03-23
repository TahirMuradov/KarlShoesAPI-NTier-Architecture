using FluentValidation;
using KarlShoes.Bussines.Abstarct;
using KarlShoes.Bussines.Concrete;
using KarlShoes.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Bussines.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddAllScoped(this IServiceCollection services)
        {
          
            services.AddScoped<IUserServices, UserManager>();
           

        }
    }
}
