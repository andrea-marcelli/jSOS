using JsOSMaui.Models;

namespace JsOSMaui.Services.Shared.Interfaces
{
    public interface IServerService
    {
        void RestartServer(Settings settings);
        void StopServer(Settings settings);
        string GetServerStatus();
    }
}