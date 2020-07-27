using RestSharp;

namespace DocuSign.MyClick.COVID19Waiver.Services
{
    public interface IDocuSignApiProvider
    {
        IRestClient DocuSignClickApiRestClient { get; }
    }
}