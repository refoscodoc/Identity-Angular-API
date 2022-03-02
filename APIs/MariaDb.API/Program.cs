using MariaDb.API.DataAccess;
using MariaDb.API.DataAccess.Interfaces;
using MariaDb.API.Services;
using Microsoft.EntityFrameworkCore;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddRazorPages();

var sqlConnectionString = builder.Configuration.GetConnectionString("DataAccessMySqlProvider");
var serverVersion = new MySqlServerVersion(new Version(10, 6, 7));

builder.Services.AddDbContext<MariaDbDataAccess>(options =>
    options.UseMySql(
        sqlConnectionString, serverVersion)
);

builder.Services.AddTransient<IMariaDbDataAccessProviderProduct, MariaDbDataAccessProviderProduct>();
builder.Services.AddTransient<IMariaDbDataAccessProviderUser, MariaDbDataAccessProviderUser>();
builder.Services.AddScoped<BusinessProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.Authority = builder.Configuration["IdentityServerAddress"];
    o.Audience = "MariaApiApp";
    o.Authority = "https://localhost:5003";
    o.RequireHttpsMetadata = false;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = false
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins(builder.Configuration["ClientAddress"])
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseAuthentication();

// app.MapRazorPages();
app.MapControllers().RequireAuthorization();

app.Run();