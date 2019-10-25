using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ColaTerminal.Models.Paypal
{
    public class RootObject
    {
        [JsonProperty("transaction_details")]
        public List<TransactionDetail> TransactionDetails { private set; get; }
        [JsonProperty("account_number")]
        public string AccountNumber { private set; get; }
        [JsonProperty("start_date")]
        public DateTime StartDate { private set; get; }
        [JsonProperty("end_date")]
        public DateTime EndDate { private set; get; }
        [JsonProperty("last_refreshed_datetime")]
        public DateTime LastRefreshedDatetime { private set; get; }
        [JsonProperty("page")]
        public int Page { private set; get; }
        [JsonProperty("total_items")]
        public int TotalItems { private set; get; }
        [JsonProperty("total_pages")]
        public int TotalPages { private set; get; }
        [JsonProperty("links")]
        public List<Link> Links { private set; get; }
    }
}