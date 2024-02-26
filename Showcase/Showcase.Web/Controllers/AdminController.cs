using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Showcase.Web.Data;
using Showcase.Web.Models;

namespace Showcase.Web.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ShowcaseWebContext _context;
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<ShowcaseUser> _manager;

        public AdminController(ShowcaseWebContext context, ILogger<AdminController> logger, UserManager<ShowcaseUser> userManager)
        {
            _context = context;
            _logger = logger;
            _manager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userViewModelList = new List<ShowcaseUserViewModel>();
            
            foreach (var user in await _context.Users.ToListAsync())
            {
                var roles = await _manager.GetRolesAsync(user);
                var userViewModel = new ShowcaseUserViewModel(user, roles.FirstOrDefault() ?? "None");
                userViewModelList.Add(userViewModel);   
            }
            return View(userViewModelList.OrderByDescending(x => ((int)x.Role)));
        }

        public async Task<ActionResult<ShowcaseUserViewModel>> GetCurrentUserData()
        {
            var currentUser = await _manager.GetUserAsync(User);
            if (currentUser == null)
            {
                return NotFound("No user is currently logged in");
            }

            var roles = await _manager.GetRolesAsync(currentUser);
            var role = roles.FirstOrDefault();

            return new ShowcaseUserViewModel(currentUser, role ?? "None");
        }

        // Get
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await _manager.GetRolesAsync(user);
            var userViewModel = new ShowcaseUserViewModel(user, roles.FirstOrDefault() ?? "None");

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id, UserName, Role, EmailConfirmed, Muted")] ShowcaseUserViewModel userViewModel)
        {
            if (id != userViewModel.Id)
            {
                return NotFound("User not found");
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Update user properties
                    user.UserName = userViewModel.UserName;
                    user.EmailConfirmed = userViewModel.EmailConfirmed;
                    user.Muted = userViewModel.Muted;

                    // Retrieve user's current roles
                    var currentRoles = await _manager.GetRolesAsync(user);

                    // Remove user from all current roles
                    await _manager.RemoveFromRolesAsync(user, currentRoles);

                    // Add user to the selected role
                    await _manager.AddToRoleAsync(user, userViewModel.Role.ToString());

                    // Save changes to the database
                    await _context.SaveChangesAsync();
                    _logger.LogInformation($"Added user {user.Id} ({user.Email}) to {userViewModel.Role} Role");

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Failed to change user role for user {user.Id} ({user.Email})", ex);
                    return BadRequest();
                }
            }

            return View(userViewModel);
        }
    }
}
