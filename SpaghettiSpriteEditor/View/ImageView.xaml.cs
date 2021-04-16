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
    /// <summary>
    /// Interaction logic for ImageView.xaml
    /// </summary>
    public partial class ImageView : UserControl
    {
        public int CursorWidth
        {
            get { return cursorWidth; }
            set { cursorWidth = value; }
        }
        protected int cursorWidth = 30;

        public int CursorHeight
        {
            get { return cursorHeight; }
            set { cursorHeight = value; }
        }
        protected int cursorHeight = 30;

        SpriteEditor editor;

        public ImageView()
        {
            InitializeComponent();
            editor = SpriteEditor.GetInstance();

            cursorCanvas.MouseDown += CanvasMouseDown;
            cursorCanvas.MouseUp += CanvasMouseUp;
            cursorCanvas.MouseMove += CanvasMousMove;
            imageViewPort.ScrollChanged += ScrollToWholeNumer;

            content.DataContext = this;
            editor.ImageDisplay = image;
            editor.CursorImage = customCursor;
            editor.SpriteCollection = spritesCollection;
            editor.ImageViewPort = imageViewPort;
            editor.CursorContainer = cursorCanvas;
        }
        private void ScrollToWholeNumer(object sender, ScrollChangedEventArgs e)
        {
            if (e.HorizontalOffset != (int)e.HorizontalOffset)
            {
                imageViewPort.ScrollToHorizontalOffset((int)(e.HorizontalOffset + 0.5));
            }
            if (e.VerticalOffset != (int)e.VerticalOffset)
            {
                imageViewPort.ScrollToVerticalOffset((int)(e.VerticalOffset + 0.5));
            }
        }
        private void CustomCusorMouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.GetPosition(cursorCanvas);
            double pX = p.X - CursorWidth / 2;
            double pY = p.Y - CursorHeight / 2;

            // Set the coordinates of customPointer to the mouse pointer coordinates
            Canvas.SetTop(customCursor, pY);
            Canvas.SetLeft(customCursor, pX);
        }

        private void cursorCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            customCursor.Visibility = Visibility.Hidden;
        }

        private void cursorCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            customCursor.Visibility = Visibility.Visible;
        }

        private void CanvasMouseDown(object sender, MouseButtonEventArgs e)
        {
            cursorCanvas.CaptureMouse();
            editor.StartJob(e);
        }
        private void CanvasMousMove(object sender, MouseEventArgs e)
        {
            editor.DoJob(e);
        }
        private void CanvasMouseUp(object sender, MouseButtonEventArgs e)
        {
            editor.EndJob(e);
            cursorCanvas.ReleaseMouseCapture();
        }
    }
}
