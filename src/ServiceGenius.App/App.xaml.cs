using Microsoft.UI.Xaml;
using ServiceGenius.App.Pages;
using System.Collections.Concurrent;

namespace ServiceGenius.App;

public partial class App : Application
{
    public App() => InitializeComponent();

    public static ConcurrentDictionary<XamlRoot, Window> AllWindows { get; } = [];
    public static Window MainWindow { get; private set; }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        MainWindow = new MainWindow(typeof(MainPage));
        MainWindow.Activate();
    }
}