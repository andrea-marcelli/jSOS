using JsOSMaui.Core;

namespace JsOSMaui.Models
{
    public class Need : ObservableObject
    {
        private string _Permission;
        private bool _Enabled;

        public string Permission { get => _Permission; set => SetAndNotify<string>(ref this._Permission, value, () => this.Permission); }
        public bool Enabled
        {
            get => _Enabled;
            set => SetAndNotify<bool>(ref this._Enabled, value, () => this.Enabled);
        }
    }
}