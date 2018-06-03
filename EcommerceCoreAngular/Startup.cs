using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using EcommerceCoreAngular.DataContext;
using EcommerceCoreAngular.Models;
using EcommerceCoreAngular.Paypal;
using EcommerceCoreAngular.Services.Infrastructure;
using EcommerceCoreAngular.Services.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace EcommerceCoreAngular
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
                             //para poder retornar listas
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });

            /*Adler*/
            services.AddOptions();
            services.Configure<PaypalSettings>(Configuration.GetSection("PaypalSettings"));

            services.AddMemoryCache();
            services.AddSession();

            services.AddDbContext<MyContext>(options =>
                                 options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddIdentity<Customer, ApplicationRole>()
                .AddEntityFrameworkStores<MyContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IProduct, ProductRepository>();
            services.AddScoped<ICategory, CategoryRepository>();
            services.AddScoped<ISubCategory, SubCategoryRepository>();
            services.AddSingleton<IOrder, OrderRepository>();
            services.AddScoped<IOrderLine, OrderLineRepository>();
            services.AddTransient<IPicture, PictureRepository>();
            services.AddScoped<ICartItem, CartItemRepository>();

            //AddSingleton
            //É CRIADO APENAS NO PRIMEIRO REQUEST E EXISTE PARA TODOS OS OUTROS REQUESTS
            //AddScoped
            //É CRIADO UM POR REQUEST E SEGUE O MESMO EM DEFINICOES DIFERENTES USA A MESMA INSTANCIA EM OUTRAS REQUESTS
            //AddTransient
            //É CRIADO A CADA REQUEST E DEFINICAO DIFERENTE E DEPOIS SOME
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            /*Adler*/
            app.UseCors("CorsPolicy");
            app.UseStaticFiles(
                 new StaticFileOptions()
                 {
                     FileProvider = new PhysicalFileProvider(
                     Path.Combine(Directory.GetCurrentDirectory(), @"ClientApp")),
                     RequestPath = new PathString("/clientapp")
                 });
            app.UseSession();
            app.UseAuthentication();
            /*Adler*/

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });

                /*Adler*/
                /*Admin area router*/
                routes.MapRoute(
                    name: "AdminAreaProduct",
                    template: "{area:exists}/{controller=Products}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "AdminAreaCategory",
                    template: "{area:exists}/{controller=Products}/{action=Index}/{id?}");
            });
        }
    }
}
