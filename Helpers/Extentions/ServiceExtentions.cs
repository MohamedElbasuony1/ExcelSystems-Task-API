using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Helpers.Mapping;
using System.Reflection;
using DAL;
using Microsoft.EntityFrameworkCore;
using Services;
using Contracts;
using Repos;
using LoggerService;
using Helpers.Validations;
using DTOs;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;

namespace Helpers.Extentions
{
    public static class ServiceExtention
    {
        public static void ConfigureSqlContext(this IServiceCollection services, string connectionstring)
        {
            services.AddDbContext<MyContext>(a => a.UseSqlServer(connectionstring));
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<ProductService>();
            services.AddScoped<PasswordHasherService>();
        }
        public static void ConfigureMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(new Assembly[]
                                        {
                                            typeof(ProductMapping).GetTypeInfo().Assembly,
                                            typeof(UserMapping).GetTypeInfo().Assembly,
                                            typeof(UserTokenMapping).GetTypeInfo().Assembly
                                        });
        }
        public static void ConfigureUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork,UnitOfWork>();
        }
        public static void ConfigureRepos(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        public static void ConfigureValidations(this IServiceCollection services)
        {
            services.AddSingleton < IValidator<UserModel>, UserModelValidations>();
            services.AddSingleton < IValidator<ProductModel>, ProductModelValidations>();
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureModelState(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                    var result = new
                    {
                        Code = "00009",
                        Message = "Validation errors",
                        Errors = errors
                    };
                    return new BadRequestObjectResult(result);
                };
            });
        }

    }
}
