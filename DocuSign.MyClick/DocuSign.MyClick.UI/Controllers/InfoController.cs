using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocuSign.MyClick.UI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class InfoController : ControllerBase
    {
        [HttpGet]
        [Route("ping")]
        public string Ping()
        {
            return "OK";
        }
    }
}