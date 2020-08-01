using DocuSign.MyClick.COVID19Waiver.Domain;
using DocuSign.MyClick.COVID19Waiver.Models;
using DocuSign.MyClick.COVID19Waiver.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocuSign.MyClick.COVID19Waiver.Controllers
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