using Microsoft.AspNetCore.Mvc;
using Showcase.Web.Models;
using Showcase.Web.Services;

namespace Showcase.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IRecaptchaService _reCaptchaService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IContactService emailService, IRecaptchaService reCaptchaService, ILogger<ContactController> logger)
        {
            _contactService = emailService;
            _reCaptchaService = reCaptchaService;
            _logger = logger;
        }

        // GET /Contact
        public IActionResult Index()
        {
            return View();
        }

        // POST /Contact
        [HttpPost]
        public async Task<IActionResult> Index([FromBody] ContactModel contactData)
        {
            try
            {
                var isTokenValid = await _reCaptchaService.ValidateToken(contactData.RecaptchaToken);

                if (!isTokenValid)
                {
                    _logger.LogWarning("ReCaptcha validation failed for contact request. Request data: {@ContactData}", contactData);
                    ModelState.AddModelError("RecaptchaToken", "Your request has been flagged as suspicious, please try again later");
                    return BadRequest(ModelState);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                await _contactService.SendContactRequest(contactData);

                _logger.LogInformation("Contact request successfully sent. Request data: {@ContactData}", contactData);
                return Ok("Contact request has been sent");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during contact request processing. Request data: {@ContactData}", contactData);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
