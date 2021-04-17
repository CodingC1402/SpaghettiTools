using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

using SpaghettiSpriteEditor.View;

namespace SpaghettiSpriteEditor.ViewModel
{
    public class PencilTool : BaseTool
    {
        Rectangle highLightPixel;
        SpriteCut currentSprite;
        Point origin;
        Point point;

        Point topLeft;
        Point bottomRight;

        public PencilTool() : base()
        {
            highLightPixel = new Rectangle();
            highLightPixel.Fill = new SolidColorBrush(Color.FromArgb(127, 0, 0, 0));
            highLightPixel.Stroke = new SolidColorBrush(Color.FromArgb(127, 255, 255, 255));
            highLightPixel.StrokeThickness = 1;
            highLightPixel.Width = 1;
            highLightPixel.Height = 1;
            highLightPixel.VerticalAlignment = VerticalAlignment.Top;
            highLightPixel.HorizontalAlignment = HorizontalAlignment.Left;

            toolType = SpriteEditor.Tools.Pencil;
            point = new Point();
            topLeft = new Point();
            bottomRight = new Point();
        }
        public override void Select()
        {
            base.Select();
            editor.Content.Children.Add(highLightPixel);
        }
        public override void StartJob(MouseButtonEventArgs e)
        {
            base.StartJob(e);

            origin = e.GetPosition(editor.ImageDisplay);
            SpriteCut newSprite = new SpriteCut();
            editor.SpriteCollection.Children.Add(newSprite);
            newSprite.Index = editor.SpriteCollection.Children.IndexOf(newSprite);
            currentSprite = newSprite;
            newSprite.Width = 0;
            newSprite.Height = 0;
            origin.X = origin.X / editor.Scale;
            origin.Y = origin.Y / editor.Scale;
            newSprite.Position = origin;
        }
        public override void DoJob(MouseEventArgs e)
        {
            point = e.GetPosition(editor.ImageDisplay);
            point.X = point.X / editor.Scale;
            point.Y = point.Y / editor.Scale;

            if (isStarted)
            {
                double deltaX = point.X - origin.X;
                double deltaY = point.Y - origin.Y;
                if (deltaX < 0)
                {
                    topLeft.X = point.X;
                    bottomRight.X = origin.X;
                }
                else
                {
                    topLeft.X = origin.X;
                    bottomRight.X = point.X;
                    point.X -= 0.001;
                }

                if (deltaY < 0)
                {
                    topLeft.Y = point.Y;
                    bottomRight.Y = origin.Y;
                }
                else
                {
                    topLeft.Y = origin.Y;
                    bottomRight.Y = point.Y;
                    point.Y -= 0.001;
                }

                bottomRight.X = (int)(bottomRight.X + 0.9999);
                bottomRight.Y = (int)(bottomRight.Y + 0.9999);
                topLeft.X = (int)topLeft.X;
                topLeft.Y = (int)topLeft.Y;

                deltaX = bottomRight.X - topLeft.X;
                deltaY = bottomRight.Y - topLeft.Y;

                using (var d = currentSprite.Dispatcher.DisableProcessing())
                {
                    currentSprite.Position = topLeft;
                    currentSprite.Width = Math.Abs(deltaX);
                    currentSprite.Height = Math.Abs(deltaY);
                }
            }

            highLightPixel.Margin = new Thickness((int)(point.X) * editor.Scale, (int)(point.Y) * editor.Scale, 0, 0);
            highLightPixel.Width = editor.Scale;
            highLightPixel.Height = editor.Scale;
        }

        public override void EndJob(MouseButtonEventArgs e)
        {
            if (!isStarted)
                return;

            base.EndJob(e);
            if (currentSprite.Width == 0 || currentSprite.Height == 0)
            {
                editor.SpriteCollection.Children.Remove(currentSprite);
            }
            currentSprite = null;
        }
        public override void Unselect()
        {
            base.Unselect();
            editor.Content.Children.Remove(highLightPixel);
        }
    }
}
