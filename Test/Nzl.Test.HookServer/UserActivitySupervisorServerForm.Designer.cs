namespace Nzl.Test.HookServer
{
    partial class UserActivitySupervisorServerForm
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
            this.components = new System.ComponentModel.Container();
            this.txtBox = new System.Windows.Forms.RichTextBox();
            this.timerSharedMemory = new System.Windows.Forms.Timer(this.components);
            this.timerCheckClient = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // txtBox
            // 
            this.txtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBox.Location = new System.Drawing.Point(0, 0);
            this.txtBox.Name = "txtBox";
            this.txtBox.ReadOnly = true;
            this.txtBox.Size = new System.Drawing.Size(322, 285);
            this.txtBox.TabIndex = 0;
            this.txtBox.Text = "";
            // 
            // timerSharedMemory
            // 
            this.timerSharedMemory.Tick += new System.EventHandler(this.timerSharedMemory_Tick);
            // 
            // timerCheckClient
            // 
            this.timerCheckClient.Tick += new System.EventHandler(this.timerCheckClient_Tick);
            // 
            // UserActivitySupervisorServerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(322, 285);
            this.Controls.Add(this.txtBox);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "UserActivitySupervisorServerForm";
            this.Text = "User Activity Supervisor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserActivitySupervisorServerForm_FormClosing);
            this.Shown += new System.EventHandler(this.UserActivityLoggerServerForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox txtBox;
        private System.Windows.Forms.Timer timerSharedMemory;
        private System.Windows.Forms.Timer timerCheckClient;
    }
}

