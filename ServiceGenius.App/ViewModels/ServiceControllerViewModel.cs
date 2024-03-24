using System;
using System.Diagnostics;
using System.Management;
using System.ServiceProcess;

namespace ServiceGenius.App.ViewModels;

public class ServiceControllerViewModel(ServiceController service)
{
    public string Name => Service.DisplayName;
    public string Description { get; } = GetDescription(service);

    public ServiceController Service { get; } = service;

    private static string GetDescription(ServiceController service)
    {
        using ManagementObject serviceObject = new(new ManagementPath(string.Format("Win32_Service.Name='{0}'", service.ServiceName)));
        try
        {
            return $"{serviceObject[nameof(Description)]}";
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return "";
        }
    }
}