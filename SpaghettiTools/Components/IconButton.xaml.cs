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

namespace SpaghettiTools.Components
{
    /// <summary>
    /// Interaction logic for IconButton.xaml
    /// </summary>
    public partial class IconButton : UserControl
    {
        public ImageSource IconSource
        { 
            get { return (ImageSource)GetValue(Image.SourceProperty); }
            set { SetValue(Image.SourceProperty, value); }
        }
        public string Text
        {
            get { return (string)GetValue(TextBlock.TextProperty); }
            set { SetValue(TextBlock.TextProperty, value); }
        }
        public TextAlignment TextAlignment
        {
            get { return textAlignment; }
            set 
            {
                switch (value)
                {
                    case TextAlignment.Left:
                        textBlock.HorizontalAlignment = HorizontalAlignment.Left;
                        break;
                    case TextAlignment.Right:
                        textBlock.HorizontalAlignment = HorizontalAlignment.Right;
                        break;
                    case TextAlignment.Center:
                        textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        break;
                    default:
                        return;
                }
                textAlignment = value;
            }
        }
        TextAlignment textAlignment = TextAlignment.Left;
        PropertyChangedNotifier<IconButton> notifier;
        public IconButton()
        {
            notifier = new PropertyChangedNotifier<IconButton>(this);

            InitializeComponent();
            content.DataContext = this;
        }
    }
}
