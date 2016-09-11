namespace Nzl.Smth.Controls.Complexes
{
    partial class ReferControl
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
            this.tcRefer = new System.Windows.Forms.TabControl();
            this.scContainer = new System.Windows.Forms.SplitContainer();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnReadAll = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.scContainer)).BeginInit();
            this.scContainer.Panel1.SuspendLayout();
            this.scContainer.Panel2.SuspendLayout();
            this.scContainer.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcRefer
            // 
            this.tcRefer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcRefer.Location = new System.Drawing.Point(0, 0);
            this.tcRefer.Name = "tcRefer";
            this.tcRefer.SelectedIndex = 0;
            this.tcRefer.Size = new System.Drawing.Size(720, 534);
            this.tcRefer.TabIndex = 0;
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
            this.scContainer.Panel1.Controls.Add(this.panelMenu);
            // 
            // scContainer.Panel2
            // 
            this.scContainer.Panel2.Controls.Add(this.tcRefer);
            this.scContainer.Size = new System.Drawing.Size(720, 560);
            this.scContainer.SplitterDistance = 25;
            this.scContainer.SplitterWidth = 1;
            this.scContainer.TabIndex = 1;
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.SystemColors.Window;
            this.panelMenu.Controls.Add(this.btnReadAll);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(720, 25);
            this.panelMenu.TabIndex = 0;
            // 
            // btnReadAll
            // 
            this.btnReadAll.Location = new System.Drawing.Point(304, 1);
            this.btnReadAll.Name = "btnReadAll";
            this.btnReadAll.Size = new System.Drawing.Size(75, 23);
            this.btnReadAll.TabIndex = 0;
            this.btnReadAll.Text = "Read all";
            this.btnReadAll.UseVisualStyleBackColor = true;
            this.btnReadAll.Click += new System.EventHandler(this.btnReadAll_Click);
            // 
            // ReferControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scContainer);
            this.Name = "ReferControl";
            this.Size = new System.Drawing.Size(720, 560);
            this.scContainer.Panel1.ResumeLayout(false);
            this.scContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scContainer)).EndInit();
            this.scContainer.ResumeLayout(false);
            this.panelMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcRefer;
        private System.Windows.Forms.SplitContainer scContainer;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Button btnReadAll;
    }
}
