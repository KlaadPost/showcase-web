using Microsoft.Net.Http.Headers;

namespace Showcase.Test.Utilities
{
    /// <summary>
    /// Helper class for sending POST requests with headers, including anti-forgery tokens.
    /// </summary>
    public static class RequestHelper
    {
        /// <summary>
        /// Sends a POST request with headers, including anti-forgery tokens.
        /// </summary>
        /// <param name="client">The HttpClient instance.</param>
        /// <param name="requestUri">The URI of the request.</param>
        /// <param name="content">The HTTP content to send.</param>
        /// <returns>The response message from the server.</returns>
        public static async Task<HttpResponseMessage> PostAsyncWithHeaders(HttpClient client, string requestUri, HttpContent content)
        {
            // Initial response that will contain the tokens
            var initialResponse = await client.GetAsync(requestUri);

            // Extract the token values from the initial response
            var tokenValues = await AntiForgeryHelper.ExtractValues(initialResponse);

            // Create request
            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Headers.Add(AntiForgeryHelper.HeaderName, tokenValues.fieldValue);
            request.Headers.Add("Cookie", new CookieHeaderValue(AntiForgeryHelper.CookieName, tokenValues.cookieValue).ToString());
            request.Content = content;

            return await client.SendAsync(request);
        }
    }
}
