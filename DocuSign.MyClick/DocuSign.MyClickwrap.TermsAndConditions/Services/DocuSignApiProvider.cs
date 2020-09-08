using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.AspNetCore.Http;
using RestSharp;
using RestSharp.Authenticators;

namespace DocuSign.MyClickwrap.TermsAndConditions.Services
{
    [ExcludeFromCodeCoverage]
    public class DocuSignApiProvider : IDocuSignApiProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DocuSignApiProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private Lazy<IRestClient> _docuSignClickApiRestClient => new Lazy<IRestClient>(() =>
        {
            string token = _httpContextAccessor.HttpContext.User.FindAll("access_token").First().Value;
            var client = new RestClient(Context.Account.BaseUri + "/clickapi/v1/")
            {
                Authenticator = new JwtAuthenticator(token)
            };

            return client;
        });

        public IRestClient DocuSignClickApiRestClient => _docuSignClickApiRestClient.Value;
    }
}