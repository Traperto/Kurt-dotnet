using Newtonsoft.Json;

namespace ColaTerminal.Models.Paypal
{
    public class EndingBalance
    {
        [JsonProperty("currency_code")]
        public string CurrencyCode { private set; get; }
        [JsonProperty("value")]
        public string Value { private set; get; }
    }
}