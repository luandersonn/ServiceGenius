using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using ServiceGenius.App.Pages;
using System;
using System.Linq;
using WinUIEx;

namespace ServiceGenius.App;

public sealed partial class MainWindow : WindowEx
{
    public MainWindow() => InitializeComponent();

    private void OnGridLoaded(object sender, RoutedEventArgs e) => navigationViewControl.SelectedItem = navigationViewControl.MenuItems.FirstOrDefault();

    private void OnNavigationViewSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        Type pageType = (string)args.SelectedItemContainer.Tag switch
        {
            "services" => typeof(ServiceListViewPage),
            _ when args.IsSettingsSelected => typeof(SettingsPage),
            _ => null,
        };

        if (pageType is not null)
        {
            frame.Navigate(pageType, args.RecommendedNavigationTransitionInfo);
        }
    }
}