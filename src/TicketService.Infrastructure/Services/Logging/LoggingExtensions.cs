using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace TicketService.Infrastructure.Services.Logging;

public static class LoggingExtensions
{
    public static void AddLoggingInfrastructure(
        this IHostBuilder host,
        IConfiguration configuration)
    {
        host.UseSerilog((context, logger) =>
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
                
                .WriteTo.Console()
                .WriteTo.Seq(seqUrl!);
        });
    }
}