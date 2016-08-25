namespace Nzl.Smth.Forms
{
    partial class TestForm
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
            this.richTextBoxEx1 = new Nzl.Controls.RichTextBoxEx();
            this.SuspendLayout();
            // 
            // richTextBoxEx1
            // 
            this.richTextBoxEx1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.richTextBoxEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxEx1.HiglightColor = Nzl.Controls.RtfColor.White;
            this.richTextBoxEx1.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxEx1.Name = "richTextBoxEx1";
            this.richTextBoxEx1.Size = new System.Drawing.Size(467, 437);
            this.richTextBoxEx1.TabIndex = 0;
            this.richTextBoxEx1.Text = "";
            this.richTextBoxEx1.TextColor = Nzl.Controls.RtfColor.Black;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(467, 437);
            this.Controls.Add(this.richTextBoxEx1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "TestForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Mail Detail";
            this.ResumeLayout(false);

        }

        #endregion

        private Nzl.Controls.RichTextBoxEx richTextBoxEx1;
    }
}