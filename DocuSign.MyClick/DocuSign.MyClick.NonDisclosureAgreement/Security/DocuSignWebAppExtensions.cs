using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Authentication;
using System.Threading.Tasks;
using DocuSign.eSign.Client;
using DocuSign.MyClick.NonDisclosureAgreement.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DocuSign.MyClick.NonDisclosureAgreement.Security
{
    [ExcludeFromCodeCoverage]
    public static class DocuSignWebAppExtensions
    {
        public static void ConfigureDocuSign(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseAuthentication();
            applicationBuilder.UseSession();
        }

        public static void ConfigureDocuSignJWTAuthentication(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication(options =>
                {
                    options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
                {
                    config.Cookie.Name = "UserLoginCookie";
                    config.LoginPath = "/Account/Login";
                    config.SlidingExpiration = true;
                    config.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                    config.Events = new CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = context =>
                        {
                            // Return 401 HttpCode for api calls instead of redirecting to login page 
                            if (context.Request.Path.StartsWithSegments("/api"))
                            {
                                context.Response.Headers["Location"] = context.RedirectUri;
                                context.Response.StatusCode = 401;
                            }
                            else
                            {
                                context.Response.Redirect(context.RedirectUri);
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });
        }

        public static void ConfigureDocuSignExceptionHandling(this IApplicationBuilder app,
            ILoggerFactory loggerFactory)
        {
            ILogger<Startup> logger = loggerFactory.CreateLogger<Startup>();
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        switch (contextFeature.Error)
                        {
                            case ApiException error:
                                logger.LogError($"Error occured during DocuSign api call: {contextFeature.Error}");

                                if (error.ErrorCode == (int)HttpStatusCode.Unauthorized)
                                {
                                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                }

                                break;
                            case AuthenticationException error:
                                logger.LogError($"AuthenticationException occured: {error.Message}");
                                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                                break;
                            default:
                                logger.LogError($"Error occured: {contextFeature.Error}");
                                break;
                        }

                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}