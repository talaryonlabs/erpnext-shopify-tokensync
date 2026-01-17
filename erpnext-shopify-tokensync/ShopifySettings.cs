using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ERPNextShopifyTokensync;

public class ShopifySettings
{
    [JsonPropertyName("password")] public string Password { get; set; }
    [JsonPropertyName("shopify_url")] public string ShopifyUrl { get; set; }

    public static async Task<HttpStatusCode> UpdateSettingsAsync(string erpnextUrl, string token, ShopifySettings settings)
    {
        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"token {token}");
        
        var data = new Dictionary<string, ShopifySettings> {{"data", settings}};
        
        var r = JsonSerializer.Serialize(data);
        var res = await client.PutAsync($"https://{erpnextUrl}/api/resource/Shopify%20Setting/Shopify%20Setting", new StringContent(r, Encoding.UTF8, "application/json"));

        return res.StatusCode;
    }
}