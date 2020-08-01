using System.Security.Claims;
using System.Threading.Tasks;
using DocuSign.MyClick.TermsAndConditions.Exceptions;
using DocuSign.MyClick.TermsAndConditions.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DocuSign.MyClick.TermsAndConditions.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDocuSignAuthenticationService _docuSignAuthenticationService;

        public AccountController(IDocuSignAuthenticationService docuSignAuthenticationService)
        {
            _docuSignAuthenticationService = docuSignAuthenticationService;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            (ClaimsPrincipal, AuthenticationProperties) authResult;

            try
            {
                authResult = _docuSignAuthenticationService.AuthenticateFromJwt();
            }
            catch (ConsentRequiredException)
            {
                return Redirect(_docuSignAuthenticationService.GetConsentUrl(
                    $"{Request.Scheme}://{Request.Host}/Account/Login"));
            }

            await HttpContext.RequestServices
                .GetRequiredService<IAuthenticationService>()
                .SignInAsync(HttpContext,
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    authResult.Item1,
                    authResult.Item2);

            return returnUrl != null && Url.IsLocalUrl(returnUrl) ? LocalRedirect(returnUrl) : LocalRedirect("/");
        }

        [HttpGet]
        [Route("/api/isauthenticated")]
        public IActionResult IsAuthenticated()
        {
            return Ok(User.Identity.IsAuthenticated);
        }
    }
}