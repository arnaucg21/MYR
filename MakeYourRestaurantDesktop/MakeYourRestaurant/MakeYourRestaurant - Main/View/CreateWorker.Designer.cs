namespace MakeYourRestaurant___Order.View
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelRight = new System.Windows.Forms.Panel();
            this.btn_delete_worker = new System.Windows.Forms.Button();
            this.btn_edit_worker = new System.Windows.Forms.Button();
            this.btn_create_worker = new System.Windows.Forms.Button();
            this.dgv_WORKERS = new System.Windows.Forms.DataGridView();
            this.lblMenuTitle = new System.Windows.Forms.Label();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_WORKERS)).BeginInit();
            this.SuspendLayout();
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(201)))), ((int)(((byte)(157)))));
            this.panelRight.Controls.Add(this.btn_delete_worker);
            this.panelRight.Controls.Add(this.btn_edit_worker);
            this.panelRight.Controls.Add(this.btn_create_worker);
            this.panelRight.Controls.Add(this.dgv_WORKERS);
            this.panelRight.Controls.Add(this.lblMenuTitle);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(742, 637);
            this.panelRight.TabIndex = 2;
            // 
            // btn_delete_worker
            // 
            this.btn_delete_worker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.btn_delete_worker.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_delete_worker.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_delete_worker.Location = new System.Drawing.Point(585, 69);
            this.btn_delete_worker.Name = "btn_delete_worker";
            this.btn_delete_worker.Size = new System.Drawing.Size(137, 30);
            this.btn_delete_worker.TabIndex = 8;
            this.btn_delete_worker.Text = "Delete";
            this.btn_delete_worker.UseVisualStyleBackColor = false;
            // 
            // btn_edit_worker
            // 
            this.btn_edit_worker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.btn_edit_worker.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_edit_worker.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_edit_worker.Location = new System.Drawing.Point(318, 69);
            this.btn_edit_worker.Name = "btn_edit_worker";
            this.btn_edit_worker.Size = new System.Drawing.Size(137, 30);
            this.btn_edit_worker.TabIndex = 7;
            this.btn_edit_worker.Text = "Edit";
            this.btn_edit_worker.UseVisualStyleBackColor = false;
            // 
            // btn_create_worker
            // 
            this.btn_create_worker.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.btn_create_worker.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_create_worker.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_create_worker.Location = new System.Drawing.Point(18, 69);
            this.btn_create_worker.Name = "btn_create_worker";
            this.btn_create_worker.Size = new System.Drawing.Size(137, 30);
            this.btn_create_worker.TabIndex = 6;
            this.btn_create_worker.Text = "Create";
            this.btn_create_worker.UseVisualStyleBackColor = false;
            // 
            // dgv_WORKERS
            // 
            this.dgv_WORKERS.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.dgv_WORKERS.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_WORKERS.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(90)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_WORKERS.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_WORKERS.Location = new System.Drawing.Point(18, 112);
            this.dgv_WORKERS.MultiSelect = false;
            this.dgv_WORKERS.Name = "dgv_WORKERS";
            this.dgv_WORKERS.ReadOnly = true;
            this.dgv_WORKERS.RowHeadersWidth = 51;
            this.dgv_WORKERS.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_WORKERS.Size = new System.Drawing.Size(704, 510);
            this.dgv_WORKERS.TabIndex = 1;
            // 
            // lblMenuTitle
            // 
            this.lblMenuTitle.AutoSize = true;
            this.lblMenuTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblMenuTitle.Location = new System.Drawing.Point(328, 23);
            this.lblMenuTitle.Name = "lblMenuTitle";
            this.lblMenuTitle.Size = new System.Drawing.Size(118, 30);
            this.lblMenuTitle.TabIndex = 0;
            this.lblMenuTitle.Text = "WORKERS";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 637);
            this.Controls.Add(this.panelRight);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "MakeYourRestaurant - Order Lines";
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_WORKERS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelRight;
        public System.Windows.Forms.Button btn_delete_worker;
        public System.Windows.Forms.Button btn_edit_worker;
        public System.Windows.Forms.Button btn_create_worker;
        public System.Windows.Forms.DataGridView dgv_WORKERS;
        private System.Windows.Forms.Label lblMenuTitle;
    }
}

