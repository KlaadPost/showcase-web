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

        public ContactController(EmailService emailService)
        {
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] ContactModel contactData)
        {
            Console.WriteLine(contactData);

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
