using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Controls.Xaml;
using Microsoft.Extensions.DependencyInjection;
using JsOSMaui.Services;
using JsOSMaui.Services.Shared.Interfaces;
using JsOSMaui.Services.Shared;
using JsOSMaui.Models;

[assembly: XamlCompilationAttribute(XamlCompilationOptions.Compile)]
namespace JsOSMaui
{
    public class Startup : IStartup
	{
		public void Configure(IAppHostBuilder appBuilder)
		{
			appBuilder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				})
				.ConfigureServices(services => 
				{ 
					services.AddSingleton<INavigationService, ViewNavigationService>();
					services.AddSingleton<IConfigService, ConfigService>();
					services.AddSingleton<IMessageBusService, MessageBusService>();
					services.AddSingleton<IServerService, ServerService>();
					services.AddSingleton<IDatabaseService, DatabaseService>();
				});
		}
	}
}