using Newtonsoft.Json;
using Showcase.Web.Models;

namespace Showcase.Web.Services
{
    public class RecaptchaService : IRecaptchaService 
    {
        private readonly string _recaptchaSecretKey;
        private readonly HttpClient _httpClient;

        public RecaptchaService(string recaptchaSecretKey, HttpClient httpClient)
        {
            _recaptchaSecretKey = recaptchaSecretKey;
            _httpClient = httpClient;
        }

        public async Task<bool> ValidateToken(string token)
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string, string>("secret", _recaptchaSecretKey),
                new KeyValuePair<string, string>("response", token)
                });

                var response = await _httpClient.PostAsync("https://www.google.com/recaptcha/api/siteverify", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var reCaptchaResponse = JsonConvert.DeserializeObject<RecaptchaResponseModel>(responseContent);

                    return reCaptchaResponse.Success;
                }
                else
                {
                    Console.WriteLine("Failed to validate reCAPTCHA token");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error validating reCAPTCHA token: {ex.Message}");
                return false;
            }
        }
    }
}
