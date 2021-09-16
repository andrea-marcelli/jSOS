using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsOSMaui.Models;
using JsOSMaui.ViewModels;

namespace JsOSMaui.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationRequestPage : ContentPage, IPage
    {
        public AuthorizationRequestPage(AppPermission appPermission) : base()
        {
            this.BindingContext = new AuthorizationRequestViewModel(appPermission);
        }
        public AuthorizationRequestPage()
        {
            InitializeComponent();

            this.BindingContext = new AuthorizationRequestViewModel(null);
        }
    }
}
