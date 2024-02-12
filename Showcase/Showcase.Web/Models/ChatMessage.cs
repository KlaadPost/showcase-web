using System.ComponentModel.DataAnnotations;

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
        [Length(1, 1200)]
        public required string Message { get; set; }
    }

    public class ChatMessageEditModel // Model for editing Messages
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        [Length(1, 700)]
        public required string Message { get; set; }
    }

    public class ChatMessageDeleteModel // Model for deleting messages 
    {
        [Required]
        public required string Id { get; set; }
    }
}
