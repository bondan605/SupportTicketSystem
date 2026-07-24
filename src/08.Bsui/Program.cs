using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.DataProtection;
using MudBlazor.Services;
using SupportTicketSystem.Bsui.Components;
using SupportTicketSystem.Bsui.Services;
using SupportTicketSystem.Client;
using SupportTicketSystem.Client.Features.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"] ?? throw new InvalidOperationException("API Base URL is not configured.");

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(
        Path.Combine(builder.Environment.ContentRootPath, "keys")))
    .SetApplicationName("TicketSystemApp");

builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<ITokenProvider, ProtectedLocalStorageTokenProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();


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
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();