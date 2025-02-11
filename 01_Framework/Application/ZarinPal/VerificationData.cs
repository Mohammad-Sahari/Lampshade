using Newtonsoft.Json;

namespace _01_Framework.Application.ZarinPal;

public class VerificationData
{
    [JsonProperty("code")]
    public int Status { get; set; }

    [JsonProperty("ref_id")]
    public long RefID { get; set; }
}