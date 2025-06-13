using System;

using Avalonia;
using DeathCounterHotkey.Database;
using DeathCounterHotkey.Database.Models;
using FallenTallyAvalon.Controller;
using FallenTallyAvalon.Dialogue;
using FallenTallyAvalon.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace FallenTallyAvalon.Desktop;

class Program
{
    private static IServiceProvider _serviceProvider;
    private static Serilog.ILogger Log => Serilog.Log.ForContext<Program>();

    [STAThread]
    public static void Main(string[] args)
    {
        // Configure Serilog  
        var outputTemplate = "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message}{NewLine}in method {MemberName} at {FilePath}:{LineNumber}{NewLine}{Exception}{NewLine}";

        Serilog.Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Warning()
                    .Enrich.FromLogContext()
                    .WriteTo.File("log/{Date}.log", outputTemplate: outputTemplate, restrictedToMinimumLevel: LogEventLevel.Warning)
                    .WriteTo.Console(LogEventLevel.Warning, outputTemplate, theme: AnsiConsoleTheme.Literate)
                    .CreateLogger();

        AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
        {
            Log.Fatal(e.ExceptionObject as Exception, "Unhandled exception occurred");
        };

        try
        {
            Log.Information("Starting application");

            // Configure Dependency Injection  
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
            ServiceLocator.Provider = _serviceProvider;

            BuildAvaloniaApp()
                .StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally
        {
        }
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        // Register application services here  
        services.AddDbContext<SQLiteDBContext>();

        services.AddSingleton<GameController>();
        // Register GameDialog as a singleton with DI for GameController
        services.AddSingleton<GameDialogWindow>(provider =>
            new GameDialogWindow(provider.GetRequiredService<GameController>()));


        services.AddTransient<TallyViewModel>();
    }

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
