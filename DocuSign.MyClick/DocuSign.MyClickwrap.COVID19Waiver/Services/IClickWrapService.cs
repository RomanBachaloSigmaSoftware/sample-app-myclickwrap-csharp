using DocuSign.MyClickwrap.COVID19Waiver.Domain;

namespace DocuSign.MyClickwrap.COVID19Waiver.Services
{
    public interface IClickWrapService
    {
        ClickWrap GetClickWrap(string accountId);
    }
}