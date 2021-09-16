using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsOSMaui.Services.Shared.Interfaces
{
    public interface IConfigService
    {
        void Init();
        string GetAppPath(string filename = "");
    }
}
