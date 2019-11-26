using Newtonsoft.Json;

namespace ColaTerminal.Models.Paypal
{
    public class TransactionDetail
    {
        [JsonProperty("transaction_info")]
        public TransactionInfo TransactionInfo { private set; get; }
    }
}