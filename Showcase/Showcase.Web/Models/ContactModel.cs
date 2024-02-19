using System.ComponentModel.DataAnnotations;

namespace Showcase.Web.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(255, ErrorMessage = "First name exceeds maximum length of 255 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MaxLength(255, ErrorMessage = "Last name exceeds maximum length of 255 characters")]
        public string LastName { get; set; }

        [MaxLength(255, ErrorMessage = "Email exceeds maximum length of 255 characters")]
        [EmailAddress(ErrorMessage = "Please provide a valid email address")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Please provide a valid phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "A Recaptcha token is required")]
        public string RecaptchaToken { get; set; }
    }
}