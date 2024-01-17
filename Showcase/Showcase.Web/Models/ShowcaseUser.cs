using Microsoft.AspNetCore.Identity;

namespace Showcase.Web.Models
{
    public class ShowcaseUser : IdentityUser
    {
        public ICollection<ChatMessage> Messages { get; set; }
    }
}
