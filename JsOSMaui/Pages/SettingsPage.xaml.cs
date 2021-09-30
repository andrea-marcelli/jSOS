using JsOSMaui.Core;
using JsOSMaui.Services;
using JsOSMaui.ViewModels;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsOSMaui.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage, IView
    {
        public SettingsPage()
        {
            InitializeComponent();
        }
    }
}
