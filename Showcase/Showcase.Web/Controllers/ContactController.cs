using Microsoft.AspNetCore.Mvc;
using Showcase.Web.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using System.Net.Mail;
using Showcase.Web.Services;

namespace Showcase.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly EmailService _emailService;
        private readonly ReCaptchaService _reCaptchaService;

        public ContactController(EmailService emailService, ReCaptchaService reCaptchaService)
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
            var isTokenValid = await _reCaptchaService.ValidateToken(contactData.ReCaptchaToken);

            if (!isTokenValid)
            {
                ModelState.AddModelError("ReCaptchaToken", "Kon ReCaptcha niet valideren");
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
                return StatusCode(500, "Kon contactverzoek niet versturen.");
            }
        }

    }
}
