using JsOSMaui.Services.Shared.Interfaces;
using System;
using System.IO;

namespace JsOSMaui.Services.Shared
{
    public class ConfigService : IConfigService
    {
        public void Init()
        {
            Directory.CreateDirectory(GetAppPath());
        }
        public string GetAppPath(string filename = "")
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), GetAppName(), filename);
        }

        private string GetAppName()
        {
            return "JsOS";
        }
    }
}
