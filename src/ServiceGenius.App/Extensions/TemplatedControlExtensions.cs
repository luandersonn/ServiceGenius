using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Reflection;

namespace ServiceGenius.App.Extensions;

public static class TemplatedControlExtensions
{
    public static T GetTemplateChild<T>(this Control controlTemplate, string childName, bool isRequired = true)
        where T : DependencyObject
    {
        var methodInfo = controlTemplate.GetType().GetMethod(nameof(GetTemplateChild), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

        var t = methodInfo.Invoke(controlTemplate, new[] { childName }) as T;

        return isRequired & t is null ? throw new NullReferenceException() : t;
    }
}