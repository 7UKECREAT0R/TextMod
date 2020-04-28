using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Windows.Media.Imaging;
using System.Diagnostics;

namespace TextMod
{
    static class ImageEffectManager
    {
        // Sharpening and Compression are instantanious,
        // Saturation and Posterization take a long time.
        public static Image DeepFry(string url)
        {
            WebClient wc = new WebClient();
            int posturization = 30;
            double saturation = 0.5;
            int redness = 50;
            long quality = 1L;
            wc.DownloadFile(url, @"textModTempData\deepfrytarget.png");

            Image imag = Image.FromFile(@"textModTempData\deepfrytarget.png");
            Bitmap i = new Bitmap(imag);
            imag.Dispose();

            Bitmap i2 = PosterizeAndSaturate(i, posturization, saturation, redness);

            Bitmap i3 = Sharpen(i2, 50);

            Image result = Compress(i3, quality);

            i3.Dispose();
            wc.Dispose();
            return result;
        }
        public static Bitmap PosterizeAndSaturate(Bitmap source, int intensity, double satIntensity, int redness)
        {
            unsafe
            {
                BitmapData imageData = source.LockBits(new Rectangle(0, 0, source.Width,
                    source.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                int bytesPerPixel = 3;

                byte* scan0 = (byte*)imageData.Scan0.ToPointer();
                int stride = imageData.Stride;

                for (int y = 0; y < imageData.Height; y++)
                {
                    byte* row = scan0 + (y * stride);

                    for (int x = 0; x < imageData.Width; x++)
                    {
                        int bi = x * bytesPerPixel;
                        int gi = bi + 1;
                        int ri = bi + 2;

                        byte r = row[ri];
                        byte g = row[gi];
                        byte b = row[bi];

                        Color c = Color.FromArgb(r, g, b);
                        ColorToHSV(c, out double h, out double s, out double v);
                        s += satIntensity;
                        if (s > 1) { s = 1; }
                        c = ColorFromHSV(h, s, v);
                        int newred = c.R + redness;
                        if (newred > 255) { newred = 255; }

                        int intr, intg, intb;
                        intr = newred;
                        intg = c.G;
                        intb = c.B;
                        intr = ((int)Math.Round(intr / (decimal)intensity)) * intensity;
                        intg = ((int)Math.Round(intg / (decimal)intensity)) * intensity;
                        intb = ((int)Math.Round(intb / (decimal)intensity)) * intensity;
                        if (intr > 255) { intr = 255; }
                        if (intg > 255) { intg = 255; }
                        if (intb > 255) { intb = 255; }
                        r = (byte)intr;
                        g = (byte)intg;
                        b = (byte)intb;
                        row[ri] = r;
                        row[gi] = g;
                        row[bi] = b;
                    }
                }
                source.UnlockBits(imageData);
            }
            return source;
        }
        public static Image Compress(Bitmap img, long quality)
        {
            SaveJpeg(@"textModTempData\deepfrycompression.jpg", img, quality);
            return Image.FromFile(@"textModTempData\deepfrycompression.jpg");
        }
        public static Bitmap Sharpen(Image image, double strength)
        {
            using (var bitmap = image as Bitmap)
            {
                if (bitmap != null)
                {
                    var sharpenImage = bitmap.Clone() as Bitmap;

                    int width = image.Width;
                    int height = image.Height;

                    // Create sharpening filter.
                    const int filterWidth = 5;
                    const int filterHeight = 5;

                    var filter = new double[,]
                        {
                    {-1, -1, -1, -1, -1},
                    {-1,  2,  2,  2, -1},
                    {-1,  2, 16,  2, -1},
                    {-1,  2,  2,  2, -1},
                    {-1, -1, -1, -1, -1}
                        };

                    double bias = 1.0 - strength;
                    double factor = strength / 16.0;

                    var result = new Color[image.Width, image.Height];

                    // Lock image bits for read/write.
                    if (sharpenImage != null)
                    {
                        BitmapData pbits = sharpenImage.LockBits(new Rectangle(0, 0, width, height),
                                                                    ImageLockMode.ReadWrite,
                                                                    PixelFormat.Format24bppRgb);

                        // Declare an array to hold the bytes of the bitmap.
                        int bytes = pbits.Stride * height;
                        var rgbValues = new byte[bytes];

                        // Copy the RGB values into the array.
                        Marshal.Copy(pbits.Scan0, rgbValues, 0, bytes);

                        int rgb;
                        // Fill the color array with the new sharpened color values.
                        for (int x = 0; x < width; ++x)
                        {
                            for (int y = 0; y < height; ++y)
                            {
                                double red = 0.0, green = 0.0, blue = 0.0;

                                for (int filterX = 0; filterX < filterWidth; filterX++)
                                {
                                    for (int filterY = 0; filterY < filterHeight; filterY++)
                                    {
                                        int imageX = (x - filterWidth / 2 + filterX + width) % width;
                                        int imageY = (y - filterHeight / 2 + filterY + height) % height;

                                        rgb = imageY * pbits.Stride + 3 * imageX;

                                        red += rgbValues[rgb + 2] * filter[filterX, filterY];
                                        green += rgbValues[rgb + 1] * filter[filterX, filterY];
                                        blue += rgbValues[rgb + 0] * filter[filterX, filterY];
                                    }

                                    rgb = y * pbits.Stride + 3 * x;

                                    int r = Math.Min(Math.Max((int)(factor * red + (bias * rgbValues[rgb + 2])), 0), 255);
                                    int g = Math.Min(Math.Max((int)(factor * green + (bias * rgbValues[rgb + 1])), 0), 255);
                                    int b = Math.Min(Math.Max((int)(factor * blue + (bias * rgbValues[rgb + 0])), 0), 255);

                                    result[x, y] = Color.FromArgb(r, g, b);
                                }
                            }
                        }

                        // Update the image with the sharpened pixels.
                        for (int x = 0; x < width; ++x)
                        {
                            for (int y = 0; y < height; ++y)
                            {
                                rgb = y * pbits.Stride + 3 * x;

                                rgbValues[rgb + 2] = result[x, y].R;
                                rgbValues[rgb + 1] = result[x, y].G;
                                rgbValues[rgb + 0] = result[x, y].B;
                            }
                        }

                        // Copy the RGB values back to the bitmap.
                        Marshal.Copy(rgbValues, 0, pbits.Scan0, bytes);
                        // Release image bits.
                        sharpenImage.UnlockBits(pbits);
                    }

                    return sharpenImage;
                }
            }
            return null;
        }
        public static void SaveJpeg(string path, Bitmap image, long quality)
        {
            using(image)
            {
                ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

                System.Drawing.Imaging.Encoder myEncoder =
                    System.Drawing.Imaging.Encoder.Quality;

                EncoderParameters myEncoderParameters = new EncoderParameters(1);

                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);
                myEncoderParameters.Param[0] = myEncoderParameter;
                image.Save(path, jpgEncoder, myEncoderParameters);
            }
        }
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value *= 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }
    }
}
