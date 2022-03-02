using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using MongoDb.API.DataAccess;
using MongoDb.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<TickerContext>();
builder.Services.AddScoped<BusinessProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    // options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // options.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // options.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", o =>
{
    o.Authority = builder.Configuration["IdentityServerAddress"];
    o.Audience = "MongoDbApiApp";
    o.Authority = "https://localhost:5003";
    o.RequireHttpsMetadata = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false
    };
});

builder.Services.AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//services cors
builder.Services.AddCors(p => p.AddPolicy("corspolicy", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("corspolicy");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

// app.MapRazorPages();
app.MapControllers().RequireAuthorization();

app.Run();