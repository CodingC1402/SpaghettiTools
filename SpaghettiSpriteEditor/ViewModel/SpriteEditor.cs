﻿using System;
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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

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

        public TopBar TopBar { get; set; }

        protected string imageName;
        protected OpenFileDialog opfDialog;
        protected SaveFileDialog sfDialog;
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
            sfDialog = new SaveFileDialog();
            sfDialog.Filter = "Json|*.json";
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
        public void StartJob(MouseButtonEventArgs e) // Mouse down
        {
            if (currentTexture == null)
                return;
            currentTool.StartJob(e);
        }
        public void DoJob(MouseEventArgs e) // Mouse move
        {
            if (currentTexture == null)
                return;

            Point position = e.GetPosition(ImageDisplay);
            position.X = (int)(position.X / Scale);
            position.Y = (int)(position.Y / Scale);

            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > ImageDisplay.Width)
                position.X = ImageDisplay.Width;
            if (position.Y > ImageDisplay.Height)
                position.Y = ImageDisplay.Height;

            TopBar.xText.Text = position.X.ToString();
            TopBar.yText.Text = position.Y.ToString();

            currentTool.DoJob(e);
        }
        public void EndJob(MouseButtonEventArgs e) // Mouse up
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

        #region Load and export
        public bool Import()
        {
            return true;
        }

        public bool Export()
        {
            if (CurrentTexture == null)
                return false;

            sfDialog.FileName = $"{imageName}.json";
            if (sfDialog.ShowDialog() == true)
            {
                List<int[]> sprites = new List<int[]>();
                foreach (SpriteCut cut in SpriteCollection.Children)
                {
                    sprites.Add(new int[4] { (int)cut.X, (int)cut.Y, (int)cut.Width, (int)cut.Height });
                }

                var data = new
                {
                    KeyColor = new
                    {
                        Red = _keyColor.R,
                        Green = _keyColor.G,
                        Blue = _keyColor.B
                    },
                    Sprites = sprites
                };

                var jsonString = JsonSerializer.Serialize(data);
                if (sfDialog.OverwritePrompt)
                {
                    try
                    {
                        File.Delete(sfDialog.FileName);
                    }
                    catch (System.IO.IOException e)
                    {
                        MessageBox.Show(e.Message);
                        return false;
                    }
                }
                File.WriteAllText(sfDialog.FileName, jsonString);

                return true;
            }

            return false;
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

                imageName = Path.GetFileName(opfDialog.FileName);
                return true;
            }

            return false;
        }
        #endregion

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
