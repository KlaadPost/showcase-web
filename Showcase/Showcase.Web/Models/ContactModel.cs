using System.ComponentModel.DataAnnotations;

namespace Showcase.Web.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "Voornaam is verplicht")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Achternaam is verplicht")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is verplicht")]
        [EmailAddress(ErrorMessage = "Geef een geldig emailadres op")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefoonnummer is verplicht")]
        [Phone(ErrorMessage = "Geef een geldig telefoonnummer op")]
        public string PhoneNumber { get; set; }
    }
}