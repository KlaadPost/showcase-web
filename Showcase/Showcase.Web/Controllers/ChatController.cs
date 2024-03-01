using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Showcase.Web.Data;
using Showcase.Web.Hubs;
using Showcase.Web.Models;

namespace Showcase.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ChatController : Controller
    {
        // GET /Chat
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}
