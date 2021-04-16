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
                height = value;
                base.Height = value * SpriteEditor.GetInstance().Scale;
                hText.Text = $"H:{value}";
            }
        }
        double height;

        public new double Width
        {
            get { return width; }
            set
            {
                width = value;
                base.Width = value * SpriteEditor.GetInstance().Scale;
                wText.Text = $"W:{value}";
            }
        }
        double width;

        public Point Position
        {
            get { return position; }
            set
            {
                position = value;
                corXText.Text = $"X:{position.X}";
                corYText.Text = $"Y:{position.Y}";
                int scale = SpriteEditor.GetInstance().Scale;
                Margin = new Thickness(position.X * scale, position.Y * scale, 0, 0);
            }
        }
        protected Point position;

        public SpriteCut()
        {
            InitializeComponent();
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Left;
            Index = 0;
        }

        public void UpdateToScale()
        {
            int scale = SpriteEditor.GetInstance().Scale;
            base.Width = Width * scale;
            base.Height = Height * scale;
            base.Margin = new Thickness(Position.X * scale, Position.Y * scale, 0, 0);
        }
    }
}
