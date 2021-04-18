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

namespace SpaghettiTools.Components
{
    /// <summary>
    /// Interaction logic for ProcessingWnd.xaml
    /// </summary>
    public partial class ProcessingWnd : Window
    {
        private static readonly DependencyProperty TextProperty = DependencyProperty.Register(
            "Text", typeof(string), typeof(ProcessingWnd), new PropertyMetadata("Doing stuff"));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public ProcessingWnd()
        {
            InitializeComponent();
            content.DataContext = this;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            DragMove();
        }
    }
}
