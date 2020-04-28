namespace TextMod
{
    partial class InfoForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.versionText = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.creditText = new System.Windows.Forms.Label();
            this.changelogText = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // versionText
            // 
            this.versionText.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.versionText.Location = new System.Drawing.Point(5, 67);
            this.versionText.Name = "versionText";
            this.versionText.Size = new System.Drawing.Size(491, 40);
            this.versionText.TabIndex = 0;
            this.versionText.Text = "TextMod Version X.XX";
            this.versionText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 409);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(484, 42);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // creditText
            // 
            this.creditText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.creditText.ForeColor = System.Drawing.Color.Silver;
            this.creditText.Location = new System.Drawing.Point(12, 108);
            this.creditText.Name = "creditText";
            this.creditText.Size = new System.Drawing.Size(484, 45);
            this.creditText.TabIndex = 3;
            this.creditText.Text = "Developed by 7UKECREAT0R\r\nUI by ZeniGraphics (xReddish)";
            this.creditText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // changelogText
            // 
            this.changelogText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.changelogText.ForeColor = System.Drawing.Color.White;
            this.changelogText.Location = new System.Drawing.Point(12, 160);
            this.changelogText.Name = "changelogText";
            this.changelogText.Size = new System.Drawing.Size(484, 246);
            this.changelogText.TabIndex = 4;
            this.changelogText.Text = "Changelog Placeholder";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Location = new System.Drawing.Point(0, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 12);
            this.panel1.TabIndex = 5;
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(508, 463);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.changelogText);
            this.Controls.Add(this.creditText);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.versionText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "InfoForm";
            this.Text = "InfoForm";
            this.Load += new System.EventHandler(this.InfoForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.InfoForm_MouseDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label versionText;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label creditText;
        private System.Windows.Forms.Label changelogText;
        private System.Windows.Forms.Panel panel1;
    }
}