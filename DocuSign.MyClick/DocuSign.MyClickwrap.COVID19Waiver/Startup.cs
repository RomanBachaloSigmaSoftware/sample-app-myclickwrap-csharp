using DocuSign.MyClickwrap.COVID19Waiver.Security;
using DocuSign.MyClickwrap.COVID19Waiver.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DocuSign.MyClickwrap.COVID19Waiver
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddHttpContextAccessor();
            services.AddScoped<Context, Context>();
            services.AddScoped<IDocuSignAuthenticationService, DocuSignAuthenticationService>();
            services.AddScoped<IClickWrapService, ClickWrapService>();
            services.AddScoped<IDocuSignApiProvider, DocuSignApiProvider>();
            services.AddControllersWithViews();
            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(ContextFilter));
                    options.Filters.Add(new ResponseCacheAttribute { NoStore = true, Location = ResponseCacheLocation.None });
                })
                .AddNewtonsoftJson();
            services.ConfigureDocuSignJWTAuthentication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            ILoggerFactory loggerFactory)
        {
            app.UsePathBase("/covid19waiver");
            app.ConfigureDocuSignExceptionHandling(loggerFactory);
            app.ConfigureDocuSign();
            app.UseStaticFiles();

            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization(); 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                spa.ApplicationBuilder.Use(async (context, next) =>
                {
                    var originalPath = context.Request.Path;
                    var originalPathBase = context.Request.PathBase;
                    context.Request.Path = originalPathBase.Add(originalPath);
                    context.Request.PathBase = PathString.Empty;

                    try
                    {
                        await next.Invoke();
                    }
                    finally
                    {
                        context.Request.Path = originalPath;
                        context.Request.PathBase = originalPathBase;
                    }
                });
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer("start");
                }
            });
        }
    }
}