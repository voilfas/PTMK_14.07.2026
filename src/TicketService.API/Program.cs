using AspNetCore.Swagger.Themes;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TicketService.API.Exceptions;
using TicketService.Application;
using TicketService.Infrastructure;
using TicketService.Infrastructure.Persistence;
using TicketService.Infrastructure.Services.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Host.AddLoggingInfrastructure(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await db.Database.MigrateAsync();
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(Theme.Dark);
}

app.UseExceptionHandler();

app.UseSerilogRequestLogging(options =>
{
    options.MessageTemplate = 
        "HTTP {RequestMethod} {RequestPath} -> {StatusCode} in {Elapsed:0.0000} ms";
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();