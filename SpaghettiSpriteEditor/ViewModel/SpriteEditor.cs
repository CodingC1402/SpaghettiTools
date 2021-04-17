using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media;
using System.Threading.Tasks;
using SpaghettiSpriteEditor.View;

using SpaghettiTools.Utilities;

namespace SpaghettiSpriteEditor.ViewModel
{
    public class SpriteEditor
    {
        #region Property and member variable;
        static SpriteEditor __instance = null;
        public static SpriteEditor GetInstance()
        {
            if (__instance == null)
            {
                __instance = new SpriteEditor();
                __instance.Init();
            }
            return __instance;
        }

        public enum Tools
        {
            Pencil,
            Eraser,
            Edit,
            Zoom,
            ColorPicker,
            Invalid
        }
        public Tools SelectedToolType
        {
            get { return selectedToolType; }
            set 
            { 
                selectedToolType = value;
                ChangeTool();
                ChangeCursorImage();
            }
        }
        protected Tools selectedToolType;

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

        public ScrollViewer ImageViewPort
        {
            get { return imageViewPort; }
            set
            {
                imageViewPort = value;
            }
        }
        protected ScrollViewer imageViewPort;

        public Canvas CursorContainer
        {
            get { return _cursorContainer; }
            set { _cursorContainer = value; }
        }
        public Canvas _cursorContainer;

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

        public Canvas SpriteCollection
        {
            get { return spriteCollection; }
            set { spriteCollection = value; }
        }
        protected Canvas spriteCollection;

        public Grid Content
        {
            get { return _content; }
            set { _content = value; }
        }
        protected Grid _content;

        public double Scale
        {
            get { return scale; }
        }
        protected double scale = 1;

        public Color KeyColor
        {
            get { return _keyColor; }
        }
        protected Color _keyColor = Color.FromArgb(255, 255, 255, 255);


        protected Dictionary<Tools, BaseTool> tools;
        protected BaseTool currentTool;
        protected int originalWidth;
        protected int originalHeight;
        protected Bitmap imageBitmap; // It's fuckingly stupid : ^) but I'm stupid too soo...
        #endregion

        SpriteEditor()
        {
            currentTexture = null;
            tools = new Dictionary<Tools, BaseTool>();
            opfDialog = new OpenFileDialog();
            opfDialog.Title  =  "Select a texture";
            opfDialog.Filter =  "All supported graphics|*.jpg;*.jpeg;*.png|" +
                                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                "Portable Network Graphic (*.png)|*.png";
        }
        protected void Init()
        {
            selectedToolType = Tools.Zoom;
            tools.Add(Tools.Pencil, new PencilTool());
            tools.Add(Tools.Zoom, new ZoomTool());
            tools.Add(Tools.Eraser, new EraserTool());
            tools.Add(Tools.Edit, new EditTool());
            tools.Add(Tools.ColorPicker, new ColorPickerTool());
            ChangeTool();
        }
        #region Job
        public void StartJob(MouseButtonEventArgs e)
        {
            if (currentTexture == null)
                return;
            currentTool.StartJob(e);
        }
        public void DoJob(MouseEventArgs e)
        {
            if (currentTexture == null)
                return;
            currentTool.DoJob(e);
        }
        public void EndJob(MouseButtonEventArgs e)
        {
            if (currentTexture == null)
                return;
            currentTool.EndJob(e);
        }
        #endregion

        #region Zoom
        public bool ZoomIn(int setScale)
        {
            if (setScale == scale)
                return true;

            if (setScale < 0.01 || setScale > 20)
                return false;

            scale = setScale;
            UpdateToScale();
            return true;
        }
        public bool ZoomOut(int setScale)
        {
            if (setScale == scale)
                return true;

            if (setScale < 0.01 || setScale > 20)
                return false;

            scale = 1 / (double)setScale;
            UpdateToScale();
            return true;
        }
        protected void UpdateToScale()
        {
            double offSetX = imageViewPort.VerticalOffset / imageDisplay.Width;
            double offSetY = imageViewPort.HorizontalOffset / imageDisplay.Height;

            imageDisplay.Width = (int)(originalWidth * scale + 0.5);
            imageDisplay.Height = (int)(originalHeight * scale + 0.5);
            foreach (SpriteCut cut in spriteCollection.Children)
            {
                cut.UpdateToScale();
            }

            imageViewPort.ScrollToVerticalOffset((int)(imageDisplay.Width * offSetX + 0.5));
            imageViewPort.ScrollToHorizontalOffset((int)(imageDisplay.Height * offSetY + 0.5));
        }
        #endregion

