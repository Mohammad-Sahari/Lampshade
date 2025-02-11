using Newtonsoft.Json;

namespace _01_Framework.Application.ZarinPal;

public class VerificationError
{
    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("code")]
    public int Code { get; set; }
}