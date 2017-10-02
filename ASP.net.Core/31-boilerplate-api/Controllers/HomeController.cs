namespace _31_boilerplate_api.Controllers
{
    using _31_boilerplate_api.Constants;
    using Microsoft.AspNetCore.Mvc;

    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ControllerBase
    {
        [HttpGet("", Name = HomeControllerRoute.GetIndex)]
        public IActionResult Index() => this.RedirectPermanent("/swagger");
    }
}
