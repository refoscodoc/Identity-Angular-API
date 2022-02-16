using System.Reflection;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Identity.App.Data;
using Identity.App.Models;

var builder = WebApplication.CreateBuilder(args);

var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

var connectionString = builder.Configuration.GetConnectionString("DataAccessMySqlProvider");
var serverVersion = new MySqlServerVersion(new Version(10, 6, 7));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(
        connectionString, serverVersion)
);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddAspNetIdentity<ApplicationUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = config =>
            config.UseMySql(connectionString, serverVersion,
                db => db.MigrationsAssembly(migrationsAssembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = config =>
            config.UseMySql(connectionString, serverVersion, 
                db => db.MigrationsAssembly(migrationsAssembly));
    });

builder.Services.AddAuthentication()
    .AddIdentityServerJwt();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html");
;

app.Run();