namespace Nzl.Smth.Controls.Complexes
{
    partial class Top10sBrowserControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tcTop10s = new System.Windows.Forms.TabControl();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panelContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcTop10s
            // 
            this.tcTop10s.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcTop10s.Location = new System.Drawing.Point(0, 0);
            this.tcTop10s.Name = "tcTop10s";
            this.tcTop10s.SelectedIndex = 0;
            this.tcTop10s.Size = new System.Drawing.Size(685, 420);
            this.tcTop10s.TabIndex = 1;
            // 
            // panelContainer
            // 
            this.panelContainer.BackColor = System.Drawing.SystemColors.Window;
            this.panelContainer.Controls.Add(this.tcTop10s);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(685, 420);
            this.panelContainer.TabIndex = 2;
            // 
            // Top10sBrowserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContainer);
            this.Name = "Top10sBrowserControl";
            this.Size = new System.Drawing.Size(685, 420);
            this.panelContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcTop10s;
        private System.Windows.Forms.Panel panelContainer;
    }
}
