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
    public class ZoomTool : BaseTool
    {
        int zoomSpeed = 20;

        Point origin;
        Point old;
        Point current;
        int deltaY;
        bool firstTime = false;
        public ZoomTool() : base()
        {
            toolType = SpriteEditor.Tools.Zoom;
        }
        public override void StartJob(MouseButtonEventArgs e)
        {
            base.StartJob(e);
            if (!firstTime)
            {
                firstTime = true;
                origin = e.GetPosition(editor.ImageViewPort);
                current = origin;
                old = current;
                deltaY = 0;
            }
            else
            {
                double deltaX = current.X - origin.X;
                double deltaY = current.Y - origin.Y;
                current = e.GetPosition(editor.ImageViewPort);
                old = current;
                origin.X = current.X - deltaX;
                origin.Y = current.Y - deltaY;
            }
        }
        public override void DoJob(MouseEventArgs e)
        {
            if (!isStarted)
                return;

            current = e.GetPosition(editor.ImageViewPort);

            deltaY = (int)(current.Y - origin.Y);
            int scale = (Math.Abs(deltaY) / zoomSpeed + 1);
            if (deltaY == 0)
            {
                editor.ZoomIn(1);
            }
            else if (deltaY < 0)
            {
                if (!editor.ZoomIn(scale))
                {
                    current = old;
                    return;
                }
            }
            else
            {
                if (!editor.ZoomOut(scale))
                {
                    current = old;
                    return;
                }
            }

            old = current;
        }

        public override void EndJob(MouseButtonEventArgs e)
        {
            if (!isStarted)
                return;
            isStarted = false;
        }
    }
}
