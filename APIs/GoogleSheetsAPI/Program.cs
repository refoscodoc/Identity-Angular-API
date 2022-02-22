using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;

var builder = WebApplication.CreateBuilder(args);

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

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();