namespace Nzl.Web.Smth.Forms
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
            this.sectionNavigationControl1 = new Nzl.Web.Smth.Controls.SectionNavigationControl();
            this.SuspendLayout();
            // 
            // sectionNavigationControl1
            // 
            this.sectionNavigationControl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sectionNavigationControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sectionNavigationControl1.Location = new System.Drawing.Point(0, 0);
            this.sectionNavigationControl1.Name = "sectionNavigationControl1";
            this.sectionNavigationControl1.Size = new System.Drawing.Size(410, 565);
            this.sectionNavigationControl1.TabIndex = 0;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(410, 565);
            this.Controls.Add(this.sectionNavigationControl1);
            this.Name = "TestForm";
            this.Text = "TestForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.SectionNavigationControl sectionNavigationControl1;
    }
}