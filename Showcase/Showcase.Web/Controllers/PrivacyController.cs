using Microsoft.AspNetCore.Mvc;

namespace Showcase.Web.Controllers
{
    public class PrivacyController : Controller
    {
        // GET: Privacy
        public IActionResult Index()
        {
            return View();
        }
    }
}
