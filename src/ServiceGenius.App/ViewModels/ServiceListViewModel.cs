using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceProcess;

namespace ServiceGenius.App.ViewModels;

public class ServiceListViewModel : ObservableObject
{
    public ServiceListViewModel()
    {
        Services = [];

        RefreshServicesCommand = new RelayCommand(RefreshServices);
    }

    public ObservableCollection<ServiceControllerViewModel> Services { get; }
    public IRelayCommand RefreshServicesCommand { get; }

    private void RefreshServices()
    {
        ServiceController[] services = ServiceController.GetServices();
        Services.Clear();
        foreach (ServiceController service in services.OrderBy(s => s.DisplayName))
        {
            Services.Add(new ServiceControllerViewModel(service));
        }
    }
}