using KarlShoes.Core.Helper.EmailHelper.Abstrac;
using KarlShoes.Core.Helper.EmailHelper.Concrete;
using KarlShoes.Core.Security.Abstarct;
using KarlShoes.Core.Security.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarlShoes.Core.DependencyResolver
{
    public static class ServiceRegistration
    {
        public static void AddCoreService(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenManager>();
            services.AddScoped<IEmailHelper, EmailHelper>();
        }
    }
}
