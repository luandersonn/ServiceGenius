using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Markup;
using Microsoft.UI.Xaml.Media;
using ServiceGenius.App.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics;

namespace ServiceGenius.App.Controls;

[ContentProperty(Name = "Content")]
public sealed partial class TitleBarControl : Control
{
    public TitleBarControl()
    {
        DefaultStyleKey = typeof(TitleBarControl);
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
        SizeChanged += OnSizeChanged;
        ActualThemeChanged += OnActualThemeChanged;
    }

    protected override void OnApplyTemplate()
    {
        LeftColumn = this.GetTemplateChild<ColumnDefinition>(nameof(LeftColumn));
        RightColumn = this.GetTemplateChild<ColumnDefinition>(nameof(RightColumn));
        AppTitleBarContainer = this.GetTemplateChild<Panel>(nameof(AppTitleBarContainer));
        AppTitleRunBlock = this.GetTemplateChild<Run>(nameof(AppTitleRunBlock));
        AppIcon = this.GetTemplateChild<Image>(nameof(AppIcon));
        AppTitleVersionRunText = this.GetTemplateChild<Run>(nameof(AppTitleVersionRunText));
        AppTitleTextBlock = this.GetTemplateChild<UIElement>(nameof(AppTitleTextBlock));
    }


    private bool SetWindowTitle(string title)
    {
        if (Window != null)
        {
            try
            {
                AppTitleVersionRunText.Text = "";

                string appName = "Service Genius";

                string windowTitle = string.IsNullOrWhiteSpace(title)
                                    ? appName
                                    : string.Format("{1} - {0}", appName, title.Trim());
                Window.Title = windowTitle;
                if (AppTitleRunBlock != null)
                {
                    AppTitleRunBlock.Text = windowTitle;
                }
                return true;
            }
            catch { }
        }
        return false;
    }

    public async Task<bool> TryUpdateRegionsForCustomTitleBarAsync()
    {
        try
        {
            if (Window is null)
                return false;

            AppWindow.TitleBar.PreferredHeightOption = PreferredHeightOption;

            Window.SetTitleBar(this);

            double scaleAdjustment = XamlRoot.RasterizationScale;

            LeftInset = AppWindow.TitleBar.LeftInset / scaleAdjustment;
            RightInset = AppWindow.TitleBar.RightInset / scaleAdjustment;
            CaptionButtonHeight = AppWindow.TitleBar.Height / scaleAdjustment;

            AppTitleBarContainer.Height = CaptionButtonHeight;

            UpdateTitleAndIconVisibilityAndPosition();

            await Task.Delay(50);

            List<RectInt32> inputRegions = new(Content?.Children?.Count ?? 0);

            if (Content is not null)
            {
                foreach (FrameworkElement input in Content.Children.OfType<FrameworkElement>()
                                                                   .Where(f => f.IsHitTestVisible && f.Visibility == Visibility.Visible))
                {
                    GeneralTransform generalTransform = input.TransformToVisual(null);
                    Rect bounds = generalTransform.TransformBounds(new Rect(0, 0, input.ActualWidth, input.ActualHeight));
                    RectInt32 rect = bounds.ToRectInt32(scaleAdjustment);
                    inputRegions.Add(rect);
                }
            }
            InputNonClientPointerSource nonClientInputSrc = InputNonClientPointerSource.GetForWindowId(AppWindow.Id);
            nonClientInputSrc.SetRegionRects(NonClientRegionKind.Passthrough, [.. inputRegions]);
            return true;
        }
        catch
        {
            return false;
        }
    }

    private void UpdateTitleAndIconVisibilityAndPosition()
    {
        LeftColumn.Width = new GridLength(LeftInset);
        RightColumn.Width = new GridLength(RightInset);

        AppIcon.Visibility = DisplayOptions.HasFlag(TitleBarControlDisplayOptions.Icon) ? Visibility.Visible : Visibility.Collapsed;

        AppTitleTextBlock.Opacity = DisplayOptions.HasFlag(TitleBarControlDisplayOptions.Title) ? 1 : 0;
    }

    private void SetWindowActiveState(WindowActivationState state)
    {
        if (AppTitleTextBlock != null)
        {
            switch (state)
            {
                case WindowActivationState.Deactivated:
                    if (Content != null)
                        Content.Opacity = 0.4;

                    if (DisplayOptions.HasFlag(TitleBarControlDisplayOptions.Title))
                        AppTitleTextBlock.Opacity = 0.4;
                    break;

                case WindowActivationState.CodeActivated:
                case WindowActivationState.PointerActivated:
                    if (Content != null)
                        Content.Opacity = 1;

                    if (DisplayOptions.HasFlag(TitleBarControlDisplayOptions.Title))
                        AppTitleTextBlock.Opacity = 1;
                    break;
            }
        }
    }

    private void UpdateTheme() => Window?.AppWindow.SetTitleBarTheme(ActualTheme);

}
