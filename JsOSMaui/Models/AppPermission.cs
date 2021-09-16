using JsOSMaui.Core;
using LiteDB;
using System;
using System.ComponentModel;
using System.Linq;

namespace JsOSMaui.Models
{
    public class AppPermission : ObservableObject
    {
        private Guid _Id = Guid.Empty;
        private string _AppName = "APP NAME";
        private string _Token = "APP NAME";
        private BindingList<Need> _Needs = new BindingList<Need>();
        private bool? _Enabled = false;

        public Guid Id { get => _Id; set => SetAndNotify<Guid>(ref this._Id, value, () => this.Id); }
        public string AppName { get => _AppName; set => SetAndNotify<string>(ref this._AppName, value, () => this.AppName); }
        public string Token { get => _Token; set => SetAndNotify<string>(ref this._Token, value, () => this.Token); }
        public BindingList<Need> Needs
        {
            get => _Needs;
            set
            {
                SetAndNotify<BindingList<Need>>(ref this._Needs, value, () => this.Needs);
                this._Needs.ListChanged += Needs_ListChanged;
                UpdateEnabled();
            }
        }

        [BsonIgnore]
        public bool? Enabled { get => _Enabled; set => SetAndNotify<bool?>(ref this._Enabled, value, () => this.Enabled); }

        private void Needs_ListChanged(object sender, ListChangedEventArgs e)
        {
            SetAndNotify<BindingList<Need>>(ref this._Needs, this._Needs, () => this.Needs);
            UpdateEnabled();
        }

        private void UpdateEnabled()
        {
            var allOn = this._Needs.Count(x => x.Enabled == true);
            var allOff = this._Needs.Count(x => x.Enabled == false);
            var all = this._Needs.Count;
            if (all == allOff) Enabled = false;
            else if (all == allOn) Enabled = true;
            else Enabled = null;
        }
    }
}
