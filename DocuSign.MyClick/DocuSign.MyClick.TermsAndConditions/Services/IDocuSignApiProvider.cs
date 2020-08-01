using RestSharp;

namespace DocuSign.MyClick.TermsAndConditions.Services
{
    public interface IDocuSignApiProvider
    {
        IRestClient DocuSignClickApiRestClient { get; }
    }
}