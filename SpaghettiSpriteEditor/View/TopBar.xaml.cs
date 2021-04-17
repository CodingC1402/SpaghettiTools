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

using SpaghettiSpriteEditor.ViewModel;
using SpaghettiTools.Components;

namespace SpaghettiSpriteEditor.View
{
    /// <summary>
    /// Interaction logic for TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl
    {
        public TopBar()
        {
            InitializeComponent();
            SpriteEditor.GetInstance().TopBar = this;
        }

        private void OpenMouseDown(object sender, MouseButtonEventArgs e)
        {
            SpriteEditor.GetInstance().LoadImage();
            ((ButtonBase)sender).IsMouseDown = false;
        }
        private void ImportMouseDown(object sender, MouseButtonEventArgs e)
        {
            ((ButtonBase)sender).IsMouseDown = false;
        }
        private void ExportMouseDown(object sender, MouseButtonEventArgs e)
        {
            SpriteEditor.GetInstance().Export();
            ((ButtonBase)sender).IsMouseDown = false;
        }
    }
}
