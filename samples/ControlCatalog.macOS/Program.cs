using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Dialogs;
using Avalonia.LogicalTree;
using Avalonia.Threading;
using ControlCatalog;

var builder = BuildAvaloniaApp();
builder.StartWithClassicDesktopLifetime(args, ShutdownMode.OnMainWindowClose);

/// <summary>
/// This method is needed for IDE previewer infrastructure
/// </summary>
static AppBuilder BuildAvaloniaApp()
    => AppBuilder.Configure<App>()
        .UsePlatformDetect()
        .UseSkia()
        .UseManagedSystemDialogs()
        .AfterSetup(builder =>
        {
            builder.Instance!.AttachDevTools(new Avalonia.Diagnostics.DevToolsOptions() { StartupScreenIndex = 1, });
        })
        .LogToTrace();
