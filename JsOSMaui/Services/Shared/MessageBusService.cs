using JsOSMaui.Services.Shared.Interfaces;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

namespace JsOSMaui.Services.Shared
{
    public class MessageBusService : IMessageBusService
    {
        Dictionary<string, List<Action<object>>> _Events = new Dictionary<string, List<Action<object>>>();
        object _Sequence = "";
        public void RegisterEvent(string name, Action<object> eventHandler)
        {
            List<Action<object>> eventActions = new List<Action<object>>();
            if (_Events.ContainsKey(name))
            {
                eventActions = _Events[name];
            }
            eventActions.Add(eventHandler);

            _Events[name] = eventActions;
        }

        public void Emit(string eventName, object argument)
        {
            lock (_Sequence)
            {
                if (_Events.ContainsKey(eventName))
                {
                    Application.Current.Dispatcher.BeginInvokeOnMainThread(() =>
                    {
                        var eventActions = _Events[eventName];
                        eventActions.ForEach(x => x.Invoke(argument));
                    });
                }
            }
        }
    }
}