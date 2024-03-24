using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ServiceGenius.App.ViewModels;

public class ServiceListViewModel : ObservableObject
{
    public ServiceListViewModel()
    {
        Services = [];

        RefreshServicesCommand = new AsyncRelayCommand(RefreshServicesAsync);
    }

    public ObservableCollection<ServiceControllerViewModel> Services { get; }
    public IAsyncRelayCommand RefreshServicesCommand { get; }

    private async Task RefreshServicesAsync()
    {
        Task task = Task.Delay(500);

        List<ServiceControllerViewModel> viewModels = await Task.Run(() =>
        {

            ServiceController[] services = ServiceController.GetServices();

            return services.OrderBy(s => s.ServiceName)
                           .Select(service => new ServiceControllerViewModel(service))
                           .ToList();
        });

        Services.Clear();

        foreach (ServiceControllerViewModel viewModel in viewModels)
        {
            Services.Add(viewModel);
        }

        await task;
    }
}