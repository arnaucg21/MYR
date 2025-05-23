
namespace MakeYourRestaurant.View
{
    partial class CreateMeal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateMeal));
            this.panelRight = new System.Windows.Forms.Panel();
            this.btn_delete_meal = new System.Windows.Forms.Button();
            this.btn_edit_meal = new System.Windows.Forms.Button();
            this.btn_create_meal = new System.Windows.Forms.Button();
            this.dgv_meals = new System.Windows.Forms.DataGridView();
            this.lblMenuTitle = new System.Windows.Forms.Label();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_meals)).BeginInit();
            this.SuspendLayout();
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(201)))), ((int)(((byte)(157)))));
            this.panelRight.Controls.Add(this.btn_delete_meal);
            this.panelRight.Controls.Add(this.btn_edit_meal);
            this.panelRight.Controls.Add(this.btn_create_meal);
            this.panelRight.Controls.Add(this.dgv_meals);
            this.panelRight.Controls.Add(this.lblMenuTitle);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(0, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(737, 700);
            this.panelRight.TabIndex = 1;
            // 
            // btn_delete_meal
            // 
            this.btn_delete_meal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.btn_delete_meal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_delete_meal.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_delete_meal.Location = new System.Drawing.Point(585, 69);
            this.btn_delete_meal.Name = "btn_delete_meal";
            this.btn_delete_meal.Size = new System.Drawing.Size(137, 30);
            this.btn_delete_meal.TabIndex = 8;
            this.btn_delete_meal.Text = "Delete";
            this.btn_delete_meal.UseVisualStyleBackColor = false;
            // 
            // btn_edit_meal
            // 
            this.btn_edit_meal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.btn_edit_meal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_edit_meal.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_edit_meal.Location = new System.Drawing.Point(317, 69);
            this.btn_edit_meal.Name = "btn_edit_meal";
            this.btn_edit_meal.Size = new System.Drawing.Size(137, 30);
            this.btn_edit_meal.TabIndex = 7;
            this.btn_edit_meal.Text = "Edit";
            this.btn_edit_meal.UseVisualStyleBackColor = false;
            // 
            // btn_create_meal
            // 
            this.btn_create_meal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.btn_create_meal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_create_meal.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_create_meal.Location = new System.Drawing.Point(18, 69);
            this.btn_create_meal.Name = "btn_create_meal";
            this.btn_create_meal.Size = new System.Drawing.Size(137, 30);
            this.btn_create_meal.TabIndex = 6;
            this.btn_create_meal.Text = "Create";
            this.btn_create_meal.UseVisualStyleBackColor = false;
            // 
            // dgv_meals
            // 
            this.dgv_meals.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.dgv_meals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_meals.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(90)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_meals.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_meals.Location = new System.Drawing.Point(18, 112);
            this.dgv_meals.MultiSelect = false;
            this.dgv_meals.Name = "dgv_meals";
            this.dgv_meals.ReadOnly = true;
            this.dgv_meals.RowHeadersWidth = 51;
            this.dgv_meals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_meals.Size = new System.Drawing.Size(704, 576);
            this.dgv_meals.TabIndex = 1;
            // 
            // lblMenuTitle
            // 
            this.lblMenuTitle.AutoSize = true;
            this.lblMenuTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblMenuTitle.Location = new System.Drawing.Point(342, 21);
            this.lblMenuTitle.Name = "lblMenuTitle";
            this.lblMenuTitle.Size = new System.Drawing.Size(84, 30);
            this.lblMenuTitle.TabIndex = 0;
            this.lblMenuTitle.Text = "MEALS";
            this.lblMenuTitle.Click += new System.EventHandler(this.lblMenuTitle_Click);
            // 
            // CreateMeal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(737, 700);
            this.Controls.Add(this.panelRight);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateMeal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Page";
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_meals)).EndInit();
            this.ResumeLayout(false);

        }
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblMenuTitle;
        public System.Windows.Forms.DataGridView dgv_meals;
        public System.Windows.Forms.Button btn_create_meal;
        public System.Windows.Forms.Button btn_delete_meal;
        public System.Windows.Forms.Button btn_edit_meal;
    }
}
