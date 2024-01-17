using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Showcase.Web.Models
{
    public class ChatMessage
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
            Created = DateTime.Now;
            Updated = Created;
        }
    }
}
