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
    public partial class ButtonBase : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
                "Text", typeof(string), typeof(ButtonBase), new PropertyMetadata(""));
        public static readonly DependencyProperty TextAlignmentProperty = DependencyProperty.Register(
                "TextAlignment", typeof(TextAlignment), typeof(ButtonBase), new PropertyMetadata(TextAlignment.Left));
        public static readonly DependencyProperty IsMouseDownProperty = DependencyProperty.Register(
        "IsMouseDown", typeof(bool), typeof(ButtonBase), new PropertyMetadata(false));

        public ButtonBase()
        {
            this.Focusable = true;
        }

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextBlock.TextProperty, value); }
        }
        public TextAlignment TextAlignment
        {
            get { return (TextAlignment)GetValue(TextAlignmentProperty); }
            set { SetValue(TextBlock.TextAlignmentProperty, value); }
        }
        public bool IsMouseDown
        {
            get { return (bool)GetValue(IsMouseDownProperty); }
            set { SetValue(IsMouseDownProperty, value); }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            this.CaptureMouse();
            IsMouseDown = true;
        }
        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            this.ReleaseMouseCapture();
            IsMouseDown = false;
        }
    }
}
