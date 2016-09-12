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
            this.tccTopic = new Nzl.Smth.Controls.Containers.ThreadControlContainer();
            this.SuspendLayout();
            // 
            // tccTopic
            // 
            this.tccTopic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tccTopic.IsRecycled = false;
            this.tccTopic.Location = new System.Drawing.Point(0, 0);
            this.tccTopic.Name = "tccTopic";
            this.tccTopic.Size = new System.Drawing.Size(756, 569);
            this.tccTopic.Status = Nzl.Recycling.RecycledStatus.Using;
            this.tccTopic.TabIndex = 0;
            this.tccTopic.TargetUserID = null;
            this.tccTopic.Url = null;
            // 
            // TopicForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 569);
            this.Controls.Add(this.tccTopic);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "TopicForm";
            this.Text = "Topic";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Containers.ThreadControlContainer tccTopic;

    }
}