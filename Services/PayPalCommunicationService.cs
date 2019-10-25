using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ColaTerminal.Models.Paypal;

namespace ColaTerminal.Services
{
    public class PayPalCommunicationService
    {
        private const string BasicUrl = "https://api.paypal.com/";
        private const string AuthEndpoint = "v1/oauth2/token";

        private DateTime _invalidationTime;
        private string _accessToken;

        private readonly IHttpClientFactory _clientFactory;

        public PayPalCommunicationService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task AuthorizeAsync(string client, string secret)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (secret == null) throw new ArgumentNullException(nameof(secret));

            // Append Basic-Auth data
            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{client}:{secret}"));
            var httpClient = _clientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            // Build form-data
            var formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };
            var requestBody = new FormUrlEncodedContent(formData);

            // Send request and extract values
            var result = await httpClient.PostAsync(BasicUrl + AuthEndpoint, requestBody);
            if (!result.IsSuccessStatusCode)
            {
                throw new Exception("Error while receiving authorization-data");
            }

            var content = await result.Content.ReadAsAsync<AuthorizationResponse>();
            if (content == null)
            {
                throw new Exception("Could not parse content");
            }

            if (string.IsNullOrEmpty(content.AccessToken))
            {
                throw new Exception("No access-token given");
            }

            // Update token and validation-time
            _invalidationTime = DateTime.Now.Add(TimeSpan.FromSeconds(content.ExpiresIn));
            _accessToken = content.AccessToken;
        }
    }
}