using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Brushes = System.Windows.Media.Brushes;

namespace Font_Chooser
{
    public partial class MainWindow : Window
    {
        private double fontSize = 56;
        private System.Windows.Media.Brush selectedButtonBackground = Brushes.Gainsboro;
        private System.Windows.Media.Brush defaultButtonBackground = Brushes.White;
        private System.Timers.Timer textUpdateTimer = new System.Timers.Timer();

        public MainWindow()
        {
            InitializeComponent();
            LoadFonts("testing");
            uiFontSize.Content = fontSize;
            uiSample.Focus();
        }

        private void LoadFonts(string text)
        {
            uiFontList.Children.Clear();
            List<string> fonts = new List<string>();
            var installedFonts = new InstalledFontCollection();
            foreach (var font in installedFonts.Families)
            {
                fonts.Add(font.Name);
                var button = GetFontButton(text, font.Name);
                button.Click += FontButton_Click;

                var tagButton = GetFontButton(text, font.Name);
                tagButton.Click += RemoveSelectedButton_Click;
                button.Tag = tagButton;
                uiFontList.Children.Add(button);
            }
        }

        private FontButton GetFontButton(string? text, string fontName)
        {
            var button = new FontButton
            {
                Content = $"{fontName}:{(string.IsNullOrWhiteSpace(text) ? "" : " " + text)}",
                FontFamily = new System.Windows.Media.FontFamily(fontName),
                BorderBrush = Brushes.Transparent,
                BorderThickness = new Thickness(0.5),
                Margin = new Thickness(2, 3, 2, 3),
                Padding = new Thickness(4, 2, 4, 2),
                FontSize = fontSize,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Background = Brushes.White,
                ToolTip = fontName
            };
            return button;
        }

        private void FontButton_Click(object sender, RoutedEventArgs e)
        {
            FontButtonClick(sender);
        }

        private void FontButtonClick(object sender)
        {
            var source = ToFontButton(sender);
            var fontName = source.FontFamily.Source;
            if (source.IsSelected)
            {
                var button = GetFontButton(source.Content.ToString(), fontName);
                button.Content = uiSample.Text;
                button.Click += RemoveSelectedButton_Click;
                uiSelectedFontList.Children.Add(button);
            }
            else
            {
                for (int i = 0; i < uiSelectedFontList.Children.Count; i++)
                {
                    var child = ToFontButton(uiSelectedFontList.Children[i]);
                    var selectedFont = child.FontFamily.Source;
                    if (selectedFont == fontName) { uiSelectedFontList.Children.RemoveAt(i); }
                }
            }
        }

        private void RemoveSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            var source = ToFontButton(sender);
            var font = source.ToolTip.ToString();
            for (int i = 0; i < uiFontList.Children.Count; i++)
            {
                var selectedFont = ToFontButton(uiFontList.Children[i]).FontFamily.Source;
                if (selectedFont == font) { ToFontButton(uiFontList.Children[i]).DeSelect(); }
            }
            uiSelectedFontList.Children.Remove(source);
        }

        private void updateText(string text)
        {
            LoadFonts(text);
            for (int i = 0; i < uiFontList.Children.Count; i++)
            {
                var selectedFont = ToFontButton(uiFontList.Children[i]).FontFamily.Source;
                ToFontButton(uiFontList.Children[i]).Content = $"{selectedFont}: {text}";
            }
            for (int i = 0; i < uiSelectedFontList.Children.Count; i++)
            {
                var selectedFont = ToFontButton(uiSelectedFontList.Children[i]).FontFamily.Source;
                ToFontButton(uiSelectedFontList.Children[i]).Content = text;
            }
        }

        private void uiSelectAll_Click(object sender, RoutedEventArgs e)
        {
            uiSelectedFontList.Children.Clear();
            var check = (CheckBox)sender;
            var select = check.IsChecked.HasValue ? check.IsChecked.Value : false;

            if (select) 
            {
                foreach (var button in uiFontList.Children)
                {
                    ToFontButton(button).Select();
                    FontButtonClick(button);
                }
            }
            else 
            { 
                foreach (var button in uiFontList.Children)
                {
                    ToFontButton(button).DeSelect();
                }
            }
        }

        private FontButton ToFontButton(object sender)
        {
            if(sender.GetType() != typeof(FontButton)) { throw new Exception($"Event handler expects FontButton, {sender.GetType().Name} was supplied."); }
            return (FontButton)sender;
        }

        private void uiUpdateText_Click(object sender, RoutedEventArgs e)
        {
            updateText(uiSample.Text);
        }

        private void uiDecrease_Click(object sender, RoutedEventArgs e)
        {
            fontSize -= 4;
            uiFontSize.Content = fontSize;

            int bit = 1;
            bool b = Convert.ToBoolean(bit);
            
            //foreach (var child in uiFontList.Children)
            //{
            //    var button = (FontButton)child;
            //    button.FontSize = fontSize;
            //}

            foreach (var child in uiSelectedFontList.Children)
            {
                var button = (FontButton)child;
                button.FontSize = fontSize;
            }

            //updateText(uiSample.Text);
        }

        private void uiIncrease_Click(object sender, RoutedEventArgs e)
        {
            fontSize += 4;
            uiFontSize.Content = fontSize;
            //foreach (var child in uiFontList.Children)
            //{
            //    var button = (FontButton)child;
            //    button.FontSize = fontSize;
            //}

            foreach (var child in uiSelectedFontList.Children)
            {
                var button = (FontButton)child;
                button.FontSize = fontSize;
            }
            //updateText(uiSample.Text);
        }

        private void uiSans_Click(object sender, RoutedEventArgs e)
        {

        }

        private void uiSerif_Click(object sender, RoutedEventArgs e)
        {

        }

        private void uiSymbol_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
