using JsOSMaui.Core;
using JsOSMaui.Models;
using JsOSMaui.Services.Shared.Interfaces;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace JsOSMaui.ViewModels
{
    public class AuthorizationsViewModel : INotifyPropertyChanged
    {
        // Services
        private IDatabaseService db;
        private IServerService serverService;
        private IMessageBusService messageBusService;

        // Commands
        public Command<List<object>> SelectChildPermission { get; init; }
        public Command SavePermission { get; init; }
        public Command<Settings> SaveSettings { get; init; }

        // Collections
        private ObservableCollection<AppPermission> _AppPermission = new ObservableCollection<AppPermission>();
        public ObservableCollection<AppPermission> AppPermission { get { return _AppPermission; } set { _AppPermission = value; OnPropertyChanged(); } }

        // Properties
        bool _StatusToUpdate = true;

        string _Title;
        public string Title { get { return _Title; } set { _Title = value; OnPropertyChanged(); } }

        string _ServerStatus;
        public string ServerStatus { get { return _ServerStatus; } set { _ServerStatus = value; OnPropertyChanged(); } }

        Settings _Settings;
        public Settings Settings { get { return _Settings; } set { _Settings = value; OnPropertyChanged(); } }

        public AuthorizationsViewModel()
        {
            this.db = ServiceProvider.GetService<IDatabaseService>();
            this.serverService = ServiceProvider.GetService<IServerService>();
            this.messageBusService = ServiceProvider.GetService<IMessageBusService>();

            // Init services
            this.messageBusService.RegisterEvent("appchanged", (appid) =>
{
                Application.Current.Dispatcher.BeginInvokeOnMainThread(() =>
                {
                    ReloadData();
                });

            });

            this.messageBusService.RegisterEvent("serverstatuschanged", (status) =>
            {
                ServerStatus = this.serverService.GetServerStatus();
                this.Title = "jSOS Server Status:" + ServerStatus;
                _StatusToUpdate = true;
            });

            // Commands
            SelectChildPermission = new Command<List<object>>((args) => OnSelectChildPermission(args));
            SavePermission = new Command(() => OnSavePermission());
            SaveSettings = new Command<Settings>((args) => OnSaveSettings(args));

            ReloadData();
        }

        private void ReloadData()
        {
            this.AppPermission.Clear();
            var apps = this.db.GetAppPermission().FindAll().ToList();

            foreach(var app in apps)
            {
                this.AppPermission.Add(app);
            }

            OnPropertyChanged(nameof(this.AppPermission));
            this.Settings = this.db.GetSettings();
        }

        private void OnSavePermission()
        {
            foreach (var app in this.AppPermission)
            {
                this.db.SavePermission(app);
            }
        }

        private void OnSaveSettings(Settings o)
        {
            this.db.SaveSettings(this.Settings);

            Application.Current.Dispatcher.BeginInvokeOnMainThread(() =>
            {
                this.serverService.RestartServer(this.Settings);
            });
        }

        private void OnSelectChildPermission(List<object> args)
        {
            AppPermission app = args[0] as AppPermission;
            AppPermission binded = AppPermission.FirstOrDefault(x => x.AppName == app.AppName);

            var enabled = (app.Enabled.HasValue == false || app.Enabled.Value == false);

            foreach (var item in binded.Needs)
            {
                item.Enabled = enabled;
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        } 
        #endregion
    }
}
