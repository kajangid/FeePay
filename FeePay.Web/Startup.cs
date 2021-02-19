using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using FeePay.Core.Application;
using FeePay.Core.Application.Interface;
using FeePay.Core.Application.Interface.Service;
using FeePay.Infrastructure.Persistence;
using FeePay.Web.Extensions;
using FeePay.Web.IoC;
using FeePay.Web.Services;
using FeePay.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace FeePay.Web
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
            services.AddLogging();
            services.AddSingleton<IValidationAttributeAdapterProvider, CustomValidatiomAttributeAdapterProvider>();
            services.AddSingleton<IMvcControllerDiscovery, MvcControllerDiscovery>();
            services.AddSingleton<IUtilityService, UtilityService>();
            services.AddSingleton<ISiteSettings>(Configuration.GetSection("SiteSettings").Get<SiteSettings>());
            //services.Configure<SiteSettings>(Configuration.GetSection("SiteSettings"));

            services.AddTransient<IAppContextAccessor, AppContextAccessor>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();


            services.AddHttpContextAccessor();
            services.AddInfrastructurePersistenceServices();
            services.AddApplicationServices(Configuration);
            services.AddIdentityServices(Configuration);

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddControllersWithViews();
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
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseStaticFiles();
            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
