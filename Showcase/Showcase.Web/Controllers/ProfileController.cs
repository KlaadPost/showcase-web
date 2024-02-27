using Microsoft.AspNetCore.Mvc;

namespace Showcase.Web.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
