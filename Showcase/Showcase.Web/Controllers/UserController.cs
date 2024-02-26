using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Showcase.Web.Data;
using Showcase.Web.Models;

namespace Showcase.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ShowcaseUser> _userManager;

        public UserController(UserManager<ShowcaseUser> userManager)
        {
            _userManager = userManager;
        }

        // GET: Api/User
        [HttpGet]
        public async Task<ActionResult<ShowcaseUserViewModel>> GetUser()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound("No user is currently logged in");
            }

            var roles = await _userManager.GetRolesAsync(currentUser);
            var role = roles.FirstOrDefault();

            return new ShowcaseUserViewModel(currentUser, role ?? "None");
        }
    }
}
