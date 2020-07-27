using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace DocuSign.MyClick.COVID19Waiver.Security
{
    public interface IDocuSignAuthenticationService
    {
        (ClaimsPrincipal, AuthenticationProperties) AuthenticateFromJwt();

        string GetConsentUrl(string redirectUrl);
    }
}