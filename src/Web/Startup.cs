using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.eShopWeb.Infrastructure.Data;
using Microsoft.eShopWeb.Web.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            //ConfigureTestingServices(services);

            // use real database
            ConfigureProductionServices(services);

        }

        private void ConfigureInMemoryDatabases(IServiceCollection services)
        {
            // use in-memory database
            services.AddDbContext<CatalogContext>(c =>
                c.UseInMemoryDatabase("Catalog"));

            // Add Identity DbContext
            //services.AddDbContext<AppIdentityDbContext>(options =>
            //    options.UseInMemoryDatabase("Identity"));

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            // use real database
            // Requires LocalDB which can be installed with SQL Server Express 2016
            // https://www.microsoft.com/en-us/download/details.aspx?id=54284
            services.AddDbContext<CatalogContext>(c =>
                c.UseSqlServer(Configuration.GetConnectionString("CatalogConnection")));

            // Add Identity DbContext
            //services.AddDbContext<AppIdentityDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            ConfigureServices(services);
        }

        public void ConfigureTestingServices(IServiceCollection services)
        {
            ConfigureInMemoryDatabases(services);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCoreServices(Configuration);
            services.AddWebServices(Configuration);

            //services.AddDbContext<CatalogContext>(c =>
            //    c.UseInMemoryDatabase("Catalog"));

            services.AddMemoryCache();
            services.AddRouting(options =>
            {
                // Replace the type and the name used to refer to it with your own
                // IOutboundParameterTransformer implementation
                //options.ConstraintMap["slugify"] = typeof(SlugifyParameterTransformer);
            });
            services.AddMvc(options =>
            {
                //options.Conventions.Add(new RouteTokenTransformerConvention(
                //         new SlugifyParameterTransformer()));

            });
            services.AddControllersWithViews();
            services.AddRazorPages(options =>
            {
                //options.Conventions.AuthorizePage("/Basket/Checkout");
            });
            services.AddHttpContextAccessor();
            services.AddHealthChecks();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Home}/{action=Index}/{id?}");

                //endpoints.MapControllerRoute("default", "{controller:slugify=Home}/{action:slugify=Index}/{id?}");
                endpoints.MapRazorPages();

                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
