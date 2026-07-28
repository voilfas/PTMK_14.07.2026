using AspNetCore.Swagger.Themes;
using Microsoft.EntityFrameworkCore;
using Serilog;
using TicketService.API.Exceptions;
using TicketService.Application;
using TicketService.Infrastructure;
using TicketService.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);


builder.Host.UseSerilog((context, logger) =>
{
    var seqUrl = context.Configuration["Serilog:SeqUrl"];
    Console.WriteLine("SEQ URL: " + seqUrl);
    
    logger
        // Общий уровень
        .MinimumLevel.Information()

        // Фильтры
        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
        .MinimumLevel.Override("System", Serilog.Events.LogEventLevel.Warning)
        
        .MinimumLevel.Override(
            "Microsoft.Hosting.Lifetime",
            Serilog.Events.LogEventLevel.Information)

        .MinimumLevel.Override(
            "Microsoft.EntityFrameworkCore",
            Serilog.Events.LogEventLevel.Warning)

        .MinimumLevel.Override(
            "Microsoft.EntityFrameworkCore.Database.Command",
            Serilog.Events.LogEventLevel.Warning)

        .MinimumLevel.Override(
            "Microsoft.AspNetCore",
            Serilog.Events.LogEventLevel.Warning)

        .MinimumLevel.Override(
            "Microsoft.AspNetCore.Hosting.Diagnostics",
            Serilog.Events.LogEventLevel.Warning)

        .MinimumLevel.Override(
            "Microsoft.AspNetCore.Mvc",
            Serilog.Events.LogEventLevel.Warning)

        .MinimumLevel.Override(
            "Microsoft.AspNetCore.Routing",
            Serilog.Events.LogEventLevel.Warning)

        // Дополнительные свойства
        .Enrich.FromLogContext()

        // Куда писать
        .WriteTo.Console()

        .WriteTo.Seq(seqUrl!);
});

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