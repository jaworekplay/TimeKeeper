using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TimeKeeper.AttachedProperties
{
    public class TextBoxAP
    {
        public static bool GetFocusOnLoaded(DependencyObject obj)
        {
            return (bool)obj.GetValue(FocusOnLoadedProperty);
        }

        public static void SetFocusOnLoaded(DependencyObject obj, bool value)
        {
            obj.SetValue(FocusOnLoadedProperty, value);
        }

        // Using a DependencyProperty as the backing store for FocusOnLoaded.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FocusOnLoadedProperty =
            DependencyProperty.RegisterAttached("FocusOnLoaded", typeof(bool), typeof(TextBoxAP), new PropertyMetadata(false));


    }
}
