using Microsoft.UI.Xaml;
using ServiceGenius.App.Extensions;
using System;

namespace ServiceGenius.App.Controls;

public partial class TitleBarControl
{
    private async void OnLoaded(object sender, RoutedEventArgs e)
    {
        Window = App.AllWindows.TryGetValue(XamlRoot, out Window window)
                ? window
                : throw new InvalidOperationException("No registered window found for this XamlRoot");

        Window.Activated += OnWindowActivated;
        Window.ExtendsContentIntoTitleBar = true;
        await TryUpdateRegionsForCustomTitleBarAsync();
        SetWindowTitle(Title);
        SetWindowActiveState(Window.GetIsWindowActive() ? WindowActivationState.CodeActivated : WindowActivationState.Deactivated);
        UpdateTheme();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        try
        {
            Window.Activated -= OnWindowActivated;
            //Window.ExtendsContentIntoTitleBar = false;
            //Window.SetTitleBar(null);
        }
        catch { }
    }

    private async void OnSizeChanged(object sender, SizeChangedEventArgs e) => await TryUpdateRegionsForCustomTitleBarAsync();

    private void OnActualThemeChanged(FrameworkElement sender, object args) => UpdateTheme();

    private void OnWindowActivated(object sender, WindowActivatedEventArgs args) => SetWindowActiveState(args.WindowActivationState);

    private static async void OnPreferredHeightOption(DependencyObject sender, DependencyPropertyChangedEventArgs _)
    {
        TitleBarControl titleBar = (TitleBarControl)sender;
        await titleBar.TryUpdateRegionsForCustomTitleBarAsync();
    }

    private static void OnTitleChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
    {
        TitleBarControl titleBar = (TitleBarControl)sender;
        titleBar.SetWindowTitle((string)args.NewValue);
    }

    private static void OnDisplayOptionsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs _)
    {
        if (sender is TitleBarControl titleBar && titleBar.AppIcon is not null)
            titleBar.UpdateTitleAndIconVisibilityAndPosition();
    }
}
