using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace groenteBoer.Helpers
{
    public static class TextBoxHelper
    {
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.RegisterAttached(
                "Placeholder",
                typeof(string),
                typeof(TextBoxHelper),
                new PropertyMetadata(string.Empty, OnPlaceholderChanged)
            );

        public static string GetPlaceholder(TextBox textBox) => (string)textBox.GetValue(PlaceholderProperty);

        public static void SetPlaceholder(TextBox textBox, string value) => textBox.SetValue(PlaceholderProperty, value);

        private static void OnPlaceholderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                // Set initial text to placeholder and change text color to gray
                textBox.Text = e.NewValue?.ToString();
                textBox.Foreground = Brushes.Gray;

                textBox.GotFocus += (s, ev) =>
                {
                    // When the TextBox gains focus, clear the text if it is the placeholder
                    if (textBox.Text == (string)e.NewValue)
                    {
                        textBox.Text = "";
                        textBox.Foreground = Brushes.Black;
                    }
                };

                textBox.LostFocus += (s, ev) =>
                {
                    // Only set placeholder if the TextBox is empty
                    if (string.IsNullOrWhiteSpace(textBox.Text))
                    {
                        textBox.Text = (string)e.NewValue;
                        textBox.Foreground = Brushes.Gray;
                    }
                };
            }
        }
    }
}
