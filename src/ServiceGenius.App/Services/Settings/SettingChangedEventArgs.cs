using System;

namespace ServiceGenius.App.Services;

public class SettingChangedEventArgs(string key, object value, string container) : EventArgs
{
    public object Value { get; } = value;
    public string Container { get; } = container;
    public string Key { get; } = key;
}
