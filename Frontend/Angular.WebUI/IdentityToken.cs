using System.Text.Json;
using IdentityModel.Client;

namespace Angular.WebUI;

public static class IdentityToken
{
    public static async void RequestToken()
    {
        var client = new HttpClient();

        var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5003");
        if (disco.IsError)
        {
            Console.WriteLine(disco.Error);
            return;
        }

// request token
        var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
        {
            Address = disco.TokenEndpoint,
            ClientId = "client",
            ClientSecret = "secret",

            Scope = "MariaApiApp"
        });

        if (tokenResponse.IsError)
        {
            Console.WriteLine(tokenResponse.Error);
            Console.WriteLine(tokenResponse.ErrorDescription);
            return;
        }

        Console.WriteLine(tokenResponse.Json);
        Console.WriteLine("\n\n");

// call api
        var apiClient = new HttpClient();
        apiClient.SetBearerToken(tokenResponse.AccessToken);

        var response = await apiClient.GetAsync("https://localhost:6003/identity");
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.StatusCode);
        }
        else
        {
            var doc = JsonDocument.Parse(await response.Content.ReadAsStringAsync()).RootElement;
            Console.WriteLine(JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true }));
        }
    }
}