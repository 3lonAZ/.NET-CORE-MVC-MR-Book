using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MR_Book.Areas.Admin.Models;
using MR_Book.Areas.Admin.Models.Admin;
using MR_Book.Areas.Admin.Models.Crud_Operations;
using MR_Book.Models.Connections;
using MR_Book.Models.Pages;
using MR_Book.Models.Pages.ExternalData;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MR_Book
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
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddControllersWithViews().AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Startup>());
            services.AddSingleton<IConnections<SqlConnection>, MyLocalSqlConnection>();
            services.AddSingleton<IExternalFeature, ExternalFeature>();
            services.AddSingleton<IPage, PageFactory>();
            services.AddSingleton<IExternalFeature,ExternalFeature>();
            services.AddSingleton<ILogin,Login>();
            services.AddSingleton<ICrud<BookModel>, BookFacotry>();
            services.AddSingleton<ICrud<CategoryModel>, CategoryFactory>();
            services.AddSingleton<ICrud<LanguageModel>, LanguageModelFactory>();
            services.AddSingleton<ICrudSpecial<OrderModel>, OrdersModelFactory>();
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseStatusCodePagesWithReExecute("/ErrorPage/Error", "?statusCode={0}");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseSession();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                      name: "admin_panel",
                      pattern: "{area:exists}/{controller=Admin}/{action=Index}/{id?}"
                    );
                });
              
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
