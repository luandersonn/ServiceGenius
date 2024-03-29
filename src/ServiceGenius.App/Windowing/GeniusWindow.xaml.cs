using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using ServiceGenius.App.Services;
using System;
using WinUIEx;

namespace ServiceGenius.App.Windowing;

public sealed partial class GeniusWindow : WindowEx
{
    private readonly IServiceProvider serviceProvider;
    private readonly Type destinationPage;
    private readonly object navigationParameter;
    private readonly ISettings settings;

    public GeniusWindow(IServiceProvider serviceProvider, Type destinationPage, object navigationParameter = null)
    {
        InitializeComponent();
        this.serviceProvider = serviceProvider;
        this.destinationPage = destinationPage;
        this.navigationParameter = navigationParameter;

        settings = this.serviceProvider.GetRequiredService<ISettings>();
    }

    private void OnUILoaded(object sender, RoutedEventArgs e)
    {
        FrameworkElement element = (FrameworkElement)sender;
        element.RequestedTheme = settings.Get<ElementTheme>("AppTheme");

        App.AllWindows.TryAdd(element.XamlRoot, this);

        frame.Navigate(destinationPage, navigationParameter);
    }

    public ElementTheme RequestedTheme
    {
        get => WindowRootContainer.RequestedTheme;
        set
        {
            WindowRootContainer.RequestedTheme = value;
            settings.Set("AppTheme", value);
        }
    }
    public ElementTheme ActualTheme => WindowRootContainer.ActualTheme;
}