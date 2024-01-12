namespace Showcase.Web.Services
{
    /// <summary>
    /// A mock implementation of the Recaptcha Service. 
    /// `ValidateToken` will return true when the token is equal to "1234567890"
    /// </summary>
    public class RecaptchaMockService : IRecaptchaService
    {
        public Task<bool> ValidateToken(string token)
        {
            return Task.FromResult(token == "1234567890");
        }
    }
}
