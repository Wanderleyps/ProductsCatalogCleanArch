using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductsCatalogCleanArch.Application.Interfaces;
using ProductsCatalogCleanArch.Application.Mappings;
using ProductsCatalogCleanArch.Application.Services;
using ProductsCatalogCleanArch.Domain.Account;
using ProductsCatalogCleanArch.Domain.Interfaces;
using ProductsCatalogCleanArch.Infra.Data.Context;
using ProductsCatalogCleanArch.Infra.Data.Identity;
using ProductsCatalogCleanArch.Infra.Data.Repositories;
using System;

namespace ProductsCatalogCleanArch.Infra.IoC
{
    public static class DependencyInjectionAPI
    {
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
             //define que os arquivos de migração irão ficar na pasta onde está definido o arquivo de contexto (Infra.Data)
             b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            //Incluindo a configuração do Identity para users e roles
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()//inclui EF Core do Identity
                .AddDefaultTokenProviders();//adiciona provedor de token padrão
           
            //AddScoped é recomendação para aplicações web
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IAuthenticate, AuthenticateService>();

            //necessário informar o nome do arquivo onde foram definidas as configurações do AutoMapper
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            //registrando a DI dos handlers
            var myHandlers = AppDomain.CurrentDomain.Load("ProductsCatalogCleanArch.Application");
            services.AddMediatR(myHandlers);

            return services;
        }
    }
}
