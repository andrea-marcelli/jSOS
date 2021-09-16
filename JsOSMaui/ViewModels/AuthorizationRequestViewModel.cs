using JsOSMaui.Core;
using JsOSMaui.Models;
using JsOSMaui.Services.Shared;
using JsOSMaui.Services.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsOSMaui.ViewModels
{
    public class AuthorizationRequestViewModel
    {
        private readonly IDatabaseService _DatabaseService;
        private readonly INavigationService _NavigationService;
        private readonly IMessageBusService _MessageBusService;

        //public AuthorizationRequestViewModel() { }
        public AuthorizationRequestViewModel(AppPermission appToSave)
        {
            this._DatabaseService = ServiceProvider.GetService<IDatabaseService>();
            this._NavigationService = ServiceProvider.GetService<INavigationService>();
            this._MessageBusService = ServiceProvider.GetService<IMessageBusService>();


            App.Current.Dispatcher.BeginInvokeOnMainThread(async () =>
            {
                await NotifyUser(appToSave);
                await this._NavigationService.GoBack();
            });
        }

        public async Task NotifyUser(AppPermission appToSave)
        {
            var msg = $"Allow app {appToSave.AppName} to access permission:";

            foreach (var item in appToSave.Needs)
            {
                msg += " " + item.Permission;
            }

            if (await App.Current.MainPage.DisplayAlert("Permission request", msg, "Ok", "Cancel"))
            {
                foreach (var item in appToSave.Needs)
                {
                    item.Enabled = true;
                }

                this._DatabaseService.SavePermission(appToSave);
            }

            this._MessageBusService.Emit("appchanged", null);
        }
    }
}
