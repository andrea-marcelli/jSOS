//using Microsoft.Maui;
//using Microsoft.Maui.Hosting;
//using Microsoft.Maui.Controls.Hosting;
//using Microsoft.Maui.Controls.Xaml;
//using Microsoft.Extensions.DependencyInjection;
//using JsOSMaui.builder.Services;
//using JsOSMaui.builder.Services.Shared.Interfaces;
//using JsOSMaui.builder.Services.Shared;
//using JsOSMaui.Models;
//using Microsoft.AspNetCore.Hosting;

//[assembly: XamlCompilationAttribute(XamlCompilationOptions.Compile)]
//namespace JsOSMaui
//{
//    public class Startup : IStartup
//	{
//		public void Configure(IApplicationBuilderApp appBuilder)
//		{
//			appBuilder
//				.UseMauiApp<App>()
//				.ConfigureFonts(fonts =>
//				{
//					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
//				})
//				.Configurebuilder.Services(builder.Services => 
//				{ 
//					builder.Services.AddSingleton<INavigationService, ViewNavigationService>();
//					builder.Services.AddSingleton<IConfigService, ConfigService>();
//					builder.Services.AddSingleton<IMessageBusService, MessageBusService>();
//					builder.Services.AddSingleton<IServerService, ServerService>();
//					builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
//				});
//		}
//	}
//}

using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Extensions.DependencyInjection;
using JsOSMaui.Services.Shared;
using JsOSMaui.Services.Shared.Interfaces;

namespace JsOSMaui
{
    public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

                builder.Services.AddSingleton<INavigationService, ViewNavigationService>();
                builder.Services.AddSingleton<IConfigService, ConfigService>();
                builder.Services.AddSingleton<IMessageBusService, MessageBusService>();
                builder.Services.AddSingleton<IServerService, ServerService>();
                builder.Services.AddSingleton<IDatabaseService, DatabaseService>();

            return builder.Build();
		}
	}
}