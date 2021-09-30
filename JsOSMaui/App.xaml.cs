﻿using JsOSMaui.Models;
using JsOSMaui.Pages;
using JsOSMaui.Services.Shared;
using JsOSMaui.Services.Shared.Interfaces;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.PlatformConfiguration.WindowsSpecific;
using Application = Microsoft.Maui.Controls.Application;

namespace JsOSMaui
{
    public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			var navigationService = Core.ServiceProvider.GetService<INavigationService>();

			navigationService.Configure("MainPage", typeof(HomePage));
			navigationService.Configure("SettingsPage", typeof(SettingsPage));
			navigationService.Configure("AuthPage", typeof(AuthorizationsPage));
            navigationService.Configure("AuthRequest", typeof(AuthorizationRequestPage));

			var mainPage = ((ViewNavigationService)navigationService).SetRootPage("AuthPage");

			// Either set "MainPage" or override "CreateWindow"
			MainPage = mainPage;

            Init();
		}

        // Either set "MainPage" or override "CreateWindow"
        //protected override Window CreateWindow(IActivationState activationState) =>
        //    new Window(new NavigationPage(new HomePage())) { Title = "Weather TwentyOne" };

        private void Init()
        {
            var config = JsOSMaui.Core.ServiceProvider.Current.GetService(typeof(IConfigService)) as IConfigService;
            config.Init();

            //TODO: move on init service

            var db = Core.ServiceProvider.Current.GetService(typeof(IDatabaseService)) as IDatabaseService;
            var settings = db.GetSettings();
            if (settings == null)
            {
                settings = new Settings();
                db.SaveSettings(settings);
            }


            var serverService = Core.ServiceProvider.Current.GetService(typeof(IServerService)) as IServerService;
            serverService.RestartServer(settings);
        }
    }
}
