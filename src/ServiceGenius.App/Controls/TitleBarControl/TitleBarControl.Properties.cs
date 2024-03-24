using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;

namespace ServiceGenius.App.Controls;

public partial class TitleBarControl
{
    public Window Window { get; private set; }
    public AppWindow AppWindow => Window.AppWindow;

    private Panel AppTitleBarContainer { get; set; }
    private Image AppIcon { get; set; }
    private Run AppTitleRunBlock { get; set; }
    private Run AppTitleVersionRunText { get; set; }
    private UIElement AppTitleTextBlock { get; set; }
    private ColumnDefinition LeftColumn { get; set; }
    private ColumnDefinition RightColumn { get; set; }

    public static readonly DependencyProperty ContentProperty
        = DependencyProperty.Register("Content",
                                      typeof(Panel),
                                      typeof(TitleBarControl),
                                      new PropertyMetadata(null));

    public Panel Content { get => (Panel)GetValue(ContentProperty); set => SetValue(ContentProperty, value); }


    public double LeftInset
    {
        get => (double)GetValue(LeftInsetProperty);
        private set => SetValue(LeftInsetProperty, value);
    }
    public static readonly DependencyProperty LeftInsetProperty =
        DependencyProperty.Register(
            "LeftInset",
            typeof(double),
            typeof(TitleBarControl),
            new PropertyMetadata(0.0d));


    public double RightInset
    {
        get => (double)GetValue(RightInsetProperty);
        private set => SetValue(RightInsetProperty, value);
    }
    public static readonly DependencyProperty RightInsetProperty =
        DependencyProperty.Register("RightInset",
                                    typeof(double),
                                    typeof(TitleBarControl),
                                    new PropertyMetadata(0.0d));

    public double CaptionButtonHeight
    {
        get => (double)GetValue(CaptionButtonHeightProperty);
        set => SetValue(CaptionButtonHeightProperty, value);
    }
    public static readonly DependencyProperty CaptionButtonHeightProperty =
        DependencyProperty.Register("CaptionButtonHeight",
                                    typeof(double),
                                    typeof(TitleBarControl),
                                    new PropertyMetadata(0.0d));



    public string Title
    {
        get => (string)GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }
    public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title",
                                    typeof(string),
                                    typeof(TitleBarControl),
                                    new PropertyMetadata(null, OnTitleChanged));



    public TitleBarHeightOption PreferredHeightOption
    {
        get => (TitleBarHeightOption)GetValue(PreferredHeightOptionProperty);
        set => SetValue(PreferredHeightOptionProperty, value);
    }
    public static readonly DependencyProperty PreferredHeightOptionProperty =
        DependencyProperty.Register("PreferredHeightOption",
                                    typeof(TitleBarHeightOption),
                                    typeof(TitleBarControl),
                                    new PropertyMetadata(TitleBarHeightOption.Standard, OnPreferredHeightOption));


    public TitleBarControlDisplayOptions DisplayOptions
    {
        get => (TitleBarControlDisplayOptions)GetValue(DisplayOptionsProperty);
        set => SetValue(DisplayOptionsProperty, value);
    }
    public static readonly DependencyProperty DisplayOptionsProperty =
        DependencyProperty.Register("DisplayOptions",
                                    typeof(TitleBarControlDisplayOptions),
                                    typeof(TitleBarControl),
                                    new PropertyMetadata(TitleBarControlDisplayOptions.Icon | TitleBarControlDisplayOptions.Title, OnDisplayOptionsChanged));
}