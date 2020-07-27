using DocuSign.MyClick.COVID19Waiver.Domain;

namespace DocuSign.MyClick.COVID19Waiver.Services
{
    public interface IClickWrapService
    {
        ClickWrap GetClickWrap(string accountId, string userId);
    }
}