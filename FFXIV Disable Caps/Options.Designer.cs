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
            SuspendLayout();
            // 
            // timer_chk_ffxiv_process
            // 
            timer_chk_ffxiv_process.Enabled = true;
            timer_chk_ffxiv_process.Interval = 10000;
            timer_chk_ffxiv_process.Tick += timer1_Tick;
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(354, 74);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Options";
            Text = "FFXIV CapsLock Disabler";
            Load += Options_Load;
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timer_chk_ffxiv_process;
    }
}
