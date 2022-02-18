using MariaDb.API.DataAccess;
using MariaDb.API.DataAccess.Interfaces;
using MariaDb.API.Services;
using Microsoft.EntityFrameworkCore;

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


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// app.MapRazorPages();
app.MapControllers();

app.Run();