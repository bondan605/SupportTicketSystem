using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using MudBlazor.Services;
using SupportTicketSystem.Bsui.Components;
using SupportTicketSystem.Bsui.Constants;
using SupportTicketSystem.Bsui.Services;
using SupportTicketSystem.Client;
using SupportTicketSystem.Client.Features.Interfaces;
using SupportTicketSystem.Shared.DTOs.Auth;
using SupportTicketSystem.Shared.Exceptions;
using System.Security.Claims;
using FluentValidation;
using SupportTicketSystem.Application.Validators.Users;

var builder = WebApplication.CreateBuilder(args);
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? throw new InvalidOperationException("API Base URL is not configured.");

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(
        Path.Combine(builder.Environment.ContentRootPath, "keys")))
    .SetApplicationName("TicketSystemApp");

builder.Services.AddHttpContextAccessor();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserRequestValidator>();
builder.Services.AddScoped<ServerJwtAccessor>();
builder.Services.AddScoped<Func<Task<string?>>>(sp => sp.GetRequiredService<ServerJwtAccessor>().GetTokenAsync);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = AppRoutes.Login;
        options.AccessDeniedPath = AppRoutes.Unauthorized;
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
            return Results.Redirect($"{AppRoutes.Login}?error=1");
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
            ExpiresUtc = new DateTimeOffset(data.ExpiresAt, TimeSpan.Zero)
        };
        authProperties.StoreTokens(new[]
        {
            new AuthenticationToken { Name = "access_token", Value = data.Token }
        });

        await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

        // Regardless of role, land the user straight on the ticket list after login.
        var redirectTo = !string.IsNullOrEmpty(returnUrl) ? returnUrl : AppRoutes.TicketList;

        return Results.Redirect(redirectTo);
    }
    catch (BusinessException)
    {
        return Results.Redirect($"{AppRoutes.Login}?error=1");
    }
});

app.MapPost("/Account/Logout", async (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect(AppRoutes.Login);
});

// GET (not POST) because SessionExpiryGuard triggers this via a plain NavigationManager
// redirect from an active Blazor circuit, not a form submit.
app.MapGet("/Account/SessionExpired", async (HttpContext httpContext) =>
{
    await httpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect(AppRoutes.Login);
});

app.Run();