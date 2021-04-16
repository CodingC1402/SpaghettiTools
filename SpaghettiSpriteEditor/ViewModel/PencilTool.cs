using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;

using SpaghettiSpriteEditor.View;

namespace SpaghettiSpriteEditor.ViewModel
{
    public class PencilTool : BaseTool
    {
        SpriteCut currentSprite;
        Point origin;
        Point point;
        Point anchor;
        public PencilTool() : base()
        {
            toolType = SpriteEditor.Tools.Pencil;
            point = new Point();
            anchor = new Point();
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
            origin.X = (int)(origin.X / editor.Scale + 0.5);
            origin.Y = (int)(origin.Y / editor.Scale + 0.5);
            newSprite.Position = origin;
        }
        public override void DoJob(MouseEventArgs e)
        {
            if (!isStarted)
                return;

            point = e.GetPosition(editor.ImageDisplay);
            point.X = (int)(point.X / editor.Scale + 0.5);
            point.Y = (int)(point.Y / editor.Scale + 0.5);

            int deltaX = (int)(point.X - origin.X);
            int deltaY = (int)(point.Y - origin.Y);
            anchor.X = deltaX < 0 ? point.X : origin.X;
            anchor.Y = deltaY < 0 ? point.Y : origin.Y;

            currentSprite.Position = anchor;
            currentSprite.Width = Math.Abs(deltaX);
            currentSprite.Height = Math.Abs(deltaY);
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
    }
}
