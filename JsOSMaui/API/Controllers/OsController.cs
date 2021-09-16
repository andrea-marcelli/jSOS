using JsOSMaui.Models;
using JsOSMaui.Services.Shared;
using System;
using Microsoft.AspNetCore.Mvc;
using JsOSMaui.Services.Shared.Interfaces;
using System.Diagnostics;

namespace JsOSMaui.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OSController : BaseController
    {
        public OSController(IDatabaseService databaseService, IMessageBusService messageBusService) : base(databaseService, messageBusService) { }


        [HttpGet("whoami")]
        public string Whoami()
        {
            ComputePermission("os/read", HttpContext);
            return Environment.UserDomainName + "\\" + Environment.UserName;
        }


        [HttpGet("username")]
        public string GetUsername()
        {
            ComputePermission("os/read", HttpContext);
            return Environment.UserName;
        }


        [HttpGet("domainname")]
        public string GetDomainName()
        {
            ComputePermission("os/read", HttpContext);
            return Environment.UserDomainName;
        }

        [HttpGet("variable")]
        public string GetVariable(string variableName)
        {
            ComputePermission("os/read", HttpContext);
            return Environment.GetEnvironmentVariable(variableName);
        }

        [HttpGet("cpu")]
        public float GetCPUUsed()
        {
            ComputePermission("os/read", HttpContext);
#if WINDOWS10_0_17763_0_OR_GREATER
            PerformanceCounter cpuCounter;
            cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            return cpuCounter.NextValue();
#endif
            return (float)0;
        }

        [HttpGet("ram")]
        public float GetRAMAvailable()
        {
            ComputePermission("os/read", HttpContext);
#if WINDOWS10_0_17763_0_OR_GREATER
            PerformanceCounter ramCounter;
            ramCounter = new PerformanceCounter("Memory", "Available MBytes");
            return ramCounter.NextValue();
#endif
            return (float)0;
        }
    }
}
