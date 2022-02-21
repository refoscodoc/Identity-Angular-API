using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient("MongoDbApi", client =>
{
    client.BaseAddress = new Uri("http://localhost:5002/"); // MongoDb.API port
});

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddOpenIdConnect(options =>
    {
        options.Authority = builder.Configuration["IdentityServerAddress"];
        options.RequireHttpsMetadata = false;

        options.ClientId = "mvc";
        options.ClientSecret = "secret";

        options.ResponseType = "code id_token";
        options.Scope.Add("apiApp");
        options.Scope.Add("offline_access");

        options.GetClaimsFromUserInfoEndpoint = true;
        options.SaveTokens = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

// app.UseSpa(spa =>
// {
//     // To learn more about options for serving an Angular SPA from ASP.NET Core,
//     // see https://go.microsoft.com/fwlink/?linkid=864501
//
//     spa.Options.SourcePath = "ClientApp";
//
//     if (env.IsDevelopment())
//     {
//         spa.UseAngularCliServer(npmScript: "start");
//     }
// });

app.MapFallbackToFile("index.html");

app.Run();