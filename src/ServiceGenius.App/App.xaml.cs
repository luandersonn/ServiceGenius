﻿using Microsoft.UI.Xaml;

namespace ServiceGenius.App;

public partial class App : Application
{
    public App() => InitializeComponent();

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        m_window = new MainWindow();
        m_window.Activate();
    }

    private Window m_window;
}