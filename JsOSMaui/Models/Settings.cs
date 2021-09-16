using JsOSMaui.Core;
using System;

namespace JsOSMaui.Models
{
    public class Settings : ObservableObject
    {
        private Guid _Id = Guid.Empty;
        private bool _Enabled = true;
        private bool _AllowExternalIps = false;
        private int _PortNumber = 54320;
        private int _PortNumberSSL = 54321;

        public Guid Id { get => _Id; set => SetAndNotify<Guid>(ref this._Id, value, () => this.Id); }
        public bool Enabled
        {
            get => _Enabled;
            set => SetAndNotify<bool>(ref this._Enabled, value, () => this.Enabled);
        }
        public bool AllowExternalIps
        {
            get => _AllowExternalIps;
            set => SetAndNotify<bool>(ref this._AllowExternalIps, value, () => this.AllowExternalIps);
        }
        public int PortNumber { get => _PortNumber; set => SetAndNotify<int>(ref this._PortNumber, value, () => this.PortNumber); }
        public int PortNumberSSL { get => _PortNumberSSL; set => SetAndNotify<int>(ref this._PortNumberSSL, value, () => this.PortNumberSSL); }
    }
}
