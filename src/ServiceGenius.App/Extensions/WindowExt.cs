using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using ServiceGenius.App.Interop;
using Windows.UI;
using WinUIEx;

namespace ServiceGenius.App.Extensions;

public static class WindowExt
{
    public static bool GetIsWindowActive(this Window window) => window.GetWindowHandle() == Win32.GetActiveWindow();

    public static void SetTitleBarTheme(this AppWindow appWindow, ElementTheme theme)
    {
        if (appWindow != null && AppWindowTitleBar.IsCustomizationSupported())
        {
            AppWindowTitleBar titleBar = appWindow.TitleBar;

            Color foreground = default, background = default;

            switch (theme)
            {
                case ElementTheme.Default:
                    foreground = (Color)Application.Current.Resources["SystemBaseHighColor"];
                    background = (Color)Application.Current.Resources["SystemAltHighColor"];
                    break;
                case ElementTheme.Light:
                    foreground = Colors.Black;
                    background = Colors.White;
                    break;
                case ElementTheme.Dark:
                    foreground = Colors.White;
                    background = Colors.Black;
                    break;
            }

            titleBar.ForegroundColor = foreground;
            titleBar.BackgroundColor = background;

            titleBar.ButtonForegroundColor = foreground;
            titleBar.ButtonBackgroundColor = Colors.Transparent;

            titleBar.ButtonHoverForegroundColor = foreground;
            byte newAlpha = (byte)(foreground.A * 0.2);
            titleBar.ButtonHoverBackgroundColor = Color.FromArgb(newAlpha, foreground.R, foreground.G, foreground.B);

            titleBar.ButtonPressedForegroundColor = foreground;
            newAlpha = (byte)(foreground.A * 0.4);
            titleBar.ButtonPressedBackgroundColor = Color.FromArgb(newAlpha, foreground.R, foreground.G, foreground.B);

            //titleBar.ButtonInactiveForegroundColor = Colors.Gray;
            titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }
    }

}
