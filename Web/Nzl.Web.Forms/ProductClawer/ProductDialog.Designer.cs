namespace Nzl.Web.Forms.ProductClawer
{
    partial class ProductDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductDialog));
            this.txtURL = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTargetPrice = new System.Windows.Forms.TextBox();
            this.txtOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtURL
            // 
            this.txtURL.Location = new System.Drawing.Point(109, 64);
            this.txtURL.Name = "txtURL";
            this.txtURL.Size = new System.Drawing.Size(450, 21);
            this.txtURL.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "网址*";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "间隔";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(109, 90);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(450, 21);
            this.txtInterval.TabIndex = 3;
            this.txtInterval.Text = "2000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 120);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "目标价格";
            // 
            // txtTargetPrice
            // 
            this.txtTargetPrice.Location = new System.Drawing.Point(109, 116);
            this.txtTargetPrice.Name = "txtTargetPrice";
            this.txtTargetPrice.Size = new System.Drawing.Size(450, 21);
            this.txtTargetPrice.TabIndex = 4;
            this.txtTargetPrice.Text = "2000";
            // 
            // txtOK
            // 
            this.txtOK.Location = new System.Drawing.Point(483, 181);
            this.txtOK.Name = "txtOK";
            this.txtOK.Size = new System.Drawing.Size(75, 23);
            this.txtOK.TabIndex = 5;
            this.txtOK.Text = "确定";
            this.txtOK.UseVisualStyleBackColor = true;
            this.txtOK.Click += new System.EventHandler(this.txtOK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "名称*";
            // 
            // txtProductName
            // 
            this.txtProductName.Location = new System.Drawing.Point(109, 37);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(450, 21);
            this.txtProductName.TabIndex = 1;
            // 
            // ProductDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 218);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtProductName);
            this.Controls.Add(this.txtOK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtTargetPrice);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtURL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProductDialog";
            this.Text = "添加新产品";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtURL;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTargetPrice;
        private System.Windows.Forms.Button txtOK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtProductName;
    }
}