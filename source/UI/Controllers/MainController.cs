using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    public class MainController
        : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index() => View();
    }
}