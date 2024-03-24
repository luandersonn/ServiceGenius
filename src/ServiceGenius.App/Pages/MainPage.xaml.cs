using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Linq;

namespace ServiceGenius.App.Pages;

public sealed partial class MainPage : Page
{
    public MainPage() => InitializeComponent();

    protected override void OnNavigatedTo(NavigationEventArgs e) => navigationViewControl.SelectedItem = navigationViewControl.MenuItems.FirstOrDefault();

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