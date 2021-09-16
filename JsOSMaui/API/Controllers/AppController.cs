using JsOSMaui.Models;
using System;
using Microsoft.AspNetCore.Mvc;
using JsOSMaui.Services.Shared.Interfaces;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;

namespace JsOSMaui.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : BaseController
    {
        private readonly INavigationService _NavigationService;

        public AppController(IDatabaseService databaseService, IMessageBusService messageBusService, INavigationService navigationService) : base(databaseService, messageBusService) 
        {
            this._NavigationService = navigationService;
        }

        [HttpPost("register")]
        public async Task<bool> Register(AppPermissionRequest request)
        {
            var appCandidate = this.DatabaseService.GetAppPermission().FindOne((x) => x.AppName == request.AppName);
            if (appCandidate != null && appCandidate.Token != request.Token)
            {
                throw new Exception("Bad change request");
            }

            var appToSave = GetAppToSave(request);
            if (request.Id.HasValue) 
            {
                //an update request reset all permission (also the already given)
                this.DatabaseService.SavePermission(appToSave);
            }

            if (!request.Async)
            {
                var msg = $"Allow app {appToSave.AppName} to access permission:";

                foreach (var item in appToSave.Needs)
                {
                    msg += " " + item.Permission;
                }

                Application.Current.Dispatcher.BeginInvokeOnMainThread(async () => await this._NavigationService.NavigateModalAsync("AuthRequest", appToSave, true));
                return true;
            }

            this.MessageBusService.Emit("appchanged", null);
            return false;
        }

        private static AppPermission GetAppToSave(AppPermissionRequest request)
        {
            var appToSave = new AppPermission();
            appToSave.AppName = request.AppName;
            appToSave.Id = request.Id ?? appToSave.Id;
            request.Needs.ForEach(x => appToSave.Needs.Add(new Need() { Permission = x, Enabled = false }));
            appToSave.Token = request.Token;
            return appToSave;
        }
    }
}
