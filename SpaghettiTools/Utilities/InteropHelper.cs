using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;

namespace SpaghettiTools.Utilities
{
    public class InteropHelper
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        // http://msdn.microsoft.com/en-us/library/dd144871(VS.85).aspx
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        // http://msdn.microsoft.com/en-us/library/dd183370(VS.85).aspx
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BitBlt(IntPtr hDestDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, Int32 dwRop);

        // http://msdn.microsoft.com/en-us/library/dd183488(VS.85).aspx
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        // http://msdn.microsoft.com/en-us/library/dd183489(VS.85).aspx
        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        // http://msdn.microsoft.com/en-us/library/dd162957(VS.85).aspx
        [DllImport("gdi32.dll", ExactSpelling = true, PreserveSig = true, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        // http://msdn.microsoft.com/en-us/library/dd183539(VS.85).aspx
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        // http://msdn.microsoft.com/en-us/library/dd162920(VS.85).aspx
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr dc);

        static int SRCCOPY = 13;

        public static BitmapSource CaptureRegion(IntPtr hWnd, int x, int y, int width, int height)
        {
            IntPtr sourceDC = IntPtr.Zero;
            IntPtr targetDC = IntPtr.Zero;
            IntPtr compatibleBitmapHandle = IntPtr.Zero;
            BitmapSource bitmap = null;

            try
            {
                // gets the main desktop and all open windows
                sourceDC = InteropHelper.GetDC(InteropHelper.GetDesktopWindow());

                //sourceDC = User32.GetDC(hWnd);
                targetDC = InteropHelper.CreateCompatibleDC(sourceDC);

                // create a bitmap compatible with our target DC
                compatibleBitmapHandle = InteropHelper.CreateCompatibleBitmap(sourceDC, width, height);

                // gets the bitmap into the target device context
                InteropHelper.SelectObject(targetDC, compatibleBitmapHandle);

                // copy from source to destination
                InteropHelper.BitBlt(targetDC, 0, 0, width, height, sourceDC, x, y, InteropHelper.SRCCOPY);

                // Here's the WPF glue to make it all work. It converts from an
                // hBitmap to a BitmapSource. Love the WPF interop functions
                bitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    compatibleBitmapHandle, IntPtr.Zero, Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());

            }
            catch (Exception ex)
            {

            }
            finally
            {
                DeleteObject(compatibleBitmapHandle);
                ReleaseDC(IntPtr.Zero, sourceDC);
                ReleaseDC(IntPtr.Zero, targetDC);
            }

            return bitmap;
        }

        public static BitmapSource CaptureScreen()
        {
            return InteropHelper.CaptureRegion(InteropHelper.GetDesktopWindow(), (int)SystemParameters.VirtualScreenLeft, (int)SystemParameters.VirtualScreenTop, 
                (int)SystemParameters.VirtualScreenWidth, (int)SystemParameters.VirtualScreenHeight);
        }
    }
}
