using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using MudBlazor.Services;
using SupportTicketSystem.Bsui.Components;
using SupportTicketSystem.Bsui.Services;
using SupportTicketSystem.Client;
using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Domain.Enums;
using SupportTicketSystem.Shared.DTOs.Auth;
using SupportTicketSystem.Shared.Exceptions;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? throw new InvalidOperationException("API Base URL is not configured.");

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(
        Path.Combine(builder.Environment.ContentRootPath, "keys")))
    .SetApplicationName("TicketSystemApp");

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ServerJwtAccessor>();
builder.Services.AddScoped<Func<Task<string?>>>(sp => sp.GetRequiredService<ServerJwtAccessor>().GetTokenAsync);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/unauthorized";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.Name = "STS.Auth";
    });
builder.Services.AddAuthorization();

// Add Razor Components & Interactive Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register Support Ticket Client Services
builder.Services.AddClientServices(apiBaseUrl);

// Add Blazor Authentication Infrastructure
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddMudServices();

var app = builder.Build();

// Log the current environment
app.Logger.LogInformation("Support Ticket System running in {Environment}", app.Environment.EnvironmentName);

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapPost("/Account/Login", async (
    HttpContext httpContext,
    [FromForm] string email,
    [FromForm] string password,
    [FromForm] string? returnUrl,
    IAuthClient authClient) =>
{
    try
    {
        var response = await authClient.LoginAsync(new LoginRequestDto { Email = email, Password = password });
        var data = response?.Data;

        if (data is null || string.IsNullOrEmpty(data.Token))
        {
            return Results.Redirect("/login?error=1");
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, data.UserId.ToString()),
            new(ClaimTypes.Name, data.Name),
            new(ClaimTypes.Email, data.Email),
            new(ClaimTypes.Role, data.Role.ToString())
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true,
            ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
        };
        authProperties.StoreTokens(new[]
        {
            new AuthenticationToken { Name = "access_token", Value = data.Token }
        });

        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

        var redirectTo = !string.IsNullOrEmpty(returnUrl)
            ? returnUrl
            : data.Role == UserRole.Manager ? "/dashboard" : "/support-agent";

        return Results.Redirect(redirectTo);
    }
    catch (BusinessException)
    {
        return Results.Redirect("/login?error=1");
    }
});

app.MapPost("/Account/Logout", async (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/login");
});

app.Run();