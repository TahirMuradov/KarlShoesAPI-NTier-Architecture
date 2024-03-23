using FluentValidation.AspNetCore;
using KarlShoes.Bussines.FluentValidation.RegisterUserValidator;
using KarlShoes.Bussines.ValidationFilters;
using KarlShoes.DataAccess.Concrete.SQLServer;
using KarlShoes.Entites;
using Microsoft.AspNetCore.Identity;
using System;
using KarlShoes.Bussines.DependencyResolver;
using System.Globalization;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(x =>
{
    x.SwaggerDoc("v1", new OpenApiInfo { Title = "KarlShoes", Version = "v1", Description = "Identity Service API swagger client." });
    //x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    //{
    //    Name = "Authorization",
    //    Type = SecuritySchemeType.ApiKey,
    //    Scheme = "Bearer",
    //    BearerFormat = "JWT",
    //    In = ParameterLocation.Header,
    //    Description = "Example: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\\"
    //});
    //x.AddSecurityRequirement(new OpenApiSecurityRequirement()
    //{
    //    {
    //        new OpenApiSecurityScheme
    //        {
    //            Reference = new OpenApiReference
    //            {
    //                Type = ReferenceType.SecurityScheme,
    //                Id = "Bearer"
    //            }
    //        },
    //        Array.Empty<string>()
    //    }

    //});
});
#region Fluent Validation Registration add services to the container.
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration =>
    {
        configuration.RegisterValidatorsFromAssemblyContaining<RegisterDTOValidator>();
        configuration.DisableDataAnnotationsValidation = true;
        configuration.ValidatorOptions.LanguageManager.Culture = new CultureInfo("az-AZ");
    })
    .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
#endregion
builder.Services.AddScoped<AppDBContext>();
builder.Services.AddAllScoped();
builder.Services.AddDefaultIdentity<User>().AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDBContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
