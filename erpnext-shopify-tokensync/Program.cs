// See https://aka.ms/new-console-template for more information

using ERPNextShopifyTokensync;

Console.WriteLine("Run TokenSync ...");

var env = Environment.GetEnvironmentVariables();

var shopifyUrl = (string)(env["SHOPIFY_URL"] ?? throw new Exception("SHOPIFY_URL not set"));

var token = await ShopifyToken.RequestTokenAsync(shopifyUrl,
    (string)(env["SHOPIFY_TOKEN"] ?? throw new Exception("SHOPIFY_TOKEN not set")),
    (string)(env["SHOPIFY_SECRET"] ?? throw new Exception("SHOPIFY_SECRET not set")));

var settings = new ShopifySettings {ShopifyUrl = shopifyUrl, Password = token.AccessToken};

var res = await ShopifySettings.UpdateSettingsAsync(
    (string)(env["ERPNEXT_URL"] ?? throw new Exception("ERPNEXT_URL not set")),
    (string)(env["ERPNEXT_TOKEN"] ?? throw new Exception("ERPNEXT_TOKEN not set")),
    settings);


Console.WriteLine(res);