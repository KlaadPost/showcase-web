using Showcase.Web.Data;
using Showcase.Web.Models;

namespace Showcase.Test.Helpers
{
    /// <summary>
    /// Helper class for initializing and reinitializing the database for testing purposes.
    /// </summary>
    public static class DbHelper
    {
        /// <summary>
        /// Initializes the database with seeding messages for testing.
        /// </summary>
        /// <param name="db">The database context.</param>
        public static void InitializeDbForTests(ShowcaseWebContext db)
        {
            db.ChatMessages.AddRange(GetSeedingMessages());
            db.SaveChanges();
        }

        /// <summary>
        /// Reinitializes the database by removing all chat messages and adding seeding messages for testing.
        /// </summary>
        /// <param name="db">The database context.</param>
        public static void ReinitializeDbForTests(ShowcaseWebContext db)
        {
            db.ChatMessages.RemoveRange(db.ChatMessages);
            InitializeDbForTests(db);
        }

        /// <summary>
        /// Gets a list of seeding messages for testing purposes.
        /// </summary>
        /// <returns>A list of chat messages.</returns>
        public static List<ChatMessage> GetSeedingMessages()
        {
            return
            [
                new()
                {
                    Message = "This shit ain't nothing to me man.",
                    SenderId = "DraculaId",
                    SenderName = "Dracula@flow.com",
                },
                new()
                {
                    Message = "The Smith & Wesson got me moving like an invasive species.",
                    SenderId = "DraculaId",
                    SenderName = "Dracula@flow.com",
                },
                new()
                {
                    Message = "My diamonds come from the worst situations possible.",
                    SenderId = "DraculaId",
                    SenderName = "Dracula@flow.com",
                },
            ];
        }
    }
}
