using Microsoft.UI.Xaml.Controls;

namespace ServiceGenius.App.Pages;

public sealed partial class SettingsPage : Page
{
    public SettingsPage() => InitializeComponent();

    public void GoBack() => Frame.GoBack();
}