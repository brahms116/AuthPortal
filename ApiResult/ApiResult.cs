
using System.Text.Json.Serialization;
namespace ApiResultLibrary;

public class ApiResult<T>
{

    private ApiResult()
    {
    }

    [JsonPropertyName("value")]
    public T? Value { get; set; }

    [JsonPropertyName("err")]
    public NiaveWhoops? Err { get; set; }

    public static ApiResult<T> Ok(T value) { 
        return new ApiResult<T> { Value = value };
    }

    public static ApiResult<T> Error(NiaveWhoops err) {
        return new ApiResult<T> { Err = err };
    }
}



