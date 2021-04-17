using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

/// <summary>
/// Cause wpf is stupid and so am I;
/// "Reference": http://csharphelper.com/blog/2018/05/easily-manipulate-pixels-in-wpf-and-c/
/// </summary>

namespace SpaghettiTools.Utilities
{
    public class Bitmap
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] Pixels { get; set; }
        public int Stride { get; set; }

        public Bitmap(Uri uri)
        {
            // Load the file into a WriteableBitmap.
            BitmapImage bitmap = new BitmapImage(uri);
            WriteableBitmap wbitmap =
                new WriteableBitmap(bitmap);

            // Initialize the object.
            InitializeFromWbitmap(wbitmap);
        }

        public Bitmap(int width, int height)
        {
            InitializeFromDimensions(width, height);
        }

        public Bitmap(WriteableBitmap wbitmap)
        {
            InitializeFromWbitmap(wbitmap);
        }

        // Initialize from a WriteableBitmap.
        private void InitializeFromWbitmap(WriteableBitmap wbitmap)
        {
            // Initialize the basics.
            // Use Convert.ToInt32 to round the dimensions to integers.
            int wid = Convert.ToInt32(wbitmap.Width);
            int hgt = Convert.ToInt32(wbitmap.Height);
            InitializeFromDimensions(wid, hgt);

            // Get the pixels.
            Int32Rect rect = new Int32Rect(0, 0, Width, Height);
            wbitmap.CopyPixels(rect, Pixels, Stride, 0);
        }

        private void InitializeFromDimensions(int width, int height)
        {
            // Save the width and height.
            Width = width;
            Height = height;

            // Create the pixel array.
            Pixels = new byte[width * height * 4];

            // Calculate the stride.
            Stride = width * 4;
        }

        public Color GetPixel(int x, int y)
        {
            Color rColor = new Color();
            int index = y * Stride + x * 4;
            rColor.B = Pixels[index++];
            rColor.G = Pixels[index++];
            rColor.R = Pixels[index++];
            rColor.A = Pixels[index];
            return rColor;
        }

        public void SetPixel(int x, int y, byte red, byte green, byte blue, byte alpha)
        {
            int index = y * Stride + x * 4;
            Pixels[index++] = blue;
            Pixels[index++] = green;
            Pixels[index++] = red;
            Pixels[index++] = alpha;
        }

        public void SetColor(byte red, byte green, byte blue, byte alpha)
        {
            int num_bytes = Width * Height * 4;
            int index = 0;
            while (index < num_bytes)
            {
                Pixels[index++] = blue;
                Pixels[index++] = green;
                Pixels[index++] = red;
                Pixels[index++] = alpha;
            }
        }

        public void SetColor(byte red, byte green, byte blue)
        {
            SetColor(red, green, blue, 255);
        }

        public WriteableBitmap MakeBitmap(double dpiX, double dpiY)
        {
            // Create the WriteableBitmap.
            WriteableBitmap wbitmap = new WriteableBitmap(
                Width, Height, dpiX, dpiY,
                PixelFormats.Bgra32, null);

            // Load the pixel data.
            Int32Rect rect = new Int32Rect(0, 0, Width, Height);
            wbitmap.WritePixels(rect, Pixels, Stride, 0);

            // Return the bitmap.
            return wbitmap;
        }
    }
}
