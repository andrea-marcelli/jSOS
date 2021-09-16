using JsOSMaui.Models;
using JsOSMaui.Services.Shared.Interfaces;
using LiteDB;
using System;
using System.Linq;

namespace JsOSMaui.Services.Shared
{
    public class DatabaseService : IDatabaseService
    {
        private LiteDatabase _Db;
        private IConfigService _IConfigService;

        private ILiteCollection<AppPermission> _AppPermissionCollection;
        private ILiteCollection<Permission> _PermissionCollection;
        private ILiteCollection<Settings> _SettingsCollection;

        public DatabaseService(IConfigService IConfigService)
        {
            this._IConfigService = IConfigService;

            _Db = new LiteDatabase(this._IConfigService.GetAppPath("data.db"));
            
            // Get customer collection
            this._PermissionCollection = _Db.GetCollection<Permission>("Permission");
            this._AppPermissionCollection = _Db.GetCollection<AppPermission>("AppPermission");
            this._SettingsCollection = _Db.GetCollection<Settings>("Settings");
        }

        public ILiteCollection<Permission> GetPermission()
        {
            return this._PermissionCollection;
        }

        public ILiteCollection<AppPermission> GetAppPermission()
        {
            return this._AppPermissionCollection;
        }

        public void SavePermission(AppPermission app)
        {
            if (Guid.Empty.Equals(app.Id))
            {
                app.Id = Guid.NewGuid();
                this._AppPermissionCollection.Insert(app);
                return;
            }

            this._AppPermissionCollection.Update(app);
        }

        public Settings GetSettings()
        {
            return this._SettingsCollection.FindAll().FirstOrDefault();
        }

        public void SaveSettings(Settings settings)
        {
            var saved = GetSettings();
            if (saved == null)
            {
                settings.Id = Guid.NewGuid();
                this._SettingsCollection.Insert(settings);
                return;
            }

            settings.Id = saved.Id; //caller cannot change id
            this._SettingsCollection.Update(settings);
        }
    }
}