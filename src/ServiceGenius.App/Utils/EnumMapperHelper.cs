using ServiceGenius.App.Collections;
using System.ServiceProcess;

namespace ServiceGenius.App.Utils;

public static class EnumMapperHelper
{
    public static TwoWayDictionary<ServiceControllerStatus, string> ServiceStatusMap { get; } = new()
    {
        [ServiceControllerStatus.Running] = "Running",
        [ServiceControllerStatus.StartPending] = "Start Pending",
        [ServiceControllerStatus.StopPending] = "Stop Pending",
        [ServiceControllerStatus.Stopped] = "Stopped",
        [ServiceControllerStatus.ContinuePending] = "Continue Pending",
        [ServiceControllerStatus.PausePending] = "Pause Pending",
        [ServiceControllerStatus.Paused] = "Paused"
    };

    public static TwoWayDictionary<ServiceStartMode, string> ServiceStartModeMap { get; } = new()
    {
        [ServiceStartMode.Boot] = "Boot",
        [ServiceStartMode.System] = "System",
        [ServiceStartMode.Automatic] = "Automatic",
        [ServiceStartMode.Manual] = "Manual",
        [ServiceStartMode.Disabled] = "Disabled"
    };
}
