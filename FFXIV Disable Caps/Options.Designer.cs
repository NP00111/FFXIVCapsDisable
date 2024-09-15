namespace FFXIV_Disable_Caps
{
    partial class Options
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options));
            timer_chk_ffxiv_process = new System.Windows.Forms.Timer(components);
            chkAutoStart = new CheckBox();
            TrayIcon = new NotifyIcon(components);
            SuspendLayout();
            // 
            // timer_chk_ffxiv_process
            // 
            timer_chk_ffxiv_process.Enabled = true;
            timer_chk_ffxiv_process.Interval = 10000;
            timer_chk_ffxiv_process.Tick += timert_check_process;
            // 
            // chkAutoStart
            // 
            chkAutoStart.AutoSize = true;
            chkAutoStart.Location = new Point(12, 28);
            chkAutoStart.Name = "chkAutoStart";
            chkAutoStart.Size = new Size(162, 19);
            chkAutoStart.TabIndex = 0;
            chkAutoStart.Text = "Auto Start with Windows?";
            chkAutoStart.UseVisualStyleBackColor = true;
            chkAutoStart.Click += chkAutoStart_Click;
            // 
            // TrayIcon
            // 
            TrayIcon.Icon = (Icon)resources.GetObject("TrayIcon.Icon");
            TrayIcon.Text = "notifyIcon1";
            TrayIcon.Visible = true;
            TrayIcon.Click += TrayIcon_Click;
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(282, 74);
            Controls.Add(chkAutoStart);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Options";
            Text = "FFXIV CapsLock Disabler";
            FormClosing += Options_FormClosing;
            Load += Options_Load;
            Resize += Options_Resize;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Timer timer_chk_ffxiv_process;
        private CheckBox chkAutoStart;
        private NotifyIcon TrayIcon;
    }
}
