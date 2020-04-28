using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TextMod
{
    public partial class KeyDialog : Form
    {
        public KeyDialog()
        {
            InitializeComponent();
        }
        protected Keys df;
        public Keys selected;

        private void KeyDialog_Load(object sender, EventArgs e)
        {
            listBox1.HorizontalScrollbar = false;
            df = Form1.processhotkey;
            Array keys = Enum.GetValues(typeof(Keys));
            foreach(Keys k in keys)
            {
                /*if(k == Keys.Enter ||
                k == Keys.Escape)
                {
                    continue;
                }*/
                listBox1.Items.Add(k);
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            object sel = listBox1.SelectedItem;
            if(sel == null)
            {
                return;
            }
            Keys k = (Keys)sel;
            selected = k;
        }

        private void confirmButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            selected = df;
            Close();
        }
    }
}
