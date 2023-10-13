using System.Drawing;
using System.Reflection;
using System.Windows;
using LogViewer.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MsLogger.Core;
using RandomLogging.Service;
using WpfLoggingDI.ViewModels;

namespace WpfLoggingDI;

public partial class App
{
     #region Private Fields

     private readonly CancellationTokenSource _cancellationTokenSource;

     private readonly IHost? _host;

     #endregion Private Fields

     #region Public Constructors

     public App()
     {
          // catch all unhandled errors
          AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

          HostApplicationBuilder builder = Host.CreateApplicationBuilder();

          builder
              /*
               * Note: For information on launch profiles for debugging,
               *     see article: https://www.codeproject.com/Articles/5354478/NET-App-Settings-Demystified-Csharp-VB
               */

              // Register the Random Logging Service
              .AddRandomBackgroundService()

              // visual debugging tools
              .AddLogViewer()

              // Microsoft Logger
              //.Logging.AddDefaultDataStoreLogger();

              // uncomment to use custom logging colors (note: System.Drawing namespace)
              //
              .Logging.AddDefaultDataStoreLogger(options =>
              {
                   options.Colors[LogLevel.Trace] = new()
                   {
                        Foreground = Color.White,
                        Background = Color.DarkGray
                   };

                   options.Colors[LogLevel.Debug] = new()
                   {
                        Foreground = Color.White,
                        Background = Color.Gray
                   };

                   options.Colors[LogLevel.Information] = new()
                   {
                        Foreground = Color.White,
                        Background = Color.DodgerBlue
                   };

                   options.Colors[LogLevel.Warning] = new()
                   {
                        Foreground = Color.White,
                        Background = Color.Orchid
                   };
              });

          IServiceCollection services = builder.Services;

          services
              .AddSingleton<MainViewModel>()
              .AddSingleton<MainWindow>(service => new MainWindow
              {
                   DataContext = service.GetRequiredService<MainViewModel>()
              });

          _host = builder.Build();
          _cancellationTokenSource = new();
     }

     #endregion Public Constructors

     #region Protected Methods

     /// <summary>
     /// Raises the <see cref="E:System.Windows.Application.Exit" /> event.
     /// </summary>
     /// <param name="e">An <see cref="T:System.Windows.ExitEventArgs" /> that contains the event data.</param>
     protected override void OnExit(ExitEventArgs e)
     {
          // tell the background services that we are shutting down
          _host!.StopAsync(_cancellationTokenSource.Token);

          base.OnExit(e);
     }

     /// <summary>
     /// Raises the <see cref="E:System.Windows.Application.Startup" /> event.
     /// </summary>
     /// <param name="e">A <see cref="T:System.Windows.StartupEventArgs" /> that contains the event data.</param>
     protected override void OnStartup(StartupEventArgs e)
     {
          try
          {
               LogStartingMode();

               // set and show
               MainWindow = _host!.Services.GetRequiredService<MainWindow>();
               MainWindow.Show();

               // startup background services
               _ = _host.StartAsync(_cancellationTokenSource.Token);
          }
          catch (OperationCanceledException)
          {
               // skip
          }
          catch (Exception ex)
          {
               MessageBox.Show(ex.Message, "Unhandled Error", MessageBoxButton.OK, MessageBoxImage.Stop);
          }
     }

     #endregion Protected Methods

     #region Private Methods

     private void LogStartingMode()
     {
          // Get the Launch mode
          bool isDevelopment = string.Equals(Environment.GetEnvironmentVariable("DOTNET_MODIFIABLE_ASSEMBLIES"), "debug",
                                             StringComparison.InvariantCultureIgnoreCase);

          // initialize a logger & EventId
          ILogger<App> logger = _host!.Services.GetRequiredService<ILogger<App>>();
          EventId eventId = new EventId(id: 0, name: Assembly.GetEntryAssembly()!.GetName().Name);

          // log a test pattern for each log level
          logger.TestPattern(eventId: eventId);

          // log that we have started...
          logger.Emit(eventId, LogLevel.Information, $"Running in {(isDevelopment ? "Debug" : "Release")} mode");
     }

     private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
                        => MessageBox.Show(
             ((Exception)e.ExceptionObject).Message,
             "Unhandled Error",
             MessageBoxButton.OK,
             MessageBoxImage.Stop);

     #endregion Private Methods
}