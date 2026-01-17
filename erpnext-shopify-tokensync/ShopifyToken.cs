using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace ERPNextShopifyTokensync;

public class ShopifyToken
{
    [JsonPropertyName("scope")] public string Scope { get; set; }
    [JsonPropertyName("expires_in")] public int ExpiresIn { get; set; }
    [JsonPropertyName("access_token")] public string AccessToken { get; set; }
    
    public static Task<ShopifyToken?> RequestTokenAsync(string shopifyUrl, string clientId, string clientSecret)
    {
        var client = new HttpClient();
        var content = new FormUrlEncodedContent([new KeyValuePair<string, string>("grant_type", "client_credentials"), new KeyValuePair<string, string>("client_id", clientId), new KeyValuePair<string, string>("client_secret", clientSecret)
        ]);
        var response = client.PostAsync($"https://{shopifyUrl}/admin/oauth/access_token", content).Result;
        return response.Content.ReadFromJsonAsync<ShopifyToken>();
    }
}