using System;
using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using GroupProject.Scripts;
using MySql.Data.MySqlClient;

namespace GroupProject.Views;

public partial class PickATopicPageView : UserControl
{
    public PickATopicPageView()
    {
        InitializeComponent();
    }
}