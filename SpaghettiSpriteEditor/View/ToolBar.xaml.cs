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
using SpaghettiSpriteEditor.Model;
using SpaghettiTools.Components;

namespace SpaghettiSpriteEditor.View
{
    public partial class ToolBar : UserControl
    {
        Border selectedToolButton;

        public ToolBar()
        {
            InitializeComponent();

            switch (SpriteEditor.GetInstance().SelectedTool)
            {
                case SpriteEditor.Tools.Eraser:
                    selectedToolButton = eraserButton;
                    break;
                case SpriteEditor.Tools.Pencil:
                    selectedToolButton = pencilButton;
                    break;
                case SpriteEditor.Tools.Move:
                    selectedToolButton = moveButton;
                    break;
            }
            selectedToolButton.Background = (Brush)this.FindResource(ThemeKey.ComponentSelected);
        }

        private void ImportImage(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                SpriteEditor.GetInstance().LoadImage();
                ((FlatButton)sender).IsMouseDown = false;
            }
        }

        private void SelectTool(object sender, MouseButtonEventArgs e)
        {
            SpriteEditor.Tools tool = SpriteEditor.Tools.Invalid;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (sender == pencilButton)
                {
                    tool = SpriteEditor.Tools.Pencil;
                }
                else if (sender == eraserButton)
                {
                    tool = SpriteEditor.Tools.Eraser;
                }
                else if (sender == moveButton)
                {
                    tool = SpriteEditor.Tools.Move;
                }
                else if (sender == zoomButton)
                {
                    tool = SpriteEditor.Tools.Zoom;
                }
                SpriteEditor.GetInstance().SelectedTool = tool;
                selectedToolButton.Background = (Brush)this.FindResource(ThemeKey.Component);
                selectedToolButton = (Border)sender;
                selectedToolButton.Background = (Brush)this.FindResource(ThemeKey.ComponentSelected);
            }
        }
    }
}
