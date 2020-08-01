using DocuSign.MyClick.NonDisclosureAgreement.Domain;

namespace DocuSign.MyClick.NonDisclosureAgreement.Services
{
    public interface IClickWrapService
    {
        ClickWrap GetClickWrap(string accountId, string userId);
    }
}