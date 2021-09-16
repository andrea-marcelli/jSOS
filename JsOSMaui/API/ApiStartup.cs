using JsOSMaui.Models;
using JsOSMaui.Services.Shared.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;
using System;
using System.Linq;

namespace JsOSMaui.API
{
    public class ApiStartup
    {
        public ApiStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
#if WINDOWS10_0_17763_0_OR_GREATER
            services.AddControllers();
#endif

            //share same instance
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.StartsWith("JsOSMaui"));
            var types = assemblies
                            .SelectMany(x => x.GetTypes())
                            .Where(x => x.FullName.StartsWith("JsOSMaui.Services.Shared.Interfaces") && x.FullName.EndsWith("Service")).ToArray();

            foreach (var service in types)
            {
                services.AddSingleton(service, Core.ServiceProvider.Current.GetRequiredService(service));
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();

#if WINDOWS10_0_17763_0_OR_GREATER
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
#endif
        }
    }
}
