using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Showcase.Web.Data;
using Showcase.Web.Models;

namespace Showcase.Web.Controllers
{
    //[Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ShowcaseWebContext _context;
        private readonly ILogger<AdminController> _logger;
        private readonly UserManager<ShowcaseUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(ShowcaseWebContext context, ILogger<AdminController> logger, UserManager<ShowcaseUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _logger = logger;

            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var userViewModelList = new List<ShowcaseUserViewModel>();
            foreach (var user in await _context.Users.ToListAsync())
            {
                var userViewModel = new ShowcaseUserViewModel(user, _roleManager.NormalizeKey(user.Id));
                userViewModelList.Add(userViewModel);   
            }
            return View(userViewModelList);
        }

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

            var role = _roleManager.NormalizeKey(user.Id);
            var userViewModel = new ShowcaseUserViewModel(user, role);

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserName, Role, EmailConfirmed, Muted")] ShowcaseUserViewModel userViewModel)
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
                    user.Email = userViewModel.Email;
                    user.EmailConfirmed = userViewModel.EmailConfirmed;
                    user.Muted = userViewModel.Muted;

                    // Retrieve user's current roles
                    var currentRoles = await _userManager.GetRolesAsync(user);

                    // Remove user from all current roles
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);

                    // Add user to the selected role
                    await _userManager.AddToRoleAsync(user, userViewModel.Role.ToString());

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }

            return View(userViewModel);
        }
    }
}
