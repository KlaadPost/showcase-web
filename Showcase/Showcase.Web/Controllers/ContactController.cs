using Microsoft.AspNetCore.Mvc;
using Showcase.Web.Models;
using Showcase.Web.Services;

namespace Showcase.Web.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class ContactController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IRecaptchaService _reCaptchaService;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IEmailService emailService, IRecaptchaService reCaptchaService, ILogger<ContactController> logger)
        {
            _emailService = emailService;
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

                // Send contact email
                var emailSent = await _emailService.SendEmail(contactData);

                if (emailSent)
                {
                    _logger.LogInformation("Contact request successfully sent. Request data: {@ContactData}", contactData);
                    return Ok("Contact request has been sent");
                }
                else
                {
                    return StatusCode(500, "Unable to send a contact request, please try again later");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error during contact request processing. Request data: {@ContactData}", contactData);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
