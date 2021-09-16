using JsOSMaui.Core;
using JsOSMaui.Services.Shared;
using JsOSMaui.Services.Shared.Interfaces;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace JsOSMaui.ViewModels
{
    public class NavBarViewModel
    {
        // Services
        // TODO: Do proper DI
        private INavigationService _NavigationService => ServiceProvider.GetService<INavigationService>();

        // Commands
        public Command GoHomeCommand { get; init; }
        public Command GoToSettingsPageCommand { get; init; }
        public Command GoToAuthorizationsPageCommand { get; init; }

        // View Triggers
        string activeTab;

        public string ActiveTab
        {
            get
            {
                return activeTab;
            }
            set
            {
                activeTab = value;

                var target = activeTab;
                var homeImg = (target == "MainPage") ? "tab_home_on.png" : "tab_home.png";
                var authImg = (target == "AuthPage") ? "tab_favorites_on.png" : "tab_favorites.png";
                var settingsImg = (target == "Settings") ? "tab_settings_on.png" : "tab_settings.png";

                HomeImg = ImageSource.FromFile(homeImg);
                AuthImg = ImageSource.FromFile(authImg);
                SettingsImg = ImageSource.FromFile(settingsImg);
            }
        }

        private ImageSource homeImg = ImageSource.FromFile("tab_home.png");
        public ImageSource HomeImg { get => homeImg; set { homeImg = value; } }

        private ImageSource settingsImg = ImageSource.FromFile("tab_settings.png");
        public ImageSource SettingsImg { get => settingsImg; set { settingsImg = value; } }

        private ImageSource authImg = ImageSource.FromFile("tab_favorites.png");
        public ImageSource AuthImg { get => authImg; set { authImg = value; } }

        public NavBarViewModel()
        {
            // Active Tab
            ActiveTab = this._NavigationService.CurrentPageKey;

            // Commands
            GoHomeCommand = new Command(async () => await DoNavigatePage("MainPage"));
            GoToSettingsPageCommand = new Command(async () => await DoNavigatePage("SettingsPage"));
            GoToAuthorizationsPageCommand = new Command(async () => await DoNavigatePage("AuthPage"));
        }

        private async Task DoNavigatePage(string pageKey)
        {
            ActiveTab = pageKey;
            await this._NavigationService.NavigateAsync(ActiveTab);
        }
    }
}