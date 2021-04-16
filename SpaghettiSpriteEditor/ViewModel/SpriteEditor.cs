using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Input;
using System.Threading.Tasks;

namespace SpaghettiSpriteEditor.ViewModel
{
    public class SpriteEditor
    {
        #region Property and member variable;
        static SpriteEditor __instance = null;
        public static SpriteEditor GetInstance()
        {
            if (__instance == null)
                __instance = new SpriteEditor();
            return __instance;
        }

        public enum Tools
        {
            Pencil,
            Eraser,
            Move
        }
        public Tools SelectedTool
        {
            get { return selectedTool; }
            set 
            { 
                selectedTool = value;
                ChangeCursorImage();
            }
        }
        protected Tools selectedTool;

        protected OpenFileDialog opfDialog;
        public Image ImageDisplay
        { 
            get { return imageDisplay; }
            set { imageDisplay = value; }
        }
        protected Image imageDisplay;

        public BitmapImage CurrentTexture
        {
            get { return currentTexture; }
        }
        protected BitmapImage currentTexture;

        public Image CursorImage
        {
            get { return cursorImage; }
            set 
            { 
                cursorImage = value;
                ChangeCursorImage();
            }
        }
        protected Image cursorImage;

        protected int Scale = 1;
        protected BaseTool tool;
        #endregion

        SpriteEditor()
        {
            opfDialog = new OpenFileDialog();
            opfDialog.Title  =  "Select a texture";
            opfDialog.Filter =  "All supported graphics|*.jpg;*.jpeg;*.png|" +
                                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                "Portable Network Graphic (*.png)|*.png";
            selectedTool = Tools.Pencil;
            tool = new PencilTool();
        }

        #region Job
        public void StartJob(MouseButtonEventArgs e)
        {
            tool.StartJob(e);
        }
        public void DoJob(MouseEventArgs e)
        {
            tool.DoJob(e);
        }
        public void EndJob(MouseButtonEventArgs e)
        {
            tool.EndJob(e);
        }
        #endregion

        #region Zoom
        public void ZoomIn()
        {

        }
        public void ZoomOut()
        {

        }
        #endregion

        protected void ChangeCursorImage()
        {
            switch(selectedTool)
            {
                case Tools.Pencil:
                    CursorImage.Source = new BitmapImage(new Uri("pack://application:,,,/SpaghettiSpriteEditor;component/Resource/Cursor/pencil.png"));
                    break;
                case Tools.Eraser:
                    CursorImage.Source = new BitmapImage(new Uri("pack://application:,,,/SpaghettiSpriteEditor;component/Resource/Cursor/eraser.png"));
                    break;
                case Tools.Move:
                    CursorImage.Source = new BitmapImage(new Uri("pack://application:,,,/SpaghettiSpriteEditor;component/Resource/Cursor/move.png"));
                    break;
            }
        }
        public bool LoadImage()
        {
            if(opfDialog.ShowDialog() == true)
            {
                currentTexture = new BitmapImage(new Uri(opfDialog.FileName));
                ImageDisplay.Source = CurrentTexture;
                ImageDisplay.Width = CurrentTexture.Width;
                ImageDisplay.Height = CurrentTexture.Height;
                return true;
            }

            return false;
        }
    }
}
