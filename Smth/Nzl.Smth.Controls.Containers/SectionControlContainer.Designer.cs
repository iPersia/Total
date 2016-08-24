namespace Nzl.Smth.Controls.Containers
{
    partial class SectionControlContainer
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
            this.panel = new System.Windows.Forms.Panel();
            this.scContainer = new System.Windows.Forms.SplitContainer();
            this.panelUp = new System.Windows.Forms.Panel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.linklblSectionName = new System.Windows.Forms.LinkLabel();
            this.linklblPrevious = new System.Windows.Forms.LinkLabel();
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
            this.panelContainer.Controls.Add(this.panel);
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(0, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(317, 480);
            this.panelContainer.TabIndex = 0;
            // 
            // panel
            // 
            this.panel.Location = new System.Drawing.Point(3, 3);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(311, 100);
            this.panel.TabIndex = 0;
            // 
            // scContainer
            // 
            this.scContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scContainer.Location = new System.Drawing.Point(0, 0);
            this.scContainer.Name = "scContainer";
            this.scContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scContainer.Panel1
            // 
            this.scContainer.Panel1.Controls.Add(this.panelUp);
            this.scContainer.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // scContainer.Panel2
            // 
            this.scContainer.Panel2.Controls.Add(this.panelContainer);
            this.scContainer.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.scContainer.Size = new System.Drawing.Size(317, 506);
            this.scContainer.SplitterDistance = 25;
            this.scContainer.SplitterWidth = 1;
            this.scContainer.TabIndex = 1;
            this.scContainer.TabStop = false;
            // 
            // panelUp
            // 
            this.panelUp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelUp.Controls.Add(this.btnRefresh);
            this.panelUp.Controls.Add(this.linklblSectionName);
            this.panelUp.Controls.Add(this.linklblPrevious);
            this.panelUp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelUp.Location = new System.Drawing.Point(0, 0);
            this.panelUp.Name = "panelUp";
            this.panelUp.Size = new System.Drawing.Size(317, 25);
            this.panelUp.TabIndex = 0;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(120, 0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // linklblSectionName
            // 
            this.linklblSectionName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linklblSectionName.Location = new System.Drawing.Point(186, 6);
            this.linklblSectionName.Name = "linklblSectionName";
            this.linklblSectionName.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.linklblSectionName.Size = new System.Drawing.Size(120, 12);
            this.linklblSectionName.TabIndex = 1;
            this.linklblSectionName.TabStop = true;
            this.linklblSectionName.Text = "Section";
            // 
            // linklblPrevious
            // 
            this.linklblPrevious.AutoSize = true;
            this.linklblPrevious.Location = new System.Drawing.Point(11, 6);
            this.linklblPrevious.Name = "linklblPrevious";
            this.linklblPrevious.Size = new System.Drawing.Size(53, 12);
            this.linklblPrevious.TabIndex = 0;
            this.linklblPrevious.TabStop = true;
            this.linklblPrevious.Text = "Previous";
            // 
            // SectionNavigationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scContainer);
            this.Name = "SectionNavigationControl";
            this.Size = new System.Drawing.Size(317, 506);
            this.panelContainer.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.SplitContainer scContainer;
        private System.Windows.Forms.Panel panelUp;
        private System.Windows.Forms.LinkLabel linklblPrevious;
        private System.Windows.Forms.LinkLabel linklblSectionName;
        private System.Windows.Forms.Button btnRefresh;
    }
}
