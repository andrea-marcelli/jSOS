using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsOSMaui.Core
{
    public static class ViewModelLocator
    {
        public static readonly BindableProperty AutoWireViewModelProperty =
               BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }

        /// <summary>
        /// Auto-bind current View with the corrisponding ViewModel on a name convention basis. This does not imply any
        /// Dependency Injection support.
        /// </summary>
        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is Element view))
            {
                return;
            }

            var viewType = view.GetType();

            var viewModelName = viewType.FullName.Replace(".Views.", ".ViewModels.").Replace("Page", "ViewModel");
            var viewModelType = Type.GetType(viewModelName);

            if (viewModelType == null) { return; }

            object vmInstance = null;

            if ((vmInstance = Activator.CreateInstance(viewModelType)) != null)
            {
                view.BindingContext = vmInstance;
            }
        }
    }
}