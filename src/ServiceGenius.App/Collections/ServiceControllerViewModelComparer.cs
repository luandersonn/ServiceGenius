using ServiceGenius.App.ViewModels;
using System;
using System.Collections.Generic;

namespace ServiceGenius.App.Collections;

public class ServiceControllerViewModelComparer(ServiceSortAttribute sortOrder, ServiceSortDirection sortOption) : IComparer<ServiceControllerViewModel>
{
    public int Compare(ServiceControllerViewModel x, ServiceControllerViewModel y)
    {
        int result = sortOrder switch
        {
            ServiceSortAttribute.DisplayName => string.Compare(x.DisplayName, y.DisplayName, StringComparison.Ordinal),
            ServiceSortAttribute.Name => string.Compare(x.ServiceName, y.ServiceName, StringComparison.Ordinal),
            ServiceSortAttribute.Description => string.Compare(x.Description, y.Description, StringComparison.Ordinal),
            ServiceSortAttribute.Status => x.Status.CompareTo(y.Status),
            _ => throw new ArgumentException("Invalid sort order"),
        };

        return sortOption == ServiceSortDirection.Desc ? -result : result;
    }
}