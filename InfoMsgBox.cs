using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextMod
{
    public partial class InfoMsgBox : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect, int nTopRect,
            int nRightRect, int nBottomRect,
            int nWidthEllipse, int nHeightEllipse);
        public void SetBorderCurve(int radius)
        {
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, radius, radius));
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public InfoMsgBox(string text)
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            SetBorderCurve(45);
            label1.Text = text;
            Text = "Command Info";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void InfoMsgBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
