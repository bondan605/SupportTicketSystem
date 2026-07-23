using MudBlazor.Services;
using SupportTicketSystem.Bsui.Components;
using SupportTicketSystem.Client;

var builder = WebApplication.CreateBuilder(args);
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];

// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

// Register the CategoryClient with the base address from configuration
builder.Services.AddClientServices(apiBaseUrl!);

// Add MudBlazor services
builder.Services.AddMudServices();

var app = builder.Build();

// Log the current environment
app.Logger.LogInformation("Running in {Environment}", app.Environment.EnvironmentName);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
