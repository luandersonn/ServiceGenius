using CommunityToolkit.Mvvm.ComponentModel;
using ServiceGenius.App.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.ServiceProcess;

namespace ServiceGenius.App.ViewModels;

public partial class ServiceControllerViewModel : ObservableObject
{
    public ServiceControllerViewModel(ServiceController service)
    {
        Service = service;

        LoadInfos();
    }

    private static TwoWayDictionary<ServiceControllerStatus, string> StatusMap { get; } = new()
    {
        [ServiceControllerStatus.Running] = "Running",
        [ServiceControllerStatus.StartPending] = "Start Pending",
        [ServiceControllerStatus.StopPending] = "Stop Pending",
        [ServiceControllerStatus.Stopped] = "Stopped",
        [ServiceControllerStatus.ContinuePending] = "Continue Pending",
        [ServiceControllerStatus.PausePending] = "Pause Pending",
        [ServiceControllerStatus.Paused] = "Paused"
    };

    private static TwoWayDictionary<ServiceStartMode, string> StartModeMap { get; } = new()
    {
        [ServiceStartMode.Boot] = "Boot",
        [ServiceStartMode.System] = "System",
        [ServiceStartMode.Automatic] = "Automatic",
        [ServiceStartMode.Manual] = "Manual",
        [ServiceStartMode.Disabled] = "Disabled"
    };

    public IEnumerable<string> StatusValues => StatusMap.Type2Values;
    public IEnumerable<string> StartModeValues => StartModeMap.Type2Values;

    public ServiceController Service { get; }

    public string DisplayName => Service.DisplayName;
    public string ServiceName => Service.ServiceName;
    public string Description { get; private set; }
    public string PathToExecutable { get; private set; }

    [ObservableProperty]
    private string _status;

    [ObservableProperty]
    private string _startMode;

    private void LoadInfos()
    {
        using ManagementObject serviceObject = new(new ManagementPath(string.Format("Win32_Service.Name='{0}'", Service.ServiceName)));
        try
        {
            Description = $"{serviceObject[nameof(Description)]}";
            PathToExecutable = $"{serviceObject["PathName"]}";
            Status = StatusMap[Service.Status];
            StartMode = StartModeMap[Service.StartType];
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }
}