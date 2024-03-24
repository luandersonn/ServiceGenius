using Microsoft.UI.Xaml;
using System;
using WinUIEx;

namespace ServiceGenius.App.Windowing;

public sealed partial class GeniusWindow : WindowEx
{
    private readonly Type destinationPage;
    private readonly object navigationParameter;

    public GeniusWindow(Type destinationPage, object navigationParameter = null)
    {
        InitializeComponent();
        this.destinationPage = destinationPage;
        this.navigationParameter = navigationParameter;
    }

    private void OnUILoaded(object sender, RoutedEventArgs e)
    {
        UIElement element = (UIElement)sender;
        App.AllWindows.TryAdd(element.XamlRoot, this);

        frame.Navigate(destinationPage, navigationParameter);
    }
}