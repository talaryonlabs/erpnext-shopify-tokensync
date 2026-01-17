using System.Text.Json.Serialization;

namespace ERPNextShopifyTokensync;

public class ErpNextApiResponse<T>
{
    [JsonPropertyName("data")]
    public T Data { get; set; }
}