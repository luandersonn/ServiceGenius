using Microsoft.UI.Xaml.Controls;
using ServiceGenius.App.ViewModels;

namespace ServiceGenius.App.Controls;

public sealed partial class ServiceDetailsDialog : ContentDialog
{
    public ServiceDetailsDialog(ServiceControllerViewModel service)
    {
        InitializeComponent();
        Service = service;
    }

    public ServiceControllerViewModel Service { get; }
}