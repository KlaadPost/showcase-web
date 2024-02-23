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
        private readonly ShowcaseWebContext _dbContext;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly UserManager<ShowcaseUser> _userManager;
        private readonly ILogger<ChatController> _logger;
        

        public ChatController(ShowcaseWebContext dbContext, IHubContext<ChatHub> hubContext, UserManager<ShowcaseUser> userManager, ILogger<ChatController> logger)
        {
            _dbContext = dbContext;
            _hubContext = hubContext;
            _userManager = userManager;
            _logger = logger;
        }

        // GET /Chat
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.ChatMessages.OrderBy(m => m.Created).ToListAsync());
        }

        // GET /Chat/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> Messages(int pageIndex = 0, int pageSize = 20)
        {
            var showcaseWebContext = _dbContext.ChatMessages;

            // Calculate the number of items to skip based on pageIndex and pageSize
            int itemsToSkip = pageIndex * pageSize;

            // Retrieve the paginated set of messages
            var messages = await showcaseWebContext
                .OrderByDescending(m => m.Created)
                .Skip(itemsToSkip)
                .Take(pageSize)
                .OrderBy(m => m.Created)
                .ToListAsync();

            return messages;
        }

        // POST /Chat
        [HttpPost]
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

            _dbContext.Add(createdChatMessage);

            try
            {
                await _dbContext.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", createdChatMessage);
                _logger.LogInformation($"Chat message sent by user {currentUser.Id} ({currentUser.UserName}): {createdChatMessage.Message}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error sending message for user {currentUser.Id} ({currentUser.UserName}): {createdChatMessage.Message}");
                return StatusCode(500, "Unable to send message. Please try again later.");
            }

            return Ok("Chat message successfully created");
        }

        // DELETE /Chat
        [HttpDelete]
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
                var existingMessage = await _dbContext.ChatMessages.FirstOrDefaultAsync(m => m.Id == deleteModel.Id);

                if (existingMessage == null)
                {
                    return NotFound("Chat message not found");
                }

                if (existingMessage.SenderId != currentUser.Id) // Needs Role check in future. 
                {
                    _logger.LogWarning($"Unauthorized attempt to delete other people's messages by user {currentUser.Id} ({currentUser.UserName}). ChatMessageId: {deleteModel.Id}");
                    return Unauthorized("You cannot delete other people's messages");
                }

                _dbContext.ChatMessages.Remove(existingMessage);
                await _dbContext.SaveChangesAsync();
                await _hubContext.Clients.All.SendAsync("MessageDeleted", deleteModel.Id);

                _logger.LogInformation($"Chat message deleted by user {currentUser.Id} ({currentUser.UserName}) ChatMessageId: {deleteModel.Id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting chat message for user {currentUser.Id} ({currentUser.UserName}). ChatMessageId: {deleteModel.Id}");
                return StatusCode(500, "Unable to delete message. Please try again later.");
            }

            return Ok("Chat message successfully deleted.");
        }

    }
}
