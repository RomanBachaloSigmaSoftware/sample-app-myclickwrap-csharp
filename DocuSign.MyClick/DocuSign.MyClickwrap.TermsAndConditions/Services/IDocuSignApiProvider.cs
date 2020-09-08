using RestSharp;

namespace DocuSign.MyClickwrap.TermsAndConditions.Services
{
    public interface IDocuSignApiProvider
    {
        IRestClient DocuSignClickApiRestClient { get; }
    }
}