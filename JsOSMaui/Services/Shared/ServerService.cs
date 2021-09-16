using JsOSMaui.Models;
using JsOSMaui.Services.Shared.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using JsOSMaui.API;

namespace JsOSMaui.Services.Shared
{
//    public class ApiStartup
//    {

//        public ApiStartup(IConfiguration configuration)
//        {
//            Configuration = configuration;
//        }

//        public IConfiguration Configuration { get; }

//        // This method gets called by the runtime. Use this method to add services to the container.
//        public void ConfigureServices(IServiceCollection services)
//        {
//#if WINDOWS10_0_17763_0_OR_GREATER
//            services.AddControllers();
//#endif

//            //share same instance
//            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("JsOSMaui"));
//            var types = assemblies
//                            .SelectMany(x => x.GetTypes())
//                            .Where(x => x.FullName.StartsWith("JsOSMaui.Services.Shared.Interfaces") && x.FullName.EndsWith("Service")).ToArray();

//            foreach (var service in types)
//            {
//                services.AddSingleton(service, Core.ServiceProvider.Current.GetRequiredService(service));
//            }
//        }

//        public void Configure(IApplicationBuilder app)
//        {
//            app.UseDeveloperExceptionPage();
//            app.UseHttpsRedirection();

//#if WINDOWS10_0_17763_0_OR_GREATER
//            app.UseRouting();
//            app.UseAuthorization();

//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllers();
//            });
//#endif
//        }
//    }

    public class ServerService : IServerService
    {
        IMessageBusService _MessageBusService; //= ServiceProvider.GetService<IMessageBusService>();
        public ServerService(IMessageBusService messageBusService)
        {
            this._MessageBusService = messageBusService;
        }

        private string _ServerStatus;

        private IWebHost _Server = null;
        public void RestartServer(Settings settings)
        {
            StopServer(settings);

            if (settings.Enabled == false)
            {
                return;
            }

            this._Server = WebHost.CreateDefaultBuilder().UseKestrel(x =>
            {

                if (settings.AllowExternalIps)
                {
                    x.ListenAnyIP(settings.PortNumber);
                }
                x.ListenLocalhost(settings.PortNumber);

            }).UseStartup<ApiStartup>().UseDefaultServiceProvider((b, o) =>
            {

            })
             .Build();

            _ServerStatus = "Starting";


            this._MessageBusService.Emit("serverstatuschanged", _ServerStatus);

            Task.Run(() =>
            {
                Thread.Sleep(3000);
                _Server.RunAsync();
                _ServerStatus = "Started";
                this._MessageBusService.Emit("serverstatuschanged", _ServerStatus);
            });
        }

        public void StopServer(Settings settings)
        {
            if (this._Server != null)
            {
                _ServerStatus = "Shutting down";
                this._MessageBusService.Emit("serverstatuschanged", _ServerStatus);
                this._Server.StopAsync().Wait();

            }

            Thread.Sleep(3000);
            _ServerStatus = "Down";
            this._MessageBusService.Emit("serverstatuschanged", _ServerStatus);
        }

        public string GetServerStatus()
        {
            return _ServerStatus;
        }
    }
}
