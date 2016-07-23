namespace Nzl.Web.Forms.ProductClawer
{
    partial class ProductClawerFom
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductClawerFom));
            this.btnTriger = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvPrice = new System.Windows.Forms.DataGridView();
            this.Vendor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProductName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.URL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAction = new System.Windows.Forms.DataGridViewButtonColumn();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblSchedulerInfor = new System.Windows.Forms.Label();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.btnClearQueue = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrice)).BeginInit();
            this.pnlContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnTriger
            // 
            this.btnTriger.Location = new System.Drawing.Point(1041, 514);
            this.btnTriger.Name = "btnTriger";
            this.btnTriger.Size = new System.Drawing.Size(75, 23);
            this.btnTriger.TabIndex = 1;
            this.btnTriger.Text = "Start?";
            this.btnTriger.UseVisualStyleBackColor = true;
            this.btnTriger.Click += new System.EventHandler(this.btnTriger_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(847, 514);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(91, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add a Product";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvPrice
            // 
            this.dgvPrice.AllowUserToAddRows = false;
            this.dgvPrice.AllowUserToDeleteRows = false;
            this.dgvPrice.AllowUserToResizeColumns = false;
            this.dgvPrice.AllowUserToResizeRows = false;
            this.dgvPrice.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvPrice.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPrice.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPrice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Vendor,
            this.ProductName,
            this.URL,
            this.Title,
            this.Price,
            this.Stock,
            this.colAction});
            this.dgvPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPrice.Location = new System.Drawing.Point(0, 0);
            this.dgvPrice.MultiSelect = false;
            this.dgvPrice.Name = "dgvPrice";
            this.dgvPrice.ReadOnly = true;
            this.dgvPrice.RowHeadersVisible = false;
            this.dgvPrice.RowTemplate.Height = 23;
            this.dgvPrice.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPrice.Size = new System.Drawing.Size(1103, 478);
            this.dgvPrice.TabIndex = 5;
            this.dgvPrice.TabStop = false;
            this.dgvPrice.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPrice_CellContentClick);
            // 
            // Vendor
            // 
            this.Vendor.DataPropertyName = "Vendor";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.Vendor.DefaultCellStyle = dataGridViewCellStyle2;
            this.Vendor.Frozen = true;
            this.Vendor.HeaderText = "网站";
            this.Vendor.Name = "Vendor";
            this.Vendor.ReadOnly = true;
            // 
            // ProductName
            // 
            this.ProductName.DataPropertyName = "ProductName";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.ProductName.DefaultCellStyle = dataGridViewCellStyle3;
            this.ProductName.Frozen = true;
            this.ProductName.HeaderText = "名称";
            this.ProductName.Name = "ProductName";
            this.ProductName.ReadOnly = true;
            // 
            // URL
            // 
            this.URL.DataPropertyName = "URL";
            this.URL.Frozen = true;
            this.URL.HeaderText = "URL";
            this.URL.Name = "URL";
            this.URL.ReadOnly = true;
            this.URL.Visible = false;
            // 
            // Title
            // 
            this.Title.DataPropertyName = "Title";
            this.Title.Frozen = true;
            this.Title.HeaderText = "产品名称";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            this.Title.Width = 582;
            // 
            // Price
            // 
            this.Price.DataPropertyName = "Price";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            dataGridViewCellStyle4.NullValue = null;
            this.Price.DefaultCellStyle = dataGridViewCellStyle4;
            this.Price.Frozen = true;
            this.Price.HeaderText = "价格";
            this.Price.Name = "Price";
            this.Price.ReadOnly = true;
            // 
            // Stock
            // 
            this.Stock.DataPropertyName = "Stock";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Stock.DefaultCellStyle = dataGridViewCellStyle5;
            this.Stock.Frozen = true;
            this.Stock.HeaderText = "是否有货";
            this.Stock.Name = "Stock";
            this.Stock.ReadOnly = true;
            // 
            // colAction
            // 
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.NullValue = "去下单";
            dataGridViewCellStyle6.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.colAction.DefaultCellStyle = dataGridViewCellStyle6;
            this.colAction.Frozen = true;
            this.colAction.HeaderText = "操作";
            this.colAction.Name = "colAction";
            this.colAction.ReadOnly = true;
            this.colAction.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.colAction.ToolTipText = "去下单";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(10, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(29, 12);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "状态";
            // 
            // lblSchedulerInfor
            // 
            this.lblSchedulerInfor.AutoSize = true;
            this.lblSchedulerInfor.Location = new System.Drawing.Point(10, 519);
            this.lblSchedulerInfor.Name = "lblSchedulerInfor";
            this.lblSchedulerInfor.Size = new System.Drawing.Size(29, 12);
            this.lblSchedulerInfor.TabIndex = 7;
            this.lblSchedulerInfor.Text = "状态";
            // 
            // pnlContainer
            // 
            this.pnlContainer.AutoScroll = true;
            this.pnlContainer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlContainer.Controls.Add(this.dgvPrice);
            this.pnlContainer.Location = new System.Drawing.Point(12, 28);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(1105, 480);
            this.pnlContainer.TabIndex = 8;
            // 
            // btnClearQueue
            // 
            this.btnClearQueue.Location = new System.Drawing.Point(944, 514);
            this.btnClearQueue.Name = "btnClearQueue";
            this.btnClearQueue.Size = new System.Drawing.Size(91, 23);
            this.btnClearQueue.TabIndex = 2;
            this.btnClearQueue.Text = "Clear Queue";
            this.btnClearQueue.UseVisualStyleBackColor = true;
            this.btnClearQueue.Click += new System.EventHandler(this.btnClearQueue_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(1042, 1);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // ProductClawerFom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 544);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnClearQueue);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.lblSchedulerInfor);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnTriger);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ProductClawerFom";
            this.Text = "The Product Clawer Form";
            ((System.ComponentModel.ISupportInitialize)(this.dgvPrice)).EndInit();
            this.pnlContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTriger;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvPrice;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblSchedulerInfor;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.Button btnClearQueue;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vendor;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProductName;
        private System.Windows.Forms.DataGridViewTextBoxColumn URL;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Price;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stock;
        private System.Windows.Forms.DataGridViewButtonColumn colAction;
        private System.Windows.Forms.Button btnReset;
    }
}