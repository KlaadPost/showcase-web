using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Showcase.Web.Models
{
    public class ChatMessage
    {
        [Key]
        public Guid Id { get; private set; } 

        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }

        public string SenderName { get; set; }
        public string SenderId { get; set; }

        public required string Message { get; set; }

        public ChatMessage() 
        {
        }
    }

    public class ChatMessageCreateModel
    {
        [Required]
        [Length(1, 1000)]
        public required string Message { get; set; }
    }

    public class ChatMessageDeleteModel
    {
        [Required]
        public Guid Id { get; set; }
    }
}
