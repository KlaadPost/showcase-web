namespace Showcase.Web.Services
{
    public interface IRecaptchaService
    {
        Task<bool> ValidateToken(string token);
    }
}
