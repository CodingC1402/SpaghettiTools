using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

using SpaghettiTools.Theme;

namespace SpaghettiSpriteEditor.ViewModel
{
    public class ToolBarViewModel
    {
        static ToolBarViewModel __instance;
        public static ToolBarViewModel GetInstance()
        {
            if (__instance == null)
                __instance = new ToolBarViewModel();
            return __instance;
        }

        public SpaghettiSpriteEditor.View.ToolBar UI { get; set; }
        Dictionary<object, SpriteEditor.Tools> buttons;
        Border selectedToolButton;
        ToolBarViewModel ()
        {
            buttons = new Dictionary<object, SpriteEditor.Tools>();
        }
        public void AddButton(object button, SpriteEditor.Tools toolType)
        {
            buttons.Add(button, toolType);
        }
        public void SelectTool(object sender)
        {
            if (sender == selectedToolButton)
                return;

            SpriteEditor.GetInstance().SelectedToolType = buttons[sender];
            UpdateUI(sender);
        }
        public void UpdateUI(object selectedButton)
        {
            if (selectedToolButton != null)
            {
                selectedToolButton.Background = (SolidColorBrush)UI.FindResource(ThemeKey.Component);
            }

            selectedToolButton = (Border)selectedButton;
            selectedToolButton.Background = (Brush)UI.FindResource(ThemeKey.ComponentSelected);
        }
        public void LoadImage()
        {
            SpriteEditor.GetInstance().LoadImage();
        }
    }
}
