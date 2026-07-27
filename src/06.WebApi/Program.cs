using QuestPDF.Infrastructure;
using SupportTicketSystem.Application;
using SupportTicketSystem.Infrastructure;
using SupportTicketSystem.Infrastructure.Persistence.Seeding;
using SupportTicketSystem.WebApi.Extensions;
QuestPDF.Settings.License = LicenseType.Community;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseApplicationMiddleware();
app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();