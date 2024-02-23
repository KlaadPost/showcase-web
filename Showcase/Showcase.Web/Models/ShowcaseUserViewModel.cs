using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Showcase.Web.Models
{
    public class ShowcaseUserViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public Role Role { get; set; }

        [BindNever]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

        public bool Muted { get; set; }

        [BindNever]
        public ICollection<ChatMessage>? ChatMessages { get; set; }

        public ShowcaseUserViewModel()
        {
            
        }

        public ShowcaseUserViewModel(ShowcaseUser user, string roleString)
        {
            Enum.TryParse<Role>(roleString, true, out var roleEnum);

            Id = user.Id;
            UserName = user.UserName ?? "";
            Role = roleEnum;

            Email = user.Email ?? "";
            EmailConfirmed = user.EmailConfirmed;

            Muted = user.Muted;
            ChatMessages = user.Messages;
        }
    }

    public enum Role
    {
        None,
        Moderator,
        Administrator,
    }
}
