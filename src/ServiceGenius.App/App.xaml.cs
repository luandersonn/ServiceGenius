using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using ServiceGenius.App.Pages;
using ServiceGenius.App.Services;
using ServiceGenius.App.Services.Settings;
using ServiceGenius.App.Windowing;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using WinRT;
using WinUIEx;
using MUX = Microsoft.UI.Xaml;
using MWA = Microsoft.Windows.AppLifecycle;
using WPA = Windows.ApplicationModel.Activation;

namespace ServiceGenius.App;

public partial class App : Application
{
    public App()
    {
        Services = ConfigureServices();

        InitializeComponent();

        UnhandledException += OnAppUnhandledException;
    }

    private void OnAppUnhandledException(object sender, MUX.UnhandledExceptionEventArgs e)
    {
        e.Handled = true;
        Debug.WriteLine(e.Message);
    }

    public static ConcurrentDictionary<XamlRoot, Window> AllWindows { get; } = [];
    public static GeniusWindow MainWindow { get; private set; }
    public IServiceProvider Services { get; }

    protected override async void OnLaunched(LaunchActivatedEventArgs args)
    {
        MWA.AppActivationArguments appArgs = MWA.AppInstance.GetCurrent().GetActivatedEventArgs();

        MWA.AppInstance mainInstance = MWA.AppInstance.FindOrRegisterForKey("main");

        if (!mainInstance.IsCurrent)
        {
            await mainInstance.RedirectActivationToAsync(appArgs);

            Process.GetCurrentProcess().Kill();
            return;
        }

        MainWindow = new GeniusWindow(Services, typeof(ServiceListViewPage))
        {
            PersistenceId = "MainWindow"
        };
        MainWindow.Activate();

        MWA.AppInstance.GetCurrent().Activated += OnAppActivated;
    }



    private void OnAppActivated(object sender, MWA.AppActivationArguments e) => HandleActivation(e.Data.As<WPA.IActivatedEventArgs>());

    private static void HandleActivation(WPA.IActivatedEventArgs _)
    {
        MainWindow?.DispatcherQueue.TryEnqueue(() =>
            {
                IntPtr hwnd = MainWindow.GetWindowHandle();
                HwndExtensions.RestoreWindow(hwnd);
            });
    }

    private static ServiceProvider ConfigureServices()
    {
        ServiceCollection services = new();

        services
            .AddSingleton<ISettings, AppSettings>()
            ;

        return services.BuildServiceProvider();
    }
}