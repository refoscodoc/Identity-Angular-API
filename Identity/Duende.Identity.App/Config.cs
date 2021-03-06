using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Duende.Identity.App;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("MariaApiApp"),
            new ApiScope("MongoDbApiApp"),
            new ApiScope("GoogleSheets"),
            new ApiScope("api1"),
        };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            // machine to machine client (from quickstart 1)
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.ClientCredentials,
                // scopes that client has access to
                AllowedScopes = { "api1" }
            },
            // interactive ASP.NET Core Web App
            new Client
            {
                ClientId = "web",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
            
                // where to redirect to after login
                RedirectUris = { "https://localhost:7123/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:7123/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile
                }
            },
            // interactive ASP.NET Core MVC client
            new Client
            {
                ClientId = "mvc",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                    
                // where to redirect to after login
                RedirectUris = { "https://localhost:5002/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api1"
                }
            },
            new Client
            {
                ClientId = "ng",
                ClientName = "Angular Client",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                RequireConsent = true,

                RedirectUris = { "http://localhost:5002/callback" },
                PostLogoutRedirectUris = { "http://localhost:5002/home" },
                AllowedCorsOrigins = { "http://localhost:5002" },

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "apiApp"
                },

            }
            
        };
}