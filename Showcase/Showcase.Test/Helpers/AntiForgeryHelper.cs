using AngleSharp;
using Microsoft.Net.Http.Headers;

namespace Showcase.Test.Utilities
{
    /// <summary>
    /// This class provides methods for extracting Anti-Forgery Tokens and adding them to request headers.
    /// </summary>
    public static class AntiForgeryHelper
    {
        // Constants for field name, header name, and cookie name
        public const string FormFieldName = "RequestVerificationField";
        public const string HeaderName = "RequestVerificationToken";
        public const string CookieName = "RequestVerificationCookie";

        /// <summary>
        /// Extracts the Anti-Forgery Cookie from the HTTP response.
        /// </summary>
        private static string ExtractCookie(HttpResponseMessage response)
        {
            var antiForgeryCookie = response.Headers.GetValues("Set-Cookie")
                .FirstOrDefault(cookie => cookie.Contains(CookieName));

            if (antiForgeryCookie is null)
                throw new ArgumentException($"Cookie '{CookieName}' not found in HTTP response", nameof(response));

            return SetCookieHeaderValue.Parse(antiForgeryCookie).Value.ToString();
        }

        /// <summary>
        /// Extracts the Anti-Forgery Token from the HTML body using AngleSharp.
        /// </summary>
        private static string ExtractToken(string htmlBody)
        {
            var context = BrowsingContext.New(AngleSharp.Configuration.Default);
            var document = context.OpenAsync(req => req.Content(htmlBody)).Result;

            var tokenElement = document.QuerySelector($"input[name={FormFieldName}][type=hidden]");

            if (tokenElement == null)
            {
                throw new ArgumentException($"Anti-forgery token '{FormFieldName}' not found in HTML", nameof(htmlBody));
            }

            var token = tokenElement.GetAttribute("value");

            if (token == null)
            {
                throw new ArgumentException($"Anti-Forgery token not found in '{FormFieldName}'");
            }

            return token;
            
        }

        /// <summary>
        /// Extracts both Anti-Forgery Token field value and cookie value from the HTTP response.
        /// </summary>
        public static async Task<(string fieldValue, string cookieValue)> ExtractValues(HttpResponseMessage response)
        {
            var cookie = ExtractCookie(response);
            var token = ExtractToken(await response.Content.ReadAsStringAsync());

            return (fieldValue: token, cookieValue: cookie);
        }
    }
}
