using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace TracelogTest.View.Behavior
{
    public static class PlaceholderBehavior
    {
        #region Declaration
        #endregion

        #region Fields
        #endregion

        #region Properties

        public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.RegisterAttached(
            "PlaceholderText",
            typeof(string),
            typeof(PlaceholderBehavior),
            new PropertyMetadata(null, OnPlaceholderChanged)
            );

        #endregion

        #region Public methods

        public static void SetPlaceholderText(TextBox textBox, string placeholder)
        {
            textBox.SetValue(PlaceholderTextProperty, placeholder);
        }

        public static string GetPlaceHolderText(TextBox textBox)
        {
            return textBox.GetValue(PlaceholderTextProperty) as string;
        }

        #endregion

        #region Private methods

        static void OnPlaceholderChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null)
                return;

            var placeHolder = e.NewValue as string;
            var handler = CreateEventHandler(placeHolder);
            if (string.IsNullOrEmpty(placeHolder))
            {
                textBox.TextChanged -= handler;
            }
            else
            {
                textBox.TextChanged += handler;
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Background = CreateVisualBrush(placeHolder);
                }
            }
        }

        static TextChangedEventHandler CreateEventHandler(string placeholder)
        {
            return (sender, e) =>
            {
                var textBox = (TextBox)sender;
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Background = CreateVisualBrush(placeholder);
                }
                else
                {
                    textBox.Background = new SolidColorBrush(Colors.Transparent);
                }
            };
        }

        static VisualBrush CreateVisualBrush(string placeholder)
        {
            var visual = new Label()
            {
                Content = placeholder,
                Padding = new Thickness(5, 1, 1, 1),
                Foreground = new SolidColorBrush(Colors.LightGray),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };
            return new VisualBrush(visual)
            {
                Stretch = Stretch.None,
                TileMode = TileMode.None,
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Center
            };
        }

        #endregion
    }
}