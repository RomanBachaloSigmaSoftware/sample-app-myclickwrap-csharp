using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Security.Claims;
using DocuSign.eSign.Client;
using DocuSign.eSign.Client.Auth;
using DocuSign.MyClick.COVID19Waiver.Domain;
using DocuSign.MyClick.COVID19Waiver.Exceptions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DocuSign.MyClick.COVID19Waiver.Security
{
    [ExcludeFromCodeCoverage]
    public class DocuSignAuthenticationService : IDocuSignAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly ApiClient _apiClient;

        public DocuSignAuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
            _apiClient = new ApiClient();
            _apiClient.SetOAuthBasePath(_configuration["DocuSign:AuthServer"]);
        }

        public (ClaimsPrincipal, AuthenticationProperties) AuthenticateFromJwt()
        {
            OAuth.OAuthToken authToken;
            try
            {
                authToken = _apiClient.RequestJWTUserToken(
                    _configuration["DocuSign:IntegrationKey"],
                    _configuration["DocuSign:UserId"],
                    _configuration["DocuSign:AuthServer"],
                    File.ReadAllBytes(_configuration["DocuSign:RSAPrivateKeyFile"]),
                    int.Parse(_configuration["DocuSign:JWTLifeTime"]),
                    new List<string> { "click.manage", "signature", "impersonation" });
            }
            catch (ApiException e)
            {
                ApiExceptionContent error = JsonConvert.DeserializeObject<ApiExceptionContent>(
                    e.ErrorContent);
                if (error.Error == "consent_required")
                {
                    throw new ConsentRequiredException();
                }

                throw;
            }

            OAuth.UserInfo userInfo = _apiClient.GetUserInfo(authToken.access_token);
            var claims = new List<Claim>
                {
                    new Claim("access_token", authToken.access_token),
                    new Claim(ClaimTypes.NameIdentifier, userInfo.Sub),
                    new Claim(ClaimTypes.Name, userInfo.Name),
                    new Claim("account_id", userInfo.Accounts.First(x => x.IsDefault == "true").AccountId),
                };

            foreach (var account in userInfo.Accounts)
            {
                claims.Add(new Claim("accounts", JsonConvert.SerializeObject(account)));
            }

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc =
                    DateTimeOffset.UtcNow + (authToken.expires_in.HasValue
                        ? TimeSpan.FromSeconds(authToken.expires_in.Value)
                        : TimeSpan.FromHours(int.Parse(_configuration["DocuSign:JWTLifeTime"])))
                    - TimeSpan.FromMinutes(1),
                IsPersistent = true,
            };

            return (new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        public string GetConsentUrl(string redirectUrl)
        {
            return $"{_configuration["DocuSign:AuthorizationEndpoint"]}" +
                   $"?response_type=code&scope=click.manage impersonation signature" +
                   $"&client_id={_configuration["DocuSign:IntegrationKey"]}" +
                   $"&redirect_uri={redirectUrl}";
        }
    }
}
