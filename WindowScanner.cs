using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TextMod
{
    static class WindowScanner
    {

        // Methods
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref RECT rect);
        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        // Flags
        private const int SRCCOPY = 13369376;
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;


        // End of p/invoke region.

        static Bitmap TakeScreenshot(IntPtr processID)
        {
            IntPtr src = GetWindowDC(processID);

            RECT rct = new RECT();
            GetWindowRect(processID, ref rct);

            int w, h;
            w = rct.right - rct.left;
            h = rct.bottom - rct.top;

            IntPtr destPtr = CreateCompatibleDC(src);
            IntPtr bmPtr = CreateCompatibleBitmap(src, w, h);
            IntPtr oldW = SelectObject(destPtr, bmPtr);

            BitBlt(destPtr, 0, 0, w, h, src, 0, 0, SRCCOPY);
            SelectObject(destPtr, oldW);
            DeleteDC(destPtr);
            ReleaseDC(processID, src);

            Bitmap temp = Image.FromHbitmap(bmPtr);
            DeleteObject(bmPtr);

            return temp;
        }
        public static bool ButtonOnWindow(IntPtr handle, params Color[] buttonColors)
        {
            using(Bitmap img = TakeScreenshot(handle))
            {
                bool buttonIsOnImage = false;
                int btnClr = 0;
                unsafe
                {
                    BitmapData imageData = img.LockBits(new Rectangle(0, 0, img.Width,
                        img.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                    int bytesPerPixel = 3;

                    byte* scan0 = (byte*)imageData.Scan0.ToPointer();
                    int stride = imageData.Stride;

                    int ih = imageData.Height;
                    int iw = imageData.Width;

                    for (int y = 0; y < ih; y++)
                    {
                        double div = ((double)y) / 10;
                        int rnd = (int)Math.Floor(div);
                        y += rnd;
                        if (y >= ih) break;

                        byte* row = scan0 + (y * stride);

                        for (int x = 0; x < iw; x++)
                        {
                            int bi = x * bytesPerPixel;
                            int gi = bi + 1;
                            int ri = bi + 2;

                            byte r = row[ri];
                            byte g = row[gi];
                            byte b = row[bi];

                            bool anyMatches = false;
                            foreach(Color buttonColor in buttonColors)
                                if (r == buttonColor.R
                                && g == buttonColor.G
                                && b == buttonColor.B)
                            {
                                btnClr++;
                                anyMatches = true;
                                break;
                            }
                            if(!anyMatches)
                            {
                                if (btnClr > 0)
                                {
                                    btnClr = 0;
                                }
                            }
                            if (btnClr >= 50)
                            {
                                buttonIsOnImage = true;
                                break;
                            }
                        }
                        if (buttonIsOnImage)
                            break;
                    }

                    img.UnlockBits(imageData);
                }
                return buttonIsOnImage;
            }
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
}
