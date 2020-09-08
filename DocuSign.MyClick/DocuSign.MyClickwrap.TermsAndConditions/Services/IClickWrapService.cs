using DocuSign.MyClickwrap.TermsAndConditions.Domain;

namespace DocuSign.MyClickwrap.TermsAndConditions.Services
{
    public interface IClickWrapService
    {
        ClickWrap GetClickWrap(string accountId, string userId);
    }
}