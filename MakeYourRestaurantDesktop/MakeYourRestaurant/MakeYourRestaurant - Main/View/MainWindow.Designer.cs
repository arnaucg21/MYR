namespace MakeYourRestaurant___Received.View
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.panelLeft = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_create_meals = new System.Windows.Forms.Button();
            this.radioBuffet = new System.Windows.Forms.RadioButton();
            this.radioDaily = new System.Windows.Forms.RadioButton();
            this.radioFull = new System.Windows.Forms.RadioButton();
            this.btnCreateWorkers = new System.Windows.Forms.Button();
            this.dgvCategories = new System.Windows.Forms.DataGridView();
            this.lblMenuTitle = new System.Windows.Forms.Label();
            this.dgvMealsCategory = new System.Windows.Forms.DataGridView();
            this.btn_add_cat = new System.Windows.Forms.Button();
            this.btn_edit_cat = new System.Windows.Forms.Button();
            this.btn_delete_cat = new System.Windows.Forms.Button();
            this.btn_generate_qr = new System.Windows.Forms.Button();
            this.btn_delete_meal = new System.Windows.Forms.Button();
            this.btn_add_meal = new System.Windows.Forms.Button();
            this.dgvMeals = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panelLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMealsCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeals)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.panelLeft.Controls.Add(this.label1);
            this.panelLeft.Controls.Add(this.btn_create_meals);
            this.panelLeft.Controls.Add(this.radioBuffet);
            this.panelLeft.Controls.Add(this.radioDaily);
            this.panelLeft.Controls.Add(this.radioFull);
            this.panelLeft.Controls.Add(this.btnCreateWorkers);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(250, 604);
            this.panelLeft.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.label1.Location = new System.Drawing.Point(15, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Menu types";
            // 
            // btn_create_meals
            // 
            this.btn_create_meals.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(201)))), ((int)(((byte)(157)))));
            this.btn_create_meals.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_create_meals.Location = new System.Drawing.Point(20, 143);
            this.btn_create_meals.Name = "btn_create_meals";
            this.btn_create_meals.Size = new System.Drawing.Size(200, 30);
            this.btn_create_meals.TabIndex = 5;
            this.btn_create_meals.Text = "Create Meals";
            this.btn_create_meals.UseVisualStyleBackColor = false;
            // 
            // radioBuffet
            // 
            this.radioBuffet.AutoSize = true;
            this.radioBuffet.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.radioBuffet.Location = new System.Drawing.Point(38, 52);
            this.radioBuffet.Name = "radioBuffet";
            this.radioBuffet.Size = new System.Drawing.Size(53, 17);
            this.radioBuffet.TabIndex = 0;
            this.radioBuffet.TabStop = true;
            this.radioBuffet.Text = "Buffet";
            this.radioBuffet.UseVisualStyleBackColor = true;
            // 
            // radioDaily
            // 
            this.radioDaily.AutoSize = true;
            this.radioDaily.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.radioDaily.Location = new System.Drawing.Point(38, 82);
            this.radioDaily.Name = "radioDaily";
            this.radioDaily.Size = new System.Drawing.Size(77, 17);
            this.radioDaily.TabIndex = 1;
            this.radioDaily.TabStop = true;
            this.radioDaily.Text = "Daily menu";
            this.radioDaily.UseVisualStyleBackColor = true;
            // 
            // radioFull
            // 
            this.radioFull.AutoSize = true;
            this.radioFull.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.radioFull.Location = new System.Drawing.Point(38, 112);
            this.radioFull.Name = "radioFull";
            this.radioFull.Size = new System.Drawing.Size(70, 17);
            this.radioFull.TabIndex = 2;
            this.radioFull.TabStop = true;
            this.radioFull.Text = "Full menu";
            this.radioFull.UseVisualStyleBackColor = true;
            // 
            // btnCreateWorkers
            // 
            this.btnCreateWorkers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(201)))), ((int)(((byte)(157)))));
            this.btnCreateWorkers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateWorkers.Location = new System.Drawing.Point(20, 179);
            this.btnCreateWorkers.Name = "btnCreateWorkers";
            this.btnCreateWorkers.Size = new System.Drawing.Size(200, 30);
            this.btnCreateWorkers.TabIndex = 3;
            this.btnCreateWorkers.Text = "Create workers";
            this.btnCreateWorkers.UseVisualStyleBackColor = false;
            // 
            // dgvCategories
            // 
            this.dgvCategories.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.dgvCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCategories.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(90)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCategories.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCategories.Location = new System.Drawing.Point(306, 112);
            this.dgvCategories.MultiSelect = false;
            this.dgvCategories.Name = "dgvCategories";
            this.dgvCategories.ReadOnly = true;
            this.dgvCategories.RowHeadersWidth = 51;
            this.dgvCategories.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCategories.Size = new System.Drawing.Size(307, 320);
            this.dgvCategories.TabIndex = 2;
            // 
            // lblMenuTitle
            // 
            this.lblMenuTitle.AutoSize = true;
            this.lblMenuTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblMenuTitle.Location = new System.Drawing.Point(301, 39);
            this.lblMenuTitle.Name = "lblMenuTitle";
            this.lblMenuTitle.Size = new System.Drawing.Size(232, 30);
            this.lblMenuTitle.TabIndex = 3;
            this.lblMenuTitle.Text = "Generate your menu!";
            // 
            // dgvMealsCategory
            // 
            this.dgvMealsCategory.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.dgvMealsCategory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMealsCategory.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(90)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMealsCategory.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvMealsCategory.Location = new System.Drawing.Point(698, 112);
            this.dgvMealsCategory.MultiSelect = false;
            this.dgvMealsCategory.Name = "dgvMealsCategory";
            this.dgvMealsCategory.ReadOnly = true;
            this.dgvMealsCategory.RowHeadersWidth = 51;
            this.dgvMealsCategory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMealsCategory.Size = new System.Drawing.Size(332, 320);
            this.dgvMealsCategory.TabIndex = 4;
            // 
            // btn_add_cat
            // 
            this.btn_add_cat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.btn_add_cat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_add_cat.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_add_cat.Location = new System.Drawing.Point(306, 457);
            this.btn_add_cat.Name = "btn_add_cat";
            this.btn_add_cat.Size = new System.Drawing.Size(88, 45);
            this.btn_add_cat.TabIndex = 5;
            this.btn_add_cat.Text = "Add Cat";
            this.btn_add_cat.UseVisualStyleBackColor = false;
            // 
            // btn_edit_cat
            // 
            this.btn_edit_cat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.btn_edit_cat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_edit_cat.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_edit_cat.Location = new System.Drawing.Point(415, 457);
            this.btn_edit_cat.Name = "btn_edit_cat";
            this.btn_edit_cat.Size = new System.Drawing.Size(88, 45);
            this.btn_edit_cat.TabIndex = 6;
            this.btn_edit_cat.Text = "Edit Cat";
            this.btn_edit_cat.UseVisualStyleBackColor = false;
            // 
            // btn_delete_cat
            // 
            this.btn_delete_cat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.btn_delete_cat.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_delete_cat.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_delete_cat.Location = new System.Drawing.Point(525, 457);
            this.btn_delete_cat.Name = "btn_delete_cat";
            this.btn_delete_cat.Size = new System.Drawing.Size(88, 45);
            this.btn_delete_cat.TabIndex = 7;
            this.btn_delete_cat.Text = "Delete Cat";
            this.btn_delete_cat.UseVisualStyleBackColor = false;
            // 
            // btn_generate_qr
            // 
            this.btn_generate_qr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.btn_generate_qr.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_generate_qr.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_generate_qr.Location = new System.Drawing.Point(306, 547);
            this.btn_generate_qr.Name = "btn_generate_qr";
            this.btn_generate_qr.Size = new System.Drawing.Size(88, 45);
            this.btn_generate_qr.TabIndex = 10;
            this.btn_generate_qr.Text = "Generate QR";
            this.btn_generate_qr.UseVisualStyleBackColor = false;
            // 
            // btn_delete_meal
            // 
            this.btn_delete_meal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.btn_delete_meal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_delete_meal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_delete_meal.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_delete_meal.Location = new System.Drawing.Point(942, 447);
            this.btn_delete_meal.Name = "btn_delete_meal";
            this.btn_delete_meal.Size = new System.Drawing.Size(88, 45);
            this.btn_delete_meal.TabIndex = 9;
            this.btn_delete_meal.Text = "Unassign Meal";
            this.btn_delete_meal.UseVisualStyleBackColor = false;
            // 
            // btn_add_meal
            // 
            this.btn_add_meal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.btn_add_meal.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btn_add_meal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_add_meal.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btn_add_meal.Location = new System.Drawing.Point(1036, 447);
            this.btn_add_meal.Name = "btn_add_meal";
            this.btn_add_meal.Size = new System.Drawing.Size(88, 45);
            this.btn_add_meal.TabIndex = 8;
            this.btn_add_meal.Text = "Assign Meal";
            this.btn_add_meal.UseVisualStyleBackColor = false;
            // 
            // dgvMeals
            // 
            this.dgvMeals.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(90)))), ((int)(((byte)(44)))));
            this.dgvMeals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMeals.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(90)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMeals.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvMeals.Location = new System.Drawing.Point(1036, 112);
            this.dgvMeals.MultiSelect = false;
            this.dgvMeals.Name = "dgvMeals";
            this.dgvMeals.ReadOnly = true;
            this.dgvMeals.RowHeadersWidth = 51;
            this.dgvMeals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMeals.Size = new System.Drawing.Size(332, 320);
            this.dgvMeals.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(419, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 21);
            this.label2.TabIndex = 12;
            this.label2.Text = "Categories";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(806, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 21);
            this.label3.TabIndex = 13;
            this.label3.Text = "Meals in category";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1188, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 21);
            this.label4.TabIndex = 14;
            this.label4.Text = "Meals";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(201)))), ((int)(((byte)(157)))));
            this.ClientSize = new System.Drawing.Size(1380, 604);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvMeals);
            this.Controls.Add(this.btn_generate_qr);
            this.Controls.Add(this.btn_delete_meal);
            this.Controls.Add(this.btn_add_meal);
            this.Controls.Add(this.btn_delete_cat);
            this.Controls.Add(this.btn_edit_cat);
            this.Controls.Add(this.btn_add_cat);
            this.Controls.Add(this.dgvMealsCategory);
            this.Controls.Add(this.lblMenuTitle);
            this.Controls.Add(this.dgvCategories);
            this.Controls.Add(this.panelLeft);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "MakeYourRestaurant - Received Orders";
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCategories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMealsCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMeals)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btn_create_meals;
        public System.Windows.Forms.RadioButton radioBuffet;
        public System.Windows.Forms.RadioButton radioDaily;
        public System.Windows.Forms.RadioButton radioFull;
        public System.Windows.Forms.Button btnCreateWorkers;
        public System.Windows.Forms.DataGridView dgvCategories;
        private System.Windows.Forms.Label lblMenuTitle;
        public System.Windows.Forms.DataGridView dgvMealsCategory;
        public System.Windows.Forms.Button btn_add_cat;
        public System.Windows.Forms.Button btn_edit_cat;
        public System.Windows.Forms.Button btn_delete_cat;
        public System.Windows.Forms.Button btn_generate_qr;
        public System.Windows.Forms.Button btn_delete_meal;
        public System.Windows.Forms.Button btn_add_meal;
        public System.Windows.Forms.DataGridView dgvMeals;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

