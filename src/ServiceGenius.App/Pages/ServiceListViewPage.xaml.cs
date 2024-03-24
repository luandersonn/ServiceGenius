using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace ServiceGenius.App.Pages;

public sealed partial class ServiceListViewPage : Page
{
    public ServiceListViewPage() => InitializeComponent();

    protected override void OnNavigatedTo(NavigationEventArgs e) => ViewModel.RefreshServicesCommand.Execute(null);
}