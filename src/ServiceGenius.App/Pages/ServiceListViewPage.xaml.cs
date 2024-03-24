using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using ServiceGenius.App.ViewModels;
using System;

namespace ServiceGenius.App.Pages;

public sealed partial class ServiceListViewPage : Page
{
    public ServiceListViewPage() => InitializeComponent();

    protected override void OnNavigatedTo(NavigationEventArgs e) => ViewModel.RefreshServicesCommand.Execute(null);

    private async void OnListViewItemClick(object sender, ItemClickEventArgs e)
    {
        _ = await new Controls.ServiceDetailsDialog((ServiceControllerViewModel)e.ClickedItem)
        {
            XamlRoot = XamlRoot,
            RequestedTheme = ActualTheme
        }.ShowAsync();
    }

    private void GoToSettingsPage() => Frame.Navigate(typeof(SettingsPage));

    private void OnSearchBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        if (args.CheckCurrent())
        {
            string queryTerm = sender.Text;
            if (string.IsNullOrWhiteSpace(queryTerm))
            {
                ViewModel.Services.Filter = null;
            }
            else
            {
                StringComparison sc = StringComparison.InvariantCultureIgnoreCase;
                ViewModel.Services.Filter = item => item.ServiceName.Contains(queryTerm, sc) || item.DisplayName.Contains(queryTerm, sc) || item.Description.Contains(queryTerm, sc);
            }
        }
    }
}