using RestSharp;

namespace DocuSign.MyClickwrap.NonDisclosureAgreement.Services
{
    public interface IDocuSignApiProvider
    {
        IRestClient DocuSignClickApiRestClient { get; }
    }
}