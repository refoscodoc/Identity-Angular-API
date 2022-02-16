using MariaDb.API.DataAccess;
using MariaDb.API.DataAccess.Interfaces;
using MariaDb.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var sqlConnectionString = builder.Configuration.GetConnectionString("DataAccessMySqlProvider");
var serverVersion = new MySqlServerVersion(new Version(10, 6, 7));

builder.Services.AddDbContext<MariaDbDataAccess>(options =>
    options.UseMySql(
        sqlConnectionString, serverVersion)
);

builder.Services.AddTransient<IMariaDbDataAccessProviderProduct, MariaDbDataAccessProviderProduct>();
builder.Services.AddScoped<BusinessProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();