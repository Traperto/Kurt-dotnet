using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ColaTerminal.Models.Paypal;

namespace ColaTerminal.Services
{
    public class NetworkService
    {
        private const string BasicUrl = "https://api.paypal.com/";
        private const string AuthUrl = "v1/oauth2/token";

        private DateTime _invalidationTime;
        private string _accessToken;

        private readonly IHttpClientFactory _clientFactory;

        public NetworkService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        public async Task AuthorizeAsync(string client, string secret)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (secret == null) throw new ArgumentNullException(nameof(secret));

            var request = new HttpRequestMessage(HttpMethod.Post, BasicUrl + AuthUrl);
            request.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{client}:{secret}"));
            var httpClient = _clientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authToken);

            var formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "client_credentials")
            };

            var result = await httpClient.PostAsync(BasicUrl + AuthUrl, new FormUrlEncodedContent(formData));
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

            _invalidationTime = DateTime.Now.Add(TimeSpan.FromSeconds(content.ExpiresIn));
            _accessToken = content.AccessToken;
        }
    }
}