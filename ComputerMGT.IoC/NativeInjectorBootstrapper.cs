using Microsoft.Extensions.DependencyInjection;
using ComputerMGT.Application.Interfaces;
using ComputerMGT.Application.Services;
using ComputerMGT.Data;
using ComputerMGT.Data.Interfaces;
using ComputerMGT.Data.Repository;
using ComputerMGT.Data.UoW;
using ComputerMGT.Domain.Models;

namespace ComputerMGT.IoC
{
    public static class NativeInjectorBootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            // services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // ASP.NET Authorization Polices
            // services.AddSingleton<IAuthorizationHandler, ClaimsRequirementHandler>();


            // Application
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IProductService, ProductService>();

            //// Data- Repo
            services.AddScoped<IRepository<TblUser>, Repository<TblUser>>();
            services.AddScoped<IRepository<TblProduct>, Repository<TblProduct>>();
            services.AddScoped<IRepository<TblOrderDetail>, Repository<TblOrderDetail>>();
            services.AddScoped<IRepository<TblOrder>, Repository<TblOrder>>();
            services.AddScoped<IRepository<TblCategory>, Repository<TblCategory>>();
            services.AddScoped<IRepository<TblCart>, Repository<TblCart>>();


            // Infra - Data
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ComputerMGTContext>();

            // Infra - Identity Services
            // services.AddTransient<IEmailSender, AuthEmailMessageSender>();
            // .AddTransient<ISmsSender, AuthSmsMessageSender>();

            // Infra - Identity
            // services.AddScoped<IUser, AspNetUser>();
        }
    }
}