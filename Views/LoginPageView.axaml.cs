using System;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using MySql.Data.MySqlClient;
using GroupProject.Scripts;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Security.Cryptography;
using GroupProject.ViewModels;

namespace GroupProject.Views
{
    public partial class LoginPageView : UserControl
    {
        public LoginPageView()
        {
            InitializeComponent();
        }
    }
}