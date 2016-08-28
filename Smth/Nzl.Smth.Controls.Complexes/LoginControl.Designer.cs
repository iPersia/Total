namespace Nzl.Smth.Controls.Complexes
{
    partial class LoginControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panelContainer = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnLogon = new System.Windows.Forms.Button();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.ckbAutoLogon = new System.Windows.Forms.CheckBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.scContainer = new System.Windows.Forms.SplitContainer();
            this.panelUp = new System.Windows.Forms.Panel();
            this.lblNote = new System.Windows.Forms.Label();
            this.panelContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scContainer)).BeginInit();
            this.scContainer.Panel1.SuspendLayout();
            this.scContainer.Panel2.SuspendLayout();
            this.scContainer.SuspendLayout();
            this.panelUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelContainer.Controls.Add(this.btnLogout);
            this.panelContainer.Controls.Add(this.btnLogon);
            this.panelContainer.Controls.Add(this.txtUserID);
            this.panelContainer.Controls.Add(this.ckbAutoLogon);
            this.panelContainer.Controls.Add(this.txtPassword);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(265, 234);
            this.panelContainer.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.Location = new System.Drawing.Point(94, 184);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 12;
            this.btnLogout.Text = "Sign out";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnLogon
            // 
            this.btnLogon.Location = new System.Drawing.Point(66, 138);
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.Size = new System.Drawing.Size(128, 23);
            this.btnLogon.TabIndex = 11;
            this.btnLogon.Text = "Sign in";
            this.btnLogon.UseVisualStyleBackColor = true;
            this.btnLogon.Click += new System.EventHandler(this.btnLogon_Click);
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(66, 25);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(128, 21);
            this.txtUserID.TabIndex = 8;
            // 
            // ckbAutoLogon
            // 
            this.ckbAutoLogon.AutoSize = true;
            this.ckbAutoLogon.Checked = true;
            this.ckbAutoLogon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAutoLogon.Location = new System.Drawing.Point(93, 109);
            this.ckbAutoLogon.Name = "ckbAutoLogon";
            this.ckbAutoLogon.Size = new System.Drawing.Size(84, 16);
            this.ckbAutoLogon.TabIndex = 10;
            this.ckbAutoLogon.Text = "Auto logon";
            this.ckbAutoLogon.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(66, 68);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(128, 21);
            this.txtPassword.TabIndex = 9;
            // 
            // scContainer
            // 
            this.scContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scContainer.IsSplitterFixed = true;
            this.scContainer.Location = new System.Drawing.Point(0, 0);
            this.scContainer.Name = "scContainer";
            this.scContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scContainer.Panel1
            // 
            this.scContainer.Panel1.Controls.Add(this.panelUp);
            // 
            // scContainer.Panel2
            // 
            this.scContainer.Panel2.Controls.Add(this.panelContainer);
            this.scContainer.Size = new System.Drawing.Size(265, 260);
            this.scContainer.SplitterDistance = 25;
            this.scContainer.SplitterWidth = 1;
            this.scContainer.TabIndex = 2;
            this.scContainer.TabStop = false;
            // 
            // panelUp
            // 
            this.panelUp.BackColor = System.Drawing.SystemColors.Window;
            this.panelUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUp.Controls.Add(this.lblNote);
            this.panelUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUp.Location = new System.Drawing.Point(0, 0);
            this.panelUp.Name = "panelUp";
            this.panelUp.Size = new System.Drawing.Size(265, 25);
            this.panelUp.TabIndex = 0;
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(95, 6);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(83, 12);
            this.lblNote.TabIndex = 1;
            this.lblNote.Text = "Sign In && Out";
            // 
            // LoginControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scContainer);
            this.Name = "LoginControl";
            this.Size = new System.Drawing.Size(265, 260);
            this.panelContainer.ResumeLayout(false);
            this.panelContainer.PerformLayout();
            this.scContainer.Panel1.ResumeLayout(false);
            this.scContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scContainer)).EndInit();
            this.scContainer.ResumeLayout(false);
            this.panelUp.ResumeLayout(false);
            this.panelUp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.SplitContainer scContainer;
        private System.Windows.Forms.Panel panelUp;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnLogon;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.CheckBox ckbAutoLogon;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblNote;
    }
}