        #region Tools
        protected void ChangeTool()
        {
            if (currentTool != null)
                currentTool.Unselect();
            switch (selectedToolType)
            {
                case Tools.Pencil:
                    currentTool = tools[Tools.Pencil];
                    break;
                case Tools.Eraser:
                    currentTool = tools[Tools.Eraser];
                    break;
                case Tools.Edit:
                    currentTool = tools[Tools.Edit];
                    break;
                case Tools.Zoom:
                    currentTool = tools[Tools.Zoom];
                    break;
                case Tools.ColorPicker:
                    currentTool = tools[Tools.ColorPicker];
                    break;
            }
            currentTool.Select();
        }
        protected void ChangeCursorImage()
        {
            switch(selectedToolType)
            {
                case Tools.Pencil:
                    CursorImage.Source = new BitmapImage(new Uri("pack://application:,,,/SpaghettiSpriteEditor;component/Resource/Cursor/pencil.png"));
                    break;
                case Tools.Eraser:
                    CursorImage.Source = new BitmapImage(new Uri("pack://application:,,,/SpaghettiSpriteEditor;component/Resource/Cursor/eraser.png"));
                    break;
                case Tools.Edit:
                    CursorImage.Source = new BitmapImage(new Uri("pack://application:,,,/SpaghettiSpriteEditor;component/Resource/Cursor/edit.png"));
                    break;
                case Tools.Zoom:
                    CursorImage.Source = new BitmapImage(new Uri("pack://application:,,,/SpaghettiSpriteEditor;component/Resource/Cursor/zoom.png"));
                    break;
                case Tools.ColorPicker:
                    CursorImage.Source = new BitmapImage(new Uri("pack://application:,,,/SpaghettiSpriteEditor;component/Resource/Cursor/colorPicker.png"));
                    break;
            }
        }
        #endregion

        public void PickKeyColorAtCord(Point position)
        {
            _keyColor = imageBitmap.GetPixel((int)position.X, (int)position.Y);
        }

        public bool LoadImage()
        {
            if(opfDialog.ShowDialog() == true)
            {
                spriteCollection.Children.Clear();
                ((ZoomTool)tools[Tools.Zoom]).Reset();
                scale = 1;
                _keyColor = Color.FromRgb(255, 255, 255);
                ToolBarViewModel.GetInstance().ChangeColorPickerColor();

                currentTexture = new BitmapImage(new Uri(opfDialog.FileName));
                ImageDisplay.Source = CurrentTexture;
                ImageDisplay.Width = (int)(CurrentTexture.PixelWidth);
                ImageDisplay.Height = (int)(CurrentTexture.PixelHeight);
                originalWidth = (int)ImageDisplay.Width;
                originalHeight = (int)ImageDisplay.Height;

                imageBitmap = new Bitmap(new Uri(opfDialog.FileName));
                return true;
            }

            return false;
        }

        public int GetMouseOverIndex(Point position)
        {
            position.X = (int)(position.X / Scale);
            position.Y = (int)(position.Y / Scale);
            int index = -1;
            int size = SpriteCollection.Children.Count;
            SpriteCut child;
            do
            {
                index++;
                if (index >= size)
                {
                    return -1;
                }
                child = (SpriteCut)SpriteCollection.Children[index];
                if (child.Position.X <= position.X && (child.Position.X + child.Width >= position.X) &&
                    child.Position.Y <= position.Y && (child.Position.Y + child.Height >= position.Y))
                {
                    break;
                }
            }
            while (true);

            return index;
        }
    }
}
