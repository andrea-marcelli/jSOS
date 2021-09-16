using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsOSMaui.Models
{
    public class AppPermissionRequest
    {
        public Guid? Id { get; set; }
        public string AppName { get; set; } = "APP NAME";
        public string Token { get; set; } = "APP NAME";
        public List<string> Needs { get; set; } = new List<string>();
        public bool Async { get; set; } = true;
    }
}
