using Duende.IdentityServer;
using Duende.Identity.App.Data;
using Duende.Identity.App.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Duende.Identity.App;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        
        var sqlConnectionString = builder.Configuration.GetConnectionString("DataAccessMySqlProvider");
        var serverVersion = new MySqlServerVersion(new Version(10, 6, 7));
        
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(sqlConnectionString, serverVersion));

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddRoles<IdentityRole>() // as per MSDN pages regarding Identity
            .AddDefaultTokenProviders();
        
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });

        builder.Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
                options.UserInteraction.LoginUrl = "~/";
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddAspNetIdentity<ApplicationUser>()
            .AddTestUsers(TestUsers.Users);

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                // register your IdentityServer with Google at https://console.developers.google.com
                // enable the Google+ API
                // set the redirect URI to https://localhost:5001/signin-google
                options.ClientId = "318562243956-5n4e3nj5gqffhc1mpa4hsohpfmhjbjqe.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors("CorsPolicy");
        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app.MapRazorPages()
            .RequireAuthorization();
        
        app.MapControllers();

        return app;
    }
}