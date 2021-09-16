using JsOSMaui.Models;
using LiteDB;

namespace JsOSMaui.Services.Shared.Interfaces
{
    public interface IDatabaseService
    {
        Settings GetSettings();
        ILiteCollection<Permission> GetPermission();
        ILiteCollection<AppPermission> GetAppPermission();
        void SavePermission(AppPermission app);
        void SaveSettings(Settings settings);
    }
}
