using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.IO;
using System.Windows.Forms;
using DiscordRPC;
using System.Drawing.Drawing2D;

namespace TextMod
{
    static class ImageGenerator
    {
        public static DiscordRpcClient singleton;
        public static Image TextToImage(string str, int fontSize)
        {
            Bitmap img = new Bitmap(1280, 1280);
            Bitmap i;
            using (Font font = new Font(Program.GetPrivateFont(Program.PrvFont.DiscordFont), fontSize))
            using (Graphics g = Graphics.FromImage(img))
            {
                g.Clear(Color.FromArgb(54, 57, 62));
                g.DrawString(str, font, Brushes.White, new RectangleF(0, 0, 1280, 1280));

                SizeF size = g.MeasureString(str, font, 1280);  
                Rectangle rf = new Rectangle(new Point(0, 0), Size.Round(size));
                i = CropImage(img, rf);
            }
            return i;
        }
        public static Image TextToComment(string str)
        {
            int likesFontSize = 24;
            int repliesFontSize = 24;
            int minutesFontSize = 20;
            int minutesX, minutesY;
            int likesX, likesY;
            int repliesX, repliesY;
            int x, y;
            x = 150;
            y = 109;
            minutesX = 15; // this is offset
            minutesY = 60;
            likesX = 295;
            likesY = 181;
            repliesX = 274;
            repliesY = 253;
            int fontSize = 28;

            int pfpDim = 93; // 93x93
            int pfpX = 30;
            int pfpY = 60;

            // 159x64
            int nameX = 151;
            int nameY = 60;

            string username = "7UKECREAT0R";
            string avatarURL = "https://discordapp.com/assets/dd4dbc0016779df1378e7812eabaa04d.png";
            if (singleton != null)
            {
                User u = singleton.CurrentUser;
                username = u.Username;
                avatarURL = u.GetAvatarURL(User.AvatarFormat.PNG, User.AvatarSize.x64);
            }
            /*if (!File.Exists(@"textModTempData\textmod_comment_v2.png"))
            {
                using (WebClient wc = new WebClient())
                {
                    //MessageBox.Show("Downloading comment image... This is a one time thing!", "Downloading...", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    wc.DownloadFile("https://i.imgur.com/yk7pL2k.png", @"textModTempData\textmod_comment_v2.png");
                }
            }*/
            using(WebClient wc = new WebClient())
            {
                wc.DownloadFile(avatarURL, @"textModTempData\textmod_avatar.png");
            }
            Image pfp = Image.FromFile(@"textModTempData\textmod_avatar.png");
            pfp = CropImage(pfp, new Rectangle(0, 0, pfp.Width, pfp.Height), new Rectangle(0, 0, pfpDim, pfpDim));
            pfp = ClipToCircle(pfp, new PointF(pfp.Width / 2, pfp.Height / 2), pfp.Width / 2, Color.White);

            Image img = Properties.Resources.textmod_comment_v2;
            using (Graphics g = Graphics.FromImage(img))
            {
                FontFamily ff = Program.GetPrivateFont(Program.PrvFont.GoogleFont);
                using (Font font = new Font(ff, fontSize))
                {
                    Font minutesFont = new Font(ff, minutesFontSize, FontStyle.Bold);
                    Font likesFont = new Font(ff, likesFontSize, FontStyle.Bold);
                    Font repliesFont = new Font(ff, repliesFontSize, FontStyle.Bold);
                    g.DrawString(str, font, Brushes.Black, new RectangleF
                        (x, y, img.Width, img.Height));
                    Random r = new Random();
                    int minutes = r.Next(57) + 2;
                    int likes = r.Next(899) + 100;
                    int replies = r.Next(88) + 10;

                    // Draw name
                    g.DrawString(username, repliesFont, Brushes.Black,
                        new RectangleF(nameX, nameY, img.Width, img.Height));
                    // Draw minutes
                    float lngth = g.MeasureString(username, repliesFont).Width;
                    g.DrawString(minutes.ToString() + " minutes ago", minutesFont, Brushes.Gray, 
                        new RectangleF(nameX+lngth+minutesX, minutesY, img.Width, img.Height));
                    // Draw likes
                    g.DrawString(likes.ToString(), likesFont, Brushes.DimGray, 
                        new RectangleF(likesX, likesY, img.Width, img.Height));
                    // Draw replies
                    g.DrawString(replies.ToString(), repliesFont, Brushes.Black, 
                        new RectangleF(repliesX, repliesY, img.Width, img.Height));

                    minutesFont.Dispose();
                    likesFont.Dispose();
                    repliesFont.Dispose();

                    // Draw profile picture
                    g.DrawImage(pfp, pfpX, pfpY);
                    pfp.Dispose();
                }
            }
            return img;
        }
        static Bitmap CropImage(Image originalImage, Rectangle sourceRectangle,
            Rectangle? destinationRectangle = null)
        {
            if (destinationRectangle == null)
            {
                destinationRectangle = new Rectangle(Point.Empty, sourceRectangle.Size);
            }
            var croppedImage = new Bitmap(destinationRectangle.Value.Width,
                destinationRectangle.Value.Height);
            using (var graphics = Graphics.FromImage(croppedImage))
            {
                graphics.DrawImage(originalImage, destinationRectangle.Value,
                    sourceRectangle, GraphicsUnit.Pixel);
            }
            originalImage.Dispose();
            return croppedImage;
        }
        public static Image ClipToCircle(Image srcImage, PointF center, float radius, Color backGround)
        {
            Image dstImage = new Bitmap(srcImage.Width, srcImage.Height, srcImage.PixelFormat);

            using (Graphics g = Graphics.FromImage(dstImage))
            {
                RectangleF r = new RectangleF(center.X - radius, center.Y - radius,
                                                         radius * 2, radius * 2);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // fills background color
                using (Brush br = new SolidBrush(backGround))
                {
                    g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
                }

                // adds the new ellipse & draws the image again 
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(r);
                g.SetClip(path);
                path.Dispose();
                g.DrawImage(srcImage, 0, 0);
                srcImage.Dispose();
                return dstImage;
            }
        }
    }
}
