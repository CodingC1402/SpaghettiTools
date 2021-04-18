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
using System.Windows.Shapes;

namespace SpaghettiSpriteEditor.View
{
    /// <summary>
    /// Interaction logic for AutoSliceWindow.xaml
    /// </summary>
    public partial class AutoSliceWindow : Window
    {
        public AutoSliceWindow()
        {
            InitializeComponent();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "")
                textBox.Text = "0";
        }

        private void Accept(object sender, MouseButtonEventArgs e)
        {

        }

        private void Cancel(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
