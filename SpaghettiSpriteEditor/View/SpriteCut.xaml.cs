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
using SpaghettiTools.Theme;

namespace SpaghettiSpriteEditor.View
{
    public partial class SpriteCut : UserControl
    {
        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                root.DataContext = this;
                indexText.Text = index.ToString();
            }
        }
        protected int index;

        public new double Height
        {
            get { return height; }
            set
            {
                height = (int)(value);
                base.Height = height * SpriteEditor.GetInstance().Scale;
                hText.Text = $"H:{height}";
            }
        }
        double height;

        public new double Width
        {
            get { return width; }
            set
            {
                width = (int)(value);
                base.Width = width * SpriteEditor.GetInstance().Scale;
                wText.Text = $"W:{width}";
            }
        }
        double width;

        public Point Position
        {
            get { return position; }
            set
            {
                position.X = (int)(value.X);
                position.Y = (int)(value.Y);
                corXText.Text = $"X:{position.X}";
                corYText.Text = $"Y:{position.Y}";
                double scale = SpriteEditor.GetInstance().Scale;
                Margin = new Thickness(position.X * scale, position.Y * scale, 0, 0);
            }
        }
        protected Point position;


        public SpriteCut()
        {
            InitializeComponent();
            position = new Point(0, 0);
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            Index = 0;
        }

        public void Select()
        {
            this.Foreground = (SolidColorBrush)this.FindResource(ThemeKey.ComponentSelected);
        }
        public void Unselect()
        {
            this.Foreground = (SolidColorBrush)this.FindResource(ThemeKey.Foreground);
        }

        public void UpdateToScale()
        {
            double scale = SpriteEditor.GetInstance().Scale;
            base.Width = Width * scale;
            base.Height = Height * scale;
            base.Margin = new Thickness(Position.X * scale, Position.Y * scale, 0, 0);
        }
    }
}
