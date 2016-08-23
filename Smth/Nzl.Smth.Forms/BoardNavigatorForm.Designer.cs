namespace Nzl.Smth.Forms
{
    partial class BoardNavigatorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BoardNavigatorForm));
            this.sncSection = new Nzl.Smth.Controls.Containers.SectionContainer();
            this.SuspendLayout();
            // 
            // sncSection
            // 
            this.sncSection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sncSection.Location = new System.Drawing.Point(0, 0);
            this.sncSection.Name = "sncSection";
            this.sncSection.Size = new System.Drawing.Size(314, 467);
            this.sncSection.TabIndex = 0;
            // 
            // BoardNavigatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 467);
            this.Controls.Add(this.sncSection);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BoardNavigatorForm";
            this.Text = "Board Navigator";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BoardNavigatorForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Containers.SectionContainer sncSection;
    }
}