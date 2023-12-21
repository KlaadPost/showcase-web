using System.ComponentModel.DataAnnotations;

namespace Showcase.Web.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "Voornaam is verplicht")]
        [MaxLength(255, ErrorMessage = "Maximale lengte voornaam overschreden")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Achternaam is verplicht")]
        [MaxLength(255, ErrorMessage = "Maximale lengte achternaam overschreden")]
        public string LastName { get; set; }

        [MaxLength(255, ErrorMessage = "Maximale lengte email overschreden")]
        [EmailAddress(ErrorMessage = "Geef een geldig emailadres op")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Geef een geldig telefoonnummer op")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "ReCAPTCHA is verplicht")]
        public string RecaptchaToken { get; set; }
    }
}