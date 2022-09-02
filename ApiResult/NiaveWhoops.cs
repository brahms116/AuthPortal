using System.Text.Json.Serialization;

namespace ApiResultLibrary;

public class NiaveWhoops
{


    [JsonPropertyName("errType")]
    public string ErrType { get; set; } = "unknown-err-type";

    [JsonPropertyName("context")]
    public string Context { get; set; } = "No context given.";

    [JsonPropertyName("reason")]
    public string Reason { get; set; } = "No reason given.";

    [JsonPropertyName("suggestion")]
    public string Suggestion { get; set; } = "No suggestions given.";
    
}