using Microsoft.AspNetCore.Mvc;

namespace Showcase.Web.Controllers
{
    public class PrivacyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
