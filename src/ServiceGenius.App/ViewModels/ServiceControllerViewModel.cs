using CommunityToolkit.Mvvm.ComponentModel;
using ServiceGenius.App.Utils;
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

    public IEnumerable<string> StatusValues => EnumMapperHelper.ServiceStatusMap.Type2Values;
    public IEnumerable<string> StartModeValues => EnumMapperHelper.ServiceStartModeMap.Type2Values;

    public ServiceController Service { get; }

    public string DisplayName => Service.DisplayName;
    public string ServiceName => Service.ServiceName;
    public string Description { get; private set; }
    public string PathToExecutable { get; private set; }
    public string Status => EnumMapperHelper.ServiceStatusMap[Service.Status];
    public string StartMode => EnumMapperHelper.ServiceStartModeMap[Service.StartType];

    private void LoadInfos()
    {
        using ManagementObject serviceObject = new(new ManagementPath(string.Format("Win32_Service.Name='{0}'", Service.ServiceName)));
        try
        {
            Description = $"{serviceObject[nameof(Description)]}";
            PathToExecutable = $"{serviceObject["PathName"]}";
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }
}