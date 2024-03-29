using System;

namespace ServiceGenius.App.Services;

public interface ISettings
{
    string GetStringFromResource(string resourceName, string resource = "Resources");
    T Get<T>(string key, string containerKey = "default");
    void Set<T>(string key, T value, string containerKey = "default");
    bool TryGet<T>(string key, out T result, string containerKey = "default");
    bool TrySet<T>(string key, T value, string containerKey = "default");

    event EventHandler<SettingChangedEventArgs> SettingChanged;
}