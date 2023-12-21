namespace Showcase.Web.Models
{
    public class RecaptchaResponseModel
    {
        public bool Success { get; set; }
        public DateTime ChallengeTs { get; set; }
        public string Hostname { get; set; }
        public string[] ErrorCodes { get; set; }
    }
}
