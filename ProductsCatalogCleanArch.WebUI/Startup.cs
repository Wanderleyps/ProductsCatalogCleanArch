using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductsCatalogCleanArch.Domain.Account;
using ProductsCatalogCleanArch.Infra.IoC;

namespace ProductsCatalogCleanArch.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //utilizando o método criado na classe DependencyInjection no projeto Infra.IoC,
            //algumas das classes registradas nesse método serão utilizados nos controllers
            services.AddInfrastructure(Configuration);
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ISeedUserRoleInitial seedUserRoleInitial)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            //para poder utilizar os arquivos estaticos no wwwroot é necessário ativar este middleware
            app.UseStaticFiles();

            app.UseRouting();

            //criando users e roles iniciais
            seedUserRoleInitial.SeedRoles();
            seedUserRoleInitial.SeedUsers();

            //importante seguir essa ordem
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //Define o controlador que é acionado quando a aplicação é iniciada
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
