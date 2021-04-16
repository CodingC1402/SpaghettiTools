using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;

namespace SpaghettiSpriteEditor.ViewModel
{
    public class PencilTool : BaseTool
    {
        public override void StartJob(MouseButtonEventArgs e)
        {
            Point pos = e.GetPosition(editor.SpriteCollection);
            Rectangle newSprite = new Rectangle();
            newSprite.Width = 50;
            newSprite.Height = 50;
            newSprite.Margin = new Thickness(pos.X, pos.Y, 0, 0);
        }
        public override void DoJob(MouseEventArgs e)
        {
            //throw new NotImplementedException();
        }
        public override void EndJob(MouseButtonEventArgs e)
        {
            //throw new NotImplementedException();
        }
    }
}
