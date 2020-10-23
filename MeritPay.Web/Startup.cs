using MeritPay.Core.IoC;
using MeritPay.Infrastructure.Data;
using MeritPay.Infrastructure.IoC;
using MeritPay.WebApi.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Sso.UMProxy;
using System.Threading.Tasks;

namespace MeritPay.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            #region Database Context
            services.AddDbContext<MeritPayContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("MeritPayConnection"));
            });
            #endregion

            //services.AddControllersWithViews(options =>
            //{
            //    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            //})
            //.AddJsonOptions(options =>
            //{
            //    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
            //});

            //بخاطر اینکه در کور نحوه سریالایز تغییر کرده و کندو کار نمیکنه این قسمت اضافه گردید
            services.AddControllersWithViews().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });

            Core.Contracts.AppSettings.lastUpdateDate = Configuration["AppSettings:lastUpdateDate"];
            Core.Contracts.AppSettings.portalPath = Configuration["AppSettings:portalPath"];
            Core.Contracts.AppSettings.resourceUrl = Configuration["AppSettings:resourceUrl"];
            Core.Contracts.AppSettings.SSOApplicationId = Configuration["AppSettings:SSOApplicationId"];
            Core.Contracts.AppSettings.SSOApplicationName = Configuration["AppSettings:SSOApplicationName"];
            Core.Contracts.AppSettings.SSOdomainName = Configuration["AppSettings:SSOdomainName"];
            Core.Contracts.AppSettings.SSOUrl = Configuration["AppSettings:SSOUrl"];
            Core.Contracts.AppSettings.themeUrl = Configuration["AppSettings:themeUrl"];
            Core.Contracts.AppSettings.Url = Configuration["AppSettings:Url"];


            //services.AddMvc(options =>
            //{
            //    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());                
            //});

            #region IoC

            //services.AddTransient<IReportService, ReportService>();
            services.RegisterCoreServices();
            services.RegisterInfrastructureServices();
            services.RegisterApiServices();

            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseMiddleware<SSORequestMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();          
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
    public class SSORequestMiddleware
    {
        private readonly RequestDelegate _next;

        public SSORequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // 'BeginRequest'
            SetSetting();
            var user = new SsoUser(UMLSettings);
            user.ValidateAuthentication(context);
            Core.Common.CommonHelper.CurrentUser = user;

            // Let the middleware pipeline run
            await _next(context);

            // 'EndRequest'
            SsoUser.RedirectSsoAuthentication();
        }
        private static Sso.UMProxy.AppSettings UMLSettings = new Sso.UMProxy.AppSettings();
        public static void SetSetting()
        {
            UMLSettings.SSOApplicationName = Core.Contracts.AppSettings.SSOApplicationName;
            UMLSettings.SSOApplicationId = Core.Contracts.AppSettings.SSOApplicationId;
            UMLSettings.SSOdomainName = Core.Contracts.AppSettings.SSOdomainName;
            UMLSettings.SSOUrl = Core.Contracts.AppSettings.SSOUrl;
        }
    }

}
