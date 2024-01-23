using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
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
        public async Task<IActionResult> Index()
        {
            var showcaseWebContext = _context.ChatMessages;
            return View(await showcaseWebContext.OrderBy(m => m.Created).ToListAsync());
        }

        // GET /Chat/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> Messages()
        {
            var showcaseWebContext = _context.ChatMessages;
            return await showcaseWebContext.OrderBy(m => m.Created).ToListAsync();
        }

        // POST /Chat
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromBody] ChatMessageCreateModel createModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null || currentUser.UserName == null)
            {
                return BadRequest("Please log in before sending a chat message");
            }

            var createdChatMessage = new ChatMessage()
            {
                Message = createModel.Message,
                SenderId = currentUser.Id,
                SenderName = currentUser.UserName,
            };

            _context.Add(createdChatMessage);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, "Unable to send message. Please try again later.");
            }

            return Ok("Chat message successfully created");
        }

        // PUT /Chat
        [HttpPut]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromBody] ChatMessageEditModel editModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return BadRequest("Please log in before editing a chat message");
            }

            try
            {
                var existingMessage = await _context.ChatMessages.FirstOrDefaultAsync(m => m.Id == editModel.Id);

                if (existingMessage == null)
                {
                    return NotFound("Chat message not found");
                }

                if (existingMessage.SenderId != currentUser.Id)
                {
                    return Unauthorized("You cannot edit other people's messages");
                }

                existingMessage.Message = editModel.Message;
                existingMessage.Updated = DateTime.UtcNow;

                _context.Update(existingMessage);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, "Unable to edit message. Please try again later.");
            }

            return Ok("Chat message successfully edited.");
        }


        // DELETE /Chat
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromBody] ChatMessageDeleteModel deleteModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser == null)
            {
                return BadRequest("Please log in before deleting a chat message");
            }

            try
            {
                var existingMessage = await _context.ChatMessages.FirstOrDefaultAsync(m => m.Id == deleteModel.Id);

                if (existingMessage == null)
                {
                    return NotFound("Chat message not found");
                }

                if (existingMessage.SenderId != currentUser.Id)
                {
                    return Unauthorized("You cannot delete other people's messages");
                }

                _context.ChatMessages.Remove(existingMessage);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(500, "Unable to delete message. Please try again later.");
            }

            return Ok("Chat message successfully deleted.");
        }
    }
}
