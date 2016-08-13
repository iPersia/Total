namespace Nzl.Smth.Forms
{
    partial class TopicForm
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
            this.tbcTopic = new Nzl.Smth.Containers.TopicBrowserControl();
            this.SuspendLayout();
            // 
            // tbcTopic
            // 
            this.tbcTopic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcTopic.Location = new System.Drawing.Point(0, 0);
            this.tbcTopic.Name = "tbcTopic";
            this.tbcTopic.Size = new System.Drawing.Size(756, 569);
            this.tbcTopic.TabIndex = 0;
            this.tbcTopic.TargetUserID = null;
            this.tbcTopic.TopicUrl = null;
            // 
            // TopicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 569);
            this.Controls.Add(this.tbcTopic);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "TopicForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Topic";
            this.ResumeLayout(false);

        }

        #endregion

        private Containers.TopicBrowserControl tbcTopic;

    }
}