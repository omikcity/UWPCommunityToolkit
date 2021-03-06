﻿using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Documents;

namespace Microsoft.Toolkit.Uwp.UI.Extensions
{
    /// <summary>
    /// Provides attached dependency properties for the <see cref="Hyperlink"/> content element that allows
    /// it to invoke a <see cref="ICommand"/> when clicked
    /// </summary>
    public static class HyperlinkExtensions
    {
        private static void OnCommandPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            Hyperlink hyperlink = sender as Hyperlink;

            if (hyperlink != null)
            {
                hyperlink.Click -= OnHyperlinkClicked;

                ICommand command = args.NewValue as ICommand;

                if (command != null)
                {
                    hyperlink.Click += OnHyperlinkClicked;
                }
            }
        }

        private static void OnHyperlinkClicked(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            ICommand command = GetCommand(sender);
            object parameter = GetCommandParameter(sender);

            if (command != null)
            {
                command.Execute(parameter);
            }
        }

        /// <summary>
        /// Attached <see cref="DependencyProperty"/> for binding an <see cref="ICommand"/> instance to a <see cref="Hyperlink"/>
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(HyperlinkExtensions), new PropertyMetadata(null, OnCommandPropertyChanged));

        /// <summary>
        /// Gets the <see cref="ICommand"/> instance assocaited with the specified <see cref="DependencyObject"/>
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> from which to get the associated <see cref="ICommand"/> instance</param>
        /// <returns>The <see cref="ICommand"/> instance associated with the the <see cref="DependencyObject"/> or null</returns>
        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        /// <summary>
        /// Sets the <see cref="ICommand"/> instance assocaited with the specified <see cref="DependencyObject"/>
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> to associated the <see cref="ICommand"/> instance to</param>
        /// <param name="value">The <see cref="ICommand"/> instance to bind to the <see cref="DependencyObject"/></param>
        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Attached <see cref="DependencyProperty"/> for binding a command parameter to a <see cref="Hyperlink"/>
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(HyperlinkExtensions), new PropertyMetadata(null));

        /// <summary>
        /// Gets the <see cref="CommandProperty"/> instance assocaited with the specified <see cref="DependencyObject"/>
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> from which to get the associated <see cref="CommandProperty"/> value</param>
        /// <returns>The <see cref="CommandProperty"/> value associated with the the <see cref="DependencyObject"/> or null</returns>
        public static object GetCommandParameter(DependencyObject obj)
        {
            return obj.GetValue(CommandParameterProperty);
        }

        /// <summary>
        /// Sets the <see cref="CommandProperty"/> assocaited with the specified <see cref="DependencyObject"/>
        /// </summary>
        /// <param name="obj">The <see cref="DependencyObject"/> to associated the <see cref="CommandProperty"/> instance to</param>
        /// <param name="value">The <see cref="object"/> to set the <see cref="CommandProperty"/> to</param>
        public static void SetCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }
    }
}
