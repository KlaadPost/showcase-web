using Microsoft.AspNetCore.Mvc;
using Showcase.Web.Models;
using Showcase.Web.Services;

namespace Showcase.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IRecaptchaService _reCaptchaService;

        public ContactController(IEmailService emailService, IRecaptchaService reCaptchaService)
        {
            _emailService = emailService;
            _reCaptchaService = reCaptchaService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] ContactModel contactData)
        {
            var isTokenValid = await _reCaptchaService.ValidateToken(contactData.RecaptchaToken);

            if (!isTokenValid)
            {
                ModelState.AddModelError("RecaptchaToken", "Uw verzoek is verdacht gevonden, probeer het later opnieuw");
                return BadRequest(ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emailSent = await _emailService.SendEmail(contactData);

            if (emailSent)
            {
                return Ok("Contactverzoek is verstuurd");
            }
            else
            {
                return StatusCode(500, "Kon contactverzoek niet versturen vanwege interne fout, probeer het later opnieuw");
            }
        }

    }
}
