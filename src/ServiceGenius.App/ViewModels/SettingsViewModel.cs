using Windows.ApplicationModel;

namespace ServiceGenius.App.ViewModels;

public class SettingsViewModel
{
    public string Version
    {
        get
        {
            PackageVersion packageVersion = Package.Current.Id.Version;
            return $"{packageVersion.Major}.{packageVersion.Minor}.{packageVersion.Build}";
        }
    }
}
