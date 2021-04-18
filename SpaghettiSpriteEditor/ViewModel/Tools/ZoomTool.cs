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
        public ZoomTool() : base()
        {
            toolType = SpriteEditor.Tools.Zoom;
        }
        public override void StartJob(MouseButtonEventArgs e)
        {
            if (isStarted)
                return;
            base.StartJob(e);
            if (e.LeftButton == MouseButtonState.Pressed)
                editor.ZoomIn();
            if (e.RightButton == MouseButtonState.Pressed)
                editor.ZoomOut();
        }
        public override void DoJob(MouseEventArgs e)
        {}

        public override void EndJob(MouseButtonEventArgs e)
        {
            base.EndJob(e);
            if (!isStarted)
                return;
        }
    }
}
