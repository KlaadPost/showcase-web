using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Showcase.Web.Models
{
    public class ChatMessage // Base model for messages 
    {
        [Key]
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public required string SenderName { get; set; }
        public required string SenderId { get; set; }

        public required string Message { get; set; }

        public ChatMessage() 
        {
            Id = Guid.NewGuid().ToString();
            Created = DateTime.UtcNow;
            Updated = Created;
        }
    }

    public class ChatMessageCreateModel // Model for creating Messages
    {
        [Required]
        [Length(1, 700)]
        public string Message { get; set; }
    }

    public class ChatMessageEditModel // Model for editing Messages
    {
        [Required]
        public string Id { get; set; }

        [Required]
        [Length(1, 700)]
        public string Message { get; set; }
    }

    public class ChatMessageDeleteModel // Model for deleting messages 
    {
        [Required]
        public string Id { get; set; }
    }
}
