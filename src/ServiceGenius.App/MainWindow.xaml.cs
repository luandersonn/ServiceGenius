using Microsoft.UI.Xaml;
using System;
using WinUIEx;

namespace ServiceGenius.App;

public sealed partial class MainWindow : WindowEx
{
    private readonly Type destinationPage;
    private readonly object navigationParameter;

    public MainWindow(Type destinationPage, object navigationParameter = null)
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