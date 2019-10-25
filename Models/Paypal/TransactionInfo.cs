using System;
using Newtonsoft.Json;

namespace ColaTerminal.Models.Paypal
{
    public class TransactionInfo
    {
        [JsonProperty("paypal_account_id")]
        public string PaypalAccountId { private set; get; }
        [JsonProperty("transaction_id")]
        public string TransactionId { private set; get; }
        [JsonProperty("transaction_event_code")]
        public string TransactionEventCode { private set; get; }
        [JsonProperty("transaction_initiation_date")]
        public DateTime TransactionInitiationDate { private set; get; }
        [JsonProperty("transaction_updated_date")]
        public DateTime TransactionUpdatedDate { private set; get; }
        [JsonProperty("transaction_amount")]
        public TransactionAmount TransactionAmount { private set; get; }
        [JsonProperty("transaction_status")]
        public string TransactionStatus { private set; get; }
        [JsonProperty("ending_balance")]
        public EndingBalance EndingBalance { private set; get; }
        [JsonProperty("available_balance")]
        public AvailableBalance AvailableBalance { private set; get; }
        [JsonProperty("protection_eligibility")]
        public string ProtectionEligibility { private set; get; }
    }
}