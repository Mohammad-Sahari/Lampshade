using Newtonsoft.Json;

namespace _01_Framework.Application.ZarinPal
{
    public class VerificationRequest
    {
        [JsonProperty("merchant_id")]
        public string merchant_id { get; set; }
        [JsonProperty("authority")]
        public string authority { get; set; }
        [JsonProperty("amount")]
        public int amount { get; set; }
    }
}
