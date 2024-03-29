using Microsoft.UI.Xaml.Controls;

namespace ServiceGenius.App.Pages;

public sealed partial class SettingsPage : Page
{
    public SettingsPage() => InitializeComponent();

    public int AppTheme
    {
        get => (int)ViewModel.AppTheme;
        set => ViewModel.AppTheme = (Microsoft.UI.Xaml.ElementTheme)value;
    }

    public void GoBack() => Frame.GoBack();
}