using System.Security.Claims;
using System.Threading.Tasks;
using DocuSign.MyClickwrap.TermsAndConditions.Exceptions;
using DocuSign.MyClickwrap.TermsAndConditions.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DocuSign.MyClickwrap.TermsAndConditions.Controllers
{
    public class AccountController : Controller
    {
        private readonly IDocuSignAuthenticationService _docuSignAuthenticationService;

        public AccountController(IDocuSignAuthenticationService docuSignAuthenticationService)
        {
            _docuSignAuthenticationService = docuSignAuthenticationService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
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

            return LocalRedirect($"{HttpContext.Request.PathBase}/");
        }

        [HttpGet]
        [Route("/api/isauthenticated")]
        public IActionResult IsAuthenticated()
        {
            return Ok(User.Identity.IsAuthenticated);
        }
    }
}