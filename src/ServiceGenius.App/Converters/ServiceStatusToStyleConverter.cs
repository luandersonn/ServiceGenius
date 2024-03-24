using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;
using System;
using System.ServiceProcess;

namespace ServiceGenius.App.Converters;

public class ServiceStatusToStyleConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        try
        {
            return Application.Current.Resources[$"ServiceStatus{(ServiceControllerStatus)value}TextStyle"];
        }
        catch
        {
            return "";
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
