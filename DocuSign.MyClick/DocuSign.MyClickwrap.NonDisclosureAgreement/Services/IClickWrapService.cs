using DocuSign.MyClickwrap.NonDisclosureAgreement.Domain;

namespace DocuSign.MyClickwrap.NonDisclosureAgreement.Services
{
    public interface IClickWrapService
    {
        ClickWrap GetClickWrap(string accountId);
    }
}