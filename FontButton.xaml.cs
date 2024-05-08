using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace Font_Chooser
{
    /// <summary>
    /// Interaction logic for FontButton.xaml
    /// </summary>
    public partial class FontButton : Button
    {
        public System.Windows.Media.Brush highlightedButtonBackground = Brushes.Gainsboro;
        public System.Windows.Media.Brush selectedButtonBackground = Brushes.Gainsboro;
        public System.Windows.Media.Brush defaultButtonBackground = Brushes.White;
        public bool IsSelected = false;

        public event SelectionUpdatedEventHandler OnSelectionUpdated;
        protected virtual void SelectionUpdated(EventArgs e)
        {
            SelectionUpdatedEventHandler handler = OnSelectionUpdated;            
            handler?.Invoke(this, e);
        }

        public delegate void SelectionUpdatedEventHandler(object sender, EventArgs e);

        public FontButton()
        {
            InitializeComponent();
            MouseEnter += FontButton_MouseEnter;
            MouseLeave += FontButton_MouseLeave;
            Click += FontButton_Click;
        }

        public void DeSelect()
        {
            Select(false);
        }

        public void Select()
        {
            Select(true);
        }

        private void Select(bool select)
        {
            if (select)
            {
                Background = selectedButtonBackground;
                IsSelected = true;
            }
            else
            {
                Background = defaultButtonBackground;
                IsSelected = false;
            }
        }
        private void FontButton_Click(object sender, RoutedEventArgs e)
        {
            IsSelected = !IsSelected;
            Background = IsSelected ? selectedButtonBackground : defaultButtonBackground;
        }

        private void FontButton_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = IsSelected ? selectedButtonBackground : defaultButtonBackground;
        }

        private void FontButton_MouseEnter(object sender, MouseEventArgs e)
        {
            Background = highlightedButtonBackground;
        }
    }
}
