
using System.Text.Json.Serialization;
namespace ApiResultLibrary;

public class ApiResult<T>
{
    [JsonPropertyName("value")]
    public T? Value { get; set; }

    [JsonPropertyName("err")]
    public NiaveWhoops? Err { get; set; }
}
