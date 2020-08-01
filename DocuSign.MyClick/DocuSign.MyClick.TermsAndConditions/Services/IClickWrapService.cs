using DocuSign.MyClick.TermsAndConditions.Domain;

namespace DocuSign.MyClick.TermsAndConditions.Services
{
    public interface IClickWrapService
    {
        ClickWrap GetClickWrap(string accountId, string userId);
    }
}