using JsOSMaui.Core;
using JsOSMaui.Models;
using JsOSMaui.Services.Shared.Interfaces;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JsOSMaui.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        // Services
        private IDatabaseService db;
        private IServerService serverService;

        // Commands
        public Command<Settings> SaveSettings { get; set; }

        // Properties
        Settings _Settings;
        public Settings Settings { get { return _Settings; } set { _Settings = value; OnPropertyChanged(); } }
        public SettingsViewModel()
        {
            this.db = ServiceProvider.GetService<IDatabaseService>();
            this.serverService = ServiceProvider.GetService<IServerService>();

            // Commands
            SaveSettings = new Command<Settings>(OnSaveSettings);
        }

        private void OnSaveSettings(Settings o)
        {
            this.db.SaveSettings(this.Settings);

            Application.Current.Dispatcher.BeginInvokeOnMainThread(() =>
            {
                this.serverService.RestartServer(this.Settings);
            });
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