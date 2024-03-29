using System;
using System.Diagnostics;
using Windows.ApplicationModel.Resources;
using Windows.Storage;

namespace ServiceGenius.App.Services.Settings;

public class AppSettings : ISettings
{
    public string GetStringFromResource(string resourceName, string resource = "Resources") => ResourceLoader.GetForViewIndependentUse(resource)?.GetString(resourceName);

    public bool TryGet<T>(string key, out T result, string containerKey = "default")
    {
        var container = GetContainer(containerKey);
        if (typeof(T).IsEnum)
        {
            if (TryGet<string>(key, out var strValue, containerKey) && Enum.TryParse(typeof(T), strValue, out object v))
            {
                result = (T)v;
                return true;
            }
        }
        else if (container.Values[key] is T v)
        {
            result = v;
            return true;
        }
        result = default;
        return false;
    }
    public bool TrySet<T>(string key, T value, string containerKey = "default")
    {
        var container = GetContainer(containerKey);
        if (typeof(T).IsEnum)
        {
            return TrySet(key, value.ToString(), containerKey);
        }
        else
        {
            object v = container.Values[key];
            if (!Equals(v, value))
            {
                try
                {
                    container.Values[key] = value;
                    SettingChanged?.Invoke(this, new SettingChangedEventArgs(key, value, containerKey));
                    return true;
                }
                catch (Exception e) when (e.HResult == unchecked((int)0x8007065E))
                {
                    container.Values[key] = value.ToString();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
        return false;
    }

    private static ApplicationDataContainer GetContainer(string key) => ApplicationData.Current.LocalSettings.CreateContainer(key, ApplicationDataCreateDisposition.Always);


    public T Get<T>(string key, string containerKey = "default")
    {
        TryGet(key, out T result, containerKey);
        return result;
    }

    public void Set<T>(string key, T value, string containerKey = "default") => TrySet(key, value, containerKey);

    public event EventHandler<SettingChangedEventArgs> SettingChanged;
}
