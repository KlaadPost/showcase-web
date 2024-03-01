using Microsoft.AspNetCore.Mvc;

namespace Showcase.Web.Controllers
{
    public class ProfileController : Controller
    {
        // GET: /Profile
        public IActionResult Index()
        {
            return View();
        }
    }
}
