using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Cronus.ViewModels;

namespace Cronus.Utilities
{
    public class WindowCloser
    {
        // Using a dependency property as the backing store for
        // EnableWindowClosing.
        public static readonly DependencyProperty EnableWindowClosingProperty =
            DependencyProperty.RegisterAttached(
                "EnableWindowClosing",
                typeof(bool),
                typeof(WindowCloser),
                new PropertyMetadata(false, OnEnableWindowClosingChanged));



        public static bool GetEnableWindowClosing(DependencyObject obj)
        {
            return (bool)obj.GetValue(EnableWindowClosingProperty);
        }


        public static void SetEnableWindowClosing(DependencyObject obj,
            bool value)
        {
            obj.SetValue(EnableWindowClosingProperty, value);
        }



        private static void OnEnableWindowClosingChanged(
            DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if (obj is Window window)
            {
                window.Loaded += (s, e) =>
                {
                    if (window.DataContext is MainViewModel viewModel)
                    {
                        window.Closing += (s, e) =>
                        {
                            // The view-model's OnWindowClosing method
                            // returns true if the user wishes to
                            // continue with the closing process, false
                            // otherwise, so we invert the value.
                            e.Cancel = !viewModel.OnWindowClosing(s, e);
                        };
                    }
                };
            }
        }
    }
}
