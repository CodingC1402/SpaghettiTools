using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

using SpaghettiSpriteEditor.View;
namespace SpaghettiSpriteEditor.ViewModel
{
    public class EraserTool : BaseTool
    {
        public override void StartJob(MouseButtonEventArgs e)
        {
            base.StartJob(e);
            EraseIndexAndUpdate(editor.GetMouseOverIndex(e.GetPosition(editor.SpriteCollection)));
        }
        public override void DoJob(MouseEventArgs e)
        {
            if (!isStarted)
                return;

            EraseIndexAndUpdate(editor.GetMouseOverIndex(e.GetPosition(editor.SpriteCollection)));
        }

        public override void EndJob(MouseButtonEventArgs e)
        {
            base.EndJob(e);
        }

        protected void EraseIndexAndUpdate(int index)
        {
            if (index < 0)
                return;

            editor.SpriteCollection.Children.RemoveAt(index);
            for (int i = 0; i < editor.SpriteCollection.Children.Count; i++)
            {
                ((SpriteCut)editor.SpriteCollection.Children[i]).Index = i;
            }
        }
    }
}
