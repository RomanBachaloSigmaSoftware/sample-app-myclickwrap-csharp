using RestSharp;

namespace DocuSign.MyClickwrap.COVID19Waiver.Services
{
    public interface IDocuSignApiProvider
    {
        IRestClient DocuSignClickApiRestClient { get; }
    }
}