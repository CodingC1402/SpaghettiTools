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
using SpaghettiTools.Theme;
using SpaghettiTools.Components;

namespace SpaghettiSpriteEditor.View
{
    public partial class ToolBar : UserControl
    {
        protected ToolBarViewModel viewModel;

        public ToolBar()
        {
            InitializeComponent();
            ToolBarViewModel.GetInstance().UI = this;
            Border selectedToolButton = null;

            viewModel = ToolBarViewModel.GetInstance();
            viewModel.AddButton(eraserButton, SpriteEditor.Tools.Eraser);
            viewModel.AddButton(pencilButton, SpriteEditor.Tools.Pencil);
            viewModel.AddButton(editButton, SpriteEditor.Tools.Edit);
            viewModel.AddButton(zoomButton, SpriteEditor.Tools.Zoom);
            viewModel.AddButton(colorPickerButton, SpriteEditor.Tools.ColorPicker);
            viewModel.EditButton = (EditButton)editButton.Child;

            switch (SpriteEditor.GetInstance().SelectedToolType)
            {
                case SpriteEditor.Tools.Eraser:
                    selectedToolButton = eraserButton;
                    break;
                case SpriteEditor.Tools.Pencil:
                    selectedToolButton = pencilButton;
                    break;
                case SpriteEditor.Tools.Edit:
                    selectedToolButton = editButton;
                    break;
                case SpriteEditor.Tools.Zoom:
                    selectedToolButton = zoomButton;
                    break;
            }
            viewModel.SelectTool(selectedToolButton);
        }

        private void SelectTool(object sender, MouseButtonEventArgs e)
        {
            viewModel.SelectTool(sender);
        }
    }
}
