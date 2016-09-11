namespace Nzl.Smth.Forms
{
    partial class NewThreadForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewThreadForm));
            this.btnSubmit = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.txtTitle = new System.Windows.Forms.TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.ckbSendMail = new System.Windows.Forms.CheckBox();
            this.txtContent = new System.Windows.Forms.RichTextBox();
            this.lblContent = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnSubmit.Location = new System.Drawing.Point(187, 356);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(100, 23);
            this.btnSubmit.TabIndex = 4;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // panel
            // 
            this.panel.Controls.Add(this.txtTitle);
            this.panel.Controls.Add(this.lblTitle);
            this.panel.Controls.Add(this.ckbSendMail);
            this.panel.Controls.Add(this.txtContent);
            this.panel.Controls.Add(this.lblContent);
            this.panel.Controls.Add(this.btnSubmit);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(474, 392);
            this.panel.TabIndex = 1;
            // 
            // txtTitle
            // 
            this.txtTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitle.Location = new System.Drawing.Point(14, 35);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(448, 21);
            this.txtTitle.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(16, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(41, 12);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "Title:";
            // 
            // ckbSendMail
            // 
            this.ckbSendMail.AutoSize = true;
            this.ckbSendMail.Location = new System.Drawing.Point(14, 331);
            this.ckbSendMail.Name = "ckbSendMail";
            this.ckbSendMail.Size = new System.Drawing.Size(78, 16);
            this.ckbSendMail.TabIndex = 3;
            this.ckbSendMail.Text = "Send Mail";
            this.ckbSendMail.UseVisualStyleBackColor = true;
            this.ckbSendMail.Visible = false;
            // 
            // txtContent
            // 
            this.txtContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtContent.Location = new System.Drawing.Point(12, 88);
            this.txtContent.Name = "txtContent";
            this.txtContent.Size = new System.Drawing.Size(450, 232);
            this.txtContent.TabIndex = 2;
            this.txtContent.Text = "";
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Location = new System.Drawing.Point(15, 68);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(47, 12);
            this.lblContent.TabIndex = 1;
            this.lblContent.Text = "Content";
            // 
            // NewThreadForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 392);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewThreadForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "New Thread";
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.RichTextBox txtContent;
        private System.Windows.Forms.CheckBox ckbSendMail;
        private System.Windows.Forms.TextBox txtTitle;
        private System.Windows.Forms.Label lblTitle;
    }
}