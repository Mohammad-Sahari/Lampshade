using Newtonsoft.Json;

namespace _01_Framework.Application.ZarinPal
{
    public class VerificationResponse
    {
        [JsonProperty("data")]
        public VerificationData Data { get; set; }

        [JsonProperty("errors")]
        public List<VerificationError> Errors { get; set; }
    }
}
