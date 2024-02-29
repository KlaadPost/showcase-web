using System.ComponentModel.DataAnnotations;

namespace Showcase.Web.Models
{
    public class ChatMessage
    {
        [Key]
        public string Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public string SenderName { get; set; }
        public string SenderId { get; set; }

        public required string Message { get; set; }

        public ChatMessage() 
        {
            Id = Guid.NewGuid().ToString();
            Created = DateTime.UtcNow;
            Updated = Created;
        }
    }

    public class ChatMessageCreateModel
    {
        [Required]
        [Length(1, 1000)]
        public required string Message { get; set; }
    }

    public class ChatMessageEditModel
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        [Length(1, 1000)]
        public required string Message { get; set; }
    }

    public class ChatMessageDeleteModel
    {
        [Required]
        public required string Id { get; set; }
    }
}
