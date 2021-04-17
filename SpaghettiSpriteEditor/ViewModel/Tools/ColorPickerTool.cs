using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace SpaghettiSpriteEditor.ViewModel
{
    public class ColorPickerTool : BaseTool
    {
        protected ToolBarViewModel toolBar;
        protected Point point;

        public ColorPickerTool()
        {
            toolBar = ToolBarViewModel.GetInstance();
        }

        public override void StartJob(MouseButtonEventArgs e)
        {
            base.StartJob(e);

            point = e.GetPosition(editor.ImageDisplay);
            Job();
        }
        public override void DoJob(MouseEventArgs e)
        {
            if (!isStarted)
                return;

            point = e.GetPosition(editor.ImageDisplay);
            Job();
        }
        public override void EndJob(MouseButtonEventArgs e)
        {
            base.EndJob(e);
        }

        public void Job()
        {
            point.X = point.X / editor.Scale;
            point.Y = point.Y / editor.Scale;

            editor.PickKeyColorAtCord(point);
            toolBar.ChangeColorPickerColor();
        }
    }
}
