using System.IdentityModel.Tokens.Jwt;
using Angular.WebUI;

IdentityToken.RequestToken();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddHttpClient("MongoDbApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:5002/"); // MongoDb.API port
});

// builder.Services.AddCors(p => p.AddPolicy("corspolicy", builder =>
// {
//     builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
// }));

builder.Services.AddControllersWithViews();

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = "Cookies";
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5003";

        options.ClientId = "web";
        options.ClientSecret = "secret";
        options.ResponseType = "code";

        options.Scope.Clear();
        options.Scope.Add("openid");
        options.Scope.Add("profile");

        options.SaveTokens = true;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseCors("corspolicy");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages().RequireAuthorization();

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