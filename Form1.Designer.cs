namespace TextMod
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.button1 = new System.Windows.Forms.Button();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.TextOperators = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.saveFileDialog2 = new System.Windows.Forms.SaveFileDialog();
            this.performanceModeButton = new System.Windows.Forms.Button();
            this.activatorChanger = new System.Windows.Forms.Button();
            this.activatorDisplay = new System.Windows.Forms.Label();
            this.bypassCheckbox = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Templates = new System.Windows.Forms.Button();
            this.ImageCommands = new System.Windows.Forms.Button();
            this.FreeNitro = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            this.minimize = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Ostrich Sans Rounded", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(12, 412);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(365, 46);
            this.button1.TabIndex = 6;
            this.button1.Text = "Emoji Folder";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "The application is now minimized to the tray! Click on the icon to bring it back." +
    "";
            this.notifyIcon.BalloonTipTitle = "TextMod";
            this.notifyIcon.Text = "TextMod - Running";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Ostrich Sans Rounded", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Location = new System.Drawing.Point(383, 412);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(527, 46);
            this.button2.TabIndex = 9;
            this.button2.Text = "Info";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TextOperators
            // 
            this.TextOperators.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
            this.TextOperators.FlatAppearance.BorderSize = 0;
            this.TextOperators.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TextOperators.Font = new System.Drawing.Font("Ostrich Sans Rounded", 38F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextOperators.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.TextOperators.Location = new System.Drawing.Point(478, 573);
            this.TextOperators.Name = "TextOperators";
            this.TextOperators.Size = new System.Drawing.Size(434, 150);
            this.TextOperators.TabIndex = 6;
            this.TextOperators.Text = "Main Commands";
            this.TextOperators.UseVisualStyleBackColor = false;
            this.TextOperators.Click += new System.EventHandler(this.TextOperators_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // performanceModeButton
            // 
            this.performanceModeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.performanceModeButton.FlatAppearance.BorderSize = 0;
            this.performanceModeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.performanceModeButton.Font = new System.Drawing.Font("Ostrich Sans Rounded", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.performanceModeButton.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.performanceModeButton.Location = new System.Drawing.Point(10, 360);
            this.performanceModeButton.Name = "performanceModeButton";
            this.performanceModeButton.Size = new System.Drawing.Size(539, 46);
            this.performanceModeButton.TabIndex = 12;
            this.performanceModeButton.Text = "Performance Mode";
            this.performanceModeButton.UseVisualStyleBackColor = false;
            this.performanceModeButton.Click += new System.EventHandler(this.performanceModeButton_Click);
            // 
            // activatorChanger
            // 
            this.activatorChanger.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.activatorChanger.FlatAppearance.BorderSize = 0;
            this.activatorChanger.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.activatorChanger.Font = new System.Drawing.Font("Ostrich Sans Rounded", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activatorChanger.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.activatorChanger.Location = new System.Drawing.Point(411, 500);
            this.activatorChanger.Name = "activatorChanger";
            this.activatorChanger.Size = new System.Drawing.Size(319, 30);
            this.activatorChanger.TabIndex = 13;
            this.activatorChanger.Text = "Change Activator Key";
            this.activatorChanger.UseVisualStyleBackColor = false;
            this.activatorChanger.Click += new System.EventHandler(this.activatorChanger_Click);
            // 
            // activatorDisplay
            // 
            this.activatorDisplay.Font = new System.Drawing.Font("Ostrich Sans Rounded", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.activatorDisplay.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.activatorDisplay.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.activatorDisplay.Location = new System.Drawing.Point(12, 499);
            this.activatorDisplay.Name = "activatorDisplay";
            this.activatorDisplay.Size = new System.Drawing.Size(393, 30);
            this.activatorDisplay.TabIndex = 14;
            this.activatorDisplay.Text = "Current Key: Return";
            this.activatorDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // bypassCheckbox
            // 
            this.bypassCheckbox.AutoSize = true;
            this.bypassCheckbox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bypassCheckbox.FlatAppearance.BorderSize = 0;
            this.bypassCheckbox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bypassCheckbox.Font = new System.Drawing.Font("Ostrich Sans Rounded", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bypassCheckbox.ForeColor = System.Drawing.Color.White;
            this.bypassCheckbox.Location = new System.Drawing.Point(72, 9);
            this.bypassCheckbox.Name = "bypassCheckbox";
            this.bypassCheckbox.Size = new System.Drawing.Size(223, 28);
            this.bypassCheckbox.TabIndex = 15;
            this.bypassCheckbox.Text = "BYPASS DYNO/MEE6";
            this.bypassCheckbox.UseVisualStyleBackColor = true;
            this.bypassCheckbox.CheckedChanged += new System.EventHandler(this.bypassCheckbox_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.panel1.Controls.Add(this.bypassCheckbox);
            this.panel1.Location = new System.Drawing.Point(553, 360);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(357, 46);
            this.panel1.TabIndex = 16;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::TextMod.Properties.Resources.textmodheader;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Location = new System.Drawing.Point(10, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(902, 307);
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // Templates
            // 
            this.Templates.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.Templates.BackgroundImage = global::TextMod.Properties.Resources.othercommands;
            this.Templates.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Templates.FlatAppearance.BorderSize = 0;
            this.Templates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Templates.Font = new System.Drawing.Font("Ostrich Sans Rounded", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Templates.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Templates.Location = new System.Drawing.Point(166, 573);
            this.Templates.Name = "Templates";
            this.Templates.Size = new System.Drawing.Size(150, 150);
            this.Templates.TabIndex = 6;
            this.Templates.UseVisualStyleBackColor = false;
            this.Templates.Click += new System.EventHandler(this.Templates_Click);
            // 
            // ImageCommands
            // 
            this.ImageCommands.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.ImageCommands.BackgroundImage = global::TextMod.Properties.Resources.imagecommands;
            this.ImageCommands.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ImageCommands.FlatAppearance.BorderSize = 0;
            this.ImageCommands.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ImageCommands.Font = new System.Drawing.Font("Ostrich Sans Rounded", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ImageCommands.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ImageCommands.Location = new System.Drawing.Point(322, 573);
            this.ImageCommands.Name = "ImageCommands";
            this.ImageCommands.Size = new System.Drawing.Size(150, 150);
            this.ImageCommands.TabIndex = 6;
            this.ImageCommands.UseVisualStyleBackColor = false;
            this.ImageCommands.Click += new System.EventHandler(this.ImageCommands_Click);
            // 
            // FreeNitro
            // 
            this.FreeNitro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.FreeNitro.BackgroundImage = global::TextMod.Properties.Resources.customemojiquickimagesquickcopypasta;
            this.FreeNitro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FreeNitro.FlatAppearance.BorderSize = 0;
            this.FreeNitro.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FreeNitro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FreeNitro.Font = new System.Drawing.Font("Ostrich Sans Rounded", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FreeNitro.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FreeNitro.Location = new System.Drawing.Point(10, 573);
            this.FreeNitro.Name = "FreeNitro";
            this.FreeNitro.Size = new System.Drawing.Size(150, 150);
            this.FreeNitro.TabIndex = 6;
            this.FreeNitro.UseVisualStyleBackColor = false;
            this.FreeNitro.Click += new System.EventHandler(this.FreeNitro_Click);
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.exit.FlatAppearance.BorderSize = 0;
            this.exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit.ForeColor = System.Drawing.Color.White;
            this.exit.Location = new System.Drawing.Point(881, 12);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(29, 29);
            this.exit.TabIndex = 18;
            this.exit.Text = "X";
            this.exit.UseVisualStyleBackColor = false;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // minimize
            // 
            this.minimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(39)))));
            this.minimize.FlatAppearance.BorderSize = 0;
            this.minimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimize.Font = new System.Drawing.Font("Wingdings 3", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimize.ForeColor = System.Drawing.Color.White;
            this.minimize.Location = new System.Drawing.Point(846, 12);
            this.minimize.Name = "minimize";
            this.minimize.Size = new System.Drawing.Size(29, 29);
            this.minimize.TabIndex = 19;
            this.minimize.Text = "__";
            this.minimize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.minimize.UseVisualStyleBackColor = false;
            this.minimize.Click += new System.EventHandler(this.minimize_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(924, 734);
            this.Controls.Add(this.minimize);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.activatorDisplay);
            this.Controls.Add(this.activatorChanger);
            this.Controls.Add(this.performanceModeButton);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.Templates);
            this.Controls.Add(this.ImageCommands);
            this.Controls.Add(this.FreeNitro);
            this.Controls.Add(this.TextOperators);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "   ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button TextOperators;
        private System.Windows.Forms.Button Templates;
        private System.Windows.Forms.Button FreeNitro;
        private System.Windows.Forms.Button ImageCommands;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog2;
        private System.Windows.Forms.Button performanceModeButton;
        private System.Windows.Forms.Button activatorChanger;
        private System.Windows.Forms.Label activatorDisplay;
        private System.Windows.Forms.CheckBox bypassCheckbox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button minimize;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

