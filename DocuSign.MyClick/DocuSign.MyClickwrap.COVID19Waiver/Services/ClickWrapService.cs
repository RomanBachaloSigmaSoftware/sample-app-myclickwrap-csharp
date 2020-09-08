using System;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using DocuSign.MyClickwrap.COVID19Waiver.Domain;
using DocuSign.MyClickwrap.COVID19Waiver.Exceptions;
using Newtonsoft.Json;
using RestSharp;

namespace DocuSign.MyClickwrap.COVID19Waiver.Services
{
    public class ClickWrapService : IClickWrapService
    {
        private const string ClickWrapName = "Covid19Waiver";

        private readonly IDocuSignApiProvider _docuSignApiProvider;

        public ClickWrapService(IDocuSignApiProvider docuSignApiProvider)
        {
            _docuSignApiProvider = docuSignApiProvider;
        }

        public ClickWrap GetClickWrap(string accountId, string userId)
        {
            if (accountId == null)
            {
                throw new ArgumentNullException(nameof(accountId));
            }

            if (userId == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            var request = new RestRequest($"/accounts/{accountId}/clickwraps", DataFormat.Json);

            IRestResponse response = _docuSignApiProvider.DocuSignClickApiRestClient.Get(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.Unauthorized:
                        throw new AuthenticationException(
                            "DocuSign Click API responded with status code Unauthorized");
                    default:
                        throw new InvalidOperationException(
                            $"DocuSign Click API responded with status code {response.StatusCode}");
                }
            }

            var clickwrapsResponse =
                JsonConvert.DeserializeObject<ClickWrapsResponse>(response.Content);

            ClickWrap clickWrap = clickwrapsResponse
                .Clickwraps
                .FirstOrDefault(x => x.ClickwrapName == ClickWrapName);

            if (clickWrap == null)
            {
                throw new ClickWrapNotFoundException(ClickWrapName);
            }

            return clickWrap;
        }
    }
}