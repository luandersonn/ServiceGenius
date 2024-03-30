using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using ServiceGenius.App.ViewModels;
using System;
using System.Threading.Tasks;

namespace ServiceGenius.App.Pages;

public sealed partial class ServiceListViewPage : Page
{
    public ServiceListViewPage() => InitializeComponent();

    protected override void OnNavigatedTo(NavigationEventArgs e) => ViewModel.RefreshServicesCommand.Execute(null);

    private async void OnListViewItemClick(object sender, ItemClickEventArgs e) => await ShowServiceDetails((ServiceControllerViewModel)e.ClickedItem);

    private void GoToSettingsPage() => Frame.Navigate(typeof(SettingsPage));

    private async void OnSearchBoxTextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
    {
        StringComparison sc = StringComparison.InvariantCultureIgnoreCase;

        string query = sender.Text;

        if (string.IsNullOrWhiteSpace(query))
        {
            ViewModel.Services.Filter = null;
        }
        else
        {
            await Task.Delay(250);

            if (args.CheckCurrent())
            {
                ViewModel.Services.Filter = item => item.ServiceName.Contains(query, sc) || item.DisplayName.Contains(query, sc) || item.Description.Contains(query, sc);
            }
        }
    }

    private async Task ShowServiceDetails(ServiceControllerViewModel service)
    {
        _ = await new Controls.ServiceDetailsDialog(service)
        {
            XamlRoot = XamlRoot,
            RequestedTheme = ActualTheme
        }.ShowAsync();
    }
}