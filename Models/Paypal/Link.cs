using Newtonsoft.Json;

namespace ColaTerminal.Models.Paypal
{
    public class Link
    {
        [JsonProperty("href")]
        public string Href { private set; get; }
        [JsonProperty("rel")]
        public string Rel { private set; get; }
        [JsonProperty("method")]
        public string Method { private set; get; }
    }
}