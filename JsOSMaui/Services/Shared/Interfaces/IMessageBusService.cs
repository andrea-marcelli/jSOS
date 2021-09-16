using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsOSMaui.Services.Shared.Interfaces
{
    public interface IMessageBusService
    {
        void RegisterEvent(string name, Action<object> eventHandler);
        void Emit(string eventName, object argument);
    }
}
