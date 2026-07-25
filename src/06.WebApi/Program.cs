using SupportTicketSystem.Application;
using SupportTicketSystem.Application.Interfaces.Repositories;
using SupportTicketSystem.Infrastructure;
using SupportTicketSystem.Infrastructure.Persistence.Repositories;
using SupportTicketSystem.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddControllers();


builder.Services.AddSwaggerDocumentation();

builder.Services.AddScoped<ITicketHistoryRepository, TicketHistoryRepository>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseApplicationMiddleware();
app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();