using FluentValidation;
using KarlShoes.Bussines.Abstarct;
using KarlShoes.Bussines.Concrete;
using KarlShoes.Core.Entities.Concrete;
using KarlShoes.Core.Security.Abstarct;
using KarlShoes.Core.Security.Concrete;
using KarlShoes.DataAccess.Abstract;
using KarlShoes.DataAccess.Concrete;
using KarlShoes.DataAccess.Concrete.SQLServer;
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
          
      
            services.AddScoped<ICategoryServices, CategoryManager>();
            services.AddScoped<ICategoryDAL, EFCategoryDAL>();
            services.AddScoped<ISubCategoryDAL, EFSubCategoryDAL>();
            services.AddScoped<ISubCategoryServices, SubCategoryManager>();
            services.AddScoped<ISizeDAL, EFSizeDAL>();
            services.AddScoped<ISizeServices, SizeManager>();
            services.AddScoped<IProductServices, ProductManager>();
            services.AddScoped<IProductDAL, EFProductDAL>();
            services.AddScoped<IAuthService, AuthManager>();
            services.AddScoped<ITokenService, TokenManager>();
            services.AddScoped<IPictureDAL, EFpictureDAL>();
            services.AddScoped<IPictureServices, PictureManager>();
            services.AddIdentity<AppUser, AppRole>()
             .AddEntityFrameworkStores<AppDBContext>()
             .AddDefaultTokenProviders();



        }
    }
}
