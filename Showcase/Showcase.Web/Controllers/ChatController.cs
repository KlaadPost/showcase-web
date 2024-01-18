using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Showcase.Web.Data;
using Showcase.Web.Models;

namespace Showcase.Web.Controllers
{
    public class ChatController : Controller
    {
        private readonly ShowcaseWebContext _context;
        private readonly UserManager<ShowcaseUser> _userManager;

        public ChatController(ShowcaseWebContext context, UserManager<ShowcaseUser> userManager)
        {
            _context = context;
            _userManager = userManager; 
        }

        // GET /Chat
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var showcaseWebContext = _context.ChatMessages;
            return View(await showcaseWebContext.OrderBy(m => m.Created).ToListAsync());
        }

        // POST /Chat
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromBody] ChatMessageCreateModel chatMessage)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null) 
            {
                return BadRequest("Please log in first before sending a chatmessage");
            }

            try
            {
                var newChatMessage = new ChatMessage()
                {
                    Message = chatMessage.Message,
                    SenderId = currentUser.Id,
                    SenderName = currentUser.UserName,
                };

                _context.Add(newChatMessage);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, "Unable to send message, please try again later");
            }

            return Ok("Chatmessage sent: " + chatMessage.Message);
        }

        // PUT /Chat
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromBody] ChatMessageEditModel chatMessage)
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return BadRequest("Please log in first before editing a chatmessage");
            }



            if (chatMessage == null) { }

            return StatusCode(500, $"Edited message: {chatMessage} into {chatMessage}");
        }

        // DELETE /Chat

    }
}
