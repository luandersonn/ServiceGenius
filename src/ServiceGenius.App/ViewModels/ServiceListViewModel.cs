﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ServiceGenius.App.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace ServiceGenius.App.ViewModels;

public partial class ServiceListViewModel : ObservableObject
{
    public ServiceListViewModel()
    {
        Services = new SortedObservableCollection<ServiceControllerViewModel>(new ServiceControllerViewModelComparer(ServiceSortAttribute.DisplayName, ServiceSortDirection.Asc));

        RefreshServicesCommand = new AsyncRelayCommand(RefreshServicesAsync);
    }

    public SortedObservableCollection<ServiceControllerViewModel> Services { get; }
    public IAsyncRelayCommand RefreshServicesCommand { get; }

    private async Task RefreshServicesAsync()
    {
        Task task = Task.Delay(500);

        List<ServiceControllerViewModel> viewModels = await Task.Run(() =>
        {
            ServiceController[] services = ServiceController.GetServices();

            return services.OrderBy(s => s.DisplayName)
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