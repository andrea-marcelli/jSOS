using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using JsOSMaui.Services.Shared.Interfaces;

namespace JsOSMaui.API.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController(IDatabaseService databaseService, IMessageBusService messageBusService)
        {
            this.DatabaseService = databaseService;
            this.MessageBusService = messageBusService;

        }

        public IDatabaseService DatabaseService { get; set; }
        public IMessageBusService MessageBusService { get; set; }


        public void ComputePermission(string permission, HttpContext httpContext)
        {
            var token = httpContext.Request.Headers["Token"].ToList().FirstOrDefault();
            var appname = httpContext.Request.Headers["AppName"].ToList().FirstOrDefault();

            var app = this.DatabaseService.GetAppPermission().FindOne(x => appname.Equals(x.AppName, StringComparison.InvariantCultureIgnoreCase)
              && x.Token.Equals(token, StringComparison.InvariantCultureIgnoreCase));
            if (app == null) throw new Exception("App not found");
            if (!app.Needs.Any(x => x.Enabled && x.Permission.Equals(permission, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new Exception("Not authorized");
            }
        }
    }
}
