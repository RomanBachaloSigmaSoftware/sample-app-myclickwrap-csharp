using DocuSign.MyClickwrap.TermsAndConditions.Domain;
using DocuSign.MyClickwrap.TermsAndConditions.Models;
using DocuSign.MyClickwrap.TermsAndConditions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocuSign.MyClickwrap.TermsAndConditions.Controllers
{
    [Authorize]
    [ApiController]
    [Route("/api/[controller]")]
    public class ClickWrapController : Controller
    {
        private readonly IClickWrapService _clickWrapService;

        public ClickWrapController(IClickWrapService clickWrapService)
        {
            _clickWrapService = clickWrapService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ClickWrap clickWrap = _clickWrapService.GetClickWrap(Context.Account.Id, Context.User.Id);

            return Ok(
                new ResponseClickWrapModel
                {
                    ClickWrap = clickWrap,
                    DocuSignBaseUrl = Context.Account.BaseUri,
                    AccountId = Context.Account.Id,
                    UserId = Context.User.Id
                });
        }
    }
}