using RestSharp;

namespace DocuSign.MyClick.NonDisclosureAgreement.Services
{
    public interface IDocuSignApiProvider
    {
        IRestClient DocuSignClickApiRestClient { get; }
    }
}