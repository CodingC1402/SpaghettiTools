using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

using SpaghettiSpriteEditor.View;

namespace SpaghettiSpriteEditor.ViewModel
{
    public class EditTool : BaseTool
    {
        SpriteCut editingSprite = null;
        int editingIndex = -1;
        ToolBarViewModel toolBar;

        Point oldPoint;
        Point point;
        Point newPosition;

        public EditTool()
        {
            toolBar = ToolBarViewModel.GetInstance();
            oldPoint = new Point();
            point = new Point();
            newPosition = new Point();
        }

        public override void StartJob(MouseButtonEventArgs e)
        {
            base.StartJob(e);

            int index = editor.GetMouseOverIndex(e.GetPosition(editor.SpriteCollection));
            if (index >= 0 )
            {
                if (index != editingIndex)
                {
                    if (editingSprite != null)
                        editingSprite.Unselect();

                    editingIndex = index;
                    editingSprite = (SpriteCut)editor.SpriteCollection.Children[editingIndex];
                    editingSprite.Select();

                    toolBar.EditButton.DataContext = editingSprite;
                }

                oldPoint = e.GetPosition(editor.SpriteCollection);
                oldPoint.X = (int)(oldPoint.X / editor.Scale);
                oldPoint.Y = (int)(oldPoint.Y / editor.Scale);
            }
            else
            {
                isStarted = false;
            }
        }
        public override void DoJob(MouseEventArgs e)
        {
            if (!isStarted)
                return;

            point = e.GetPosition(editor.SpriteCollection);
            point.X = (int)(point.X / editor.Scale);
            point.Y = (int)(point.Y / editor.Scale);

            if (oldPoint != point)
            {
                newPosition.X = (int)(editingSprite.X + (point.X - oldPoint.X));
                newPosition.Y = (int)(editingSprite.Y + (point.Y - oldPoint.Y));
                editingSprite.Position = newPosition;

                oldPoint = point;
            }
        }
        public override void EndJob(MouseButtonEventArgs e)
        {
            base.EndJob(e);
            
            if (editingSprite != null)
            {
                using (var d = toolBar.EditButton.Dispatcher.DisableProcessing())
                {
                    toolBar.EditButton.xText.Text = editingSprite.X.ToString();
                    toolBar.EditButton.yText.Text = editingSprite.Y.ToString();
                }
            }
        }
        public override void Unselect()
        {
            base.Unselect();
            if (editingSprite != null)
            {
                editingSprite.Unselect();
            }
            editingSprite = null;
            editingIndex = -1;
            toolBar.EditButton.DataContext = null;
        }
    }
}
