using DocuSign.MyClickwrap.COVID19Waiver.Domain;
using DocuSign.MyClickwrap.COVID19Waiver.Models;
using DocuSign.MyClickwrap.COVID19Waiver.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocuSign.MyClickwrap.COVID19Waiver.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
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
            ClickWrap clickWrap = _clickWrapService.GetClickWrap(Context.Account.Id);

            return Ok(
                new ResponseClickWrapModel
                {
                    ClickWrap = clickWrap,
                    DocuSignBaseUrl = Context.Account.BaseUri,
                    AccountId = Context.Account.Id,
                });
        }
    }
}