using SupportTicketSystem.Application;
using SupportTicketSystem.Infrastructure;
using SupportTicketSystem.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApiVersioningConfiguration();
builder.Services.AddSwaggerDocumentation();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.UseSwaggerDocumentation();
app.UseApplicationMiddleware();
app.UseHttpsRedirection();

app.MapControllers();

app.Run();