namespace Nzl.Web.Smth.Forms
{
    partial class LoginForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUserID = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.ckbAutoLogon = new System.Windows.Forms.CheckBox();
            this.btnLogon = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.bwFetchPage = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "User ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Password";
            // 
            // txtUserID
            // 
            this.txtUserID.Location = new System.Drawing.Point(99, 28);
            this.txtUserID.Name = "txtUserID";
            this.txtUserID.Size = new System.Drawing.Size(128, 21);
            this.txtUserID.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(99, 70);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(128, 21);
            this.txtPassword.TabIndex = 2;
            // 
            // ckbAutoLogon
            // 
            this.ckbAutoLogon.AutoSize = true;
            this.ckbAutoLogon.Checked = true;
            this.ckbAutoLogon.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbAutoLogon.Location = new System.Drawing.Point(99, 119);
            this.ckbAutoLogon.Name = "ckbAutoLogon";
            this.ckbAutoLogon.Size = new System.Drawing.Size(84, 16);
            this.ckbAutoLogon.TabIndex = 3;
            this.ckbAutoLogon.Text = "Auto logon";
            this.ckbAutoLogon.UseVisualStyleBackColor = true;
            // 
            // btnLogon
            // 
            this.btnLogon.Location = new System.Drawing.Point(31, 155);
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.Size = new System.Drawing.Size(75, 23);
            this.btnLogon.TabIndex = 4;
            this.btnLogon.Text = "Login";
            this.btnLogon.UseVisualStyleBackColor = true;
            this.btnLogon.Click += new System.EventHandler(this.btnLogon_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(169, 155);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(75, 23);
            this.btnLogout.TabIndex = 5;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnLogon);
            this.panel1.Controls.Add(this.txtUserID);
            this.panel1.Controls.Add(this.ckbAutoLogon);
            this.panel1.Controls.Add(this.txtPassword);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(284, 201);
            this.panel1.TabIndex = 6;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 201);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginForm";
            this.Text = "Logon";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUserID;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox ckbAutoLogon;
        private System.Windows.Forms.Button btnLogon;
        private System.Windows.Forms.Button btnLogout;
        private System.ComponentModel.BackgroundWorker bwFetchPage;
        private System.Windows.Forms.Panel panel1;
    }
}