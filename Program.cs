using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace TextMod
{
    static class Program
    {
        public enum PrvFont
        {
            DiscordFont = 2,
            TextModFont = 0,
            GoogleFont = 1
        }
        public static PrivateFontCollection pfc = new PrivateFontCollection();
        public static FontFamily GetPrivateFont(PrvFont select)
        {
            /*foreach(FontFamily ff in pfc.Families)
            {
                MessageBox.Show(ff.Name);
            }*/
            return pfc.Families[(int)select];
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
