using Newtonsoft.Json;

namespace _01_Framework.Application.ZarinPal
{
    //public class PaymentResponse
    //{
    //    public int Status { get; set; }
    //    public string Authority { get; set; }
    //}
    public class PaymentResponse
    {
        [JsonProperty("data")]
        public PaymentData Data { get; set; }

        [JsonProperty("errors")]
        public List<PaymentError> Errors { get; set; }
    }

    public class PaymentData
    {
        [JsonProperty("code")]
        public int Status { get; set; }

        [JsonProperty("authority")]
        public string Authority { get; set; }
    }

    public class PaymentError
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }
    }
}
