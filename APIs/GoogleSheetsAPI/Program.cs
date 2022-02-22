using GoogleSheetsAPI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using MongoDb.API.DataAccess;
using MongoDb.API.Services;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<GoogleSheets>();
builder.Services.AddScoped<BusinessProvider>();
builder.Services.AddScoped<TickerContext>();

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    }); 

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie(options =>
    {
        options.LoginPath = "/google-login";
        options.AccessDeniedPath = "/denied";
        // options.LogoutPath = "/signout"; // ??
        // options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.CookieManager = new ChunkingCookieManager();

        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.None;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    }).AddGoogle(options =>
    {
        options.ClientId = "337517271573-nnhgef96pd048pdlec3ner6m3g0j5f5o.apps.googleusercontent.com";
        options.ClientSecret = "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";
        options.SaveTokens = true;
        options.CallbackPath = "/auth";
        options.AuthorizationEndpoint += "?prompt=consent";
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => {
        builder.WithOrigins("https://localhost","https://accounts.google.com")
            .AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.MapControllers();

app.Run();