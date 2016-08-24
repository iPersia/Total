namespace Nzl.Smth.Forms
{
    partial class FavorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavorForm));
            this.fcFavor = new Nzl.Smth.Controls.Containers.BoardControlContainer();
            this.SuspendLayout();
            // 
            // fcFavor
            // 
            this.fcFavor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fcFavor.Location = new System.Drawing.Point(0, 0);
            this.fcFavor.Name = "fcFavor";
            this.fcFavor.Size = new System.Drawing.Size(365, 539);
            this.fcFavor.TabIndex = 0;
            // 
            // FavorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(365, 539);
            this.Controls.Add(this.fcFavor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FavorForm";
            this.Text = "Favor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FavorForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Containers.BoardControlContainer fcFavor;
    }
}