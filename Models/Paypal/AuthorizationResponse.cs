using Newtonsoft.Json;

namespace ColaTerminal.Models.Paypal
{
    public class AuthorizationResponse
    {
        [JsonProperty("scope")]
        public string Scope { private set;  get; }
        [JsonProperty("access_token")]
        public string AccessToken { private set; get; }
        [JsonProperty("token_type")]
        public string TokenType { private set; get; }
        [JsonProperty("app_id")]
        public string AppId { private set; get; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { private set; get; }
        [JsonProperty("nonce")]
        public string Nonce { private set; get; }
    }
}