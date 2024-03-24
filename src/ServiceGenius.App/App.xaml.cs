using Microsoft.UI.Xaml;
using ServiceGenius.App.Pages;
using ServiceGenius.App.Windowing;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace ServiceGenius.App;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        UnhandledException += OnAppUnhandledException;
    }

    private void OnAppUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        e.Handled = true;
        Debug.WriteLine(e.Message);
    }

    public static ConcurrentDictionary<XamlRoot, Window> AllWindows { get; } = [];
    public static GeniusWindow MainWindow { get; private set; }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        MainWindow = new GeniusWindow(typeof(MainPage))
        {
            PersistenceId = "MainWindow"
        };
        MainWindow.Activate();
    }
}