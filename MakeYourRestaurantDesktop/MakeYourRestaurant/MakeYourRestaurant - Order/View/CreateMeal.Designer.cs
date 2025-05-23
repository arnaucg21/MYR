
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
            this.panelLeft = new System.Windows.Forms.Panel();
            this.radioBuffet = new System.Windows.Forms.RadioButton();
            this.radioFull = new System.Windows.Forms.RadioButton();
            this.btnCreateWorkers = new System.Windows.Forms.Button();
            this.btnDrinks = new System.Windows.Forms.Button();
            this.panelRight = new System.Windows.Forms.Panel();
            this.lblMenuTitle = new System.Windows.Forms.Label();
            this.btn_create_meals = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewOrders = new System.Windows.Forms.DataGridView();
            this.btn_create_meal = new System.Windows.Forms.Button();
            this.btn_edit_meal = new System.Windows.Forms.Button();
            this.btn_delete_meal = new System.Windows.Forms.Button();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.BackColor = System.Drawing.Color.LightGray;
            this.panelLeft.Controls.Add(this.label1);
            this.panelLeft.Controls.Add(this.btn_create_meals);
            this.panelLeft.Controls.Add(this.radioBuffet);
            this.panelLeft.Controls.Add(this.radioDaily);
            this.panelLeft.Controls.Add(this.radioFull);
            this.panelLeft.Controls.Add(this.btnCreateWorkers);
            this.panelLeft.Controls.Add(this.btnDrinks);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(250, 700);
            this.panelLeft.TabIndex = 0;
            // 
            // radioBuffet
            // 
            this.radioBuffet.AutoSize = true;
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
            this.btnCreateWorkers.Location = new System.Drawing.Point(20, 179);
            this.btnCreateWorkers.Name = "btnCreateWorkers";
            this.btnCreateWorkers.Size = new System.Drawing.Size(200, 30);
            this.btnCreateWorkers.TabIndex = 3;
            this.btnCreateWorkers.Text = "Create workers";
            this.btnCreateWorkers.UseVisualStyleBackColor = true;
            // 
            // btnDrinks
            // 
            this.btnDrinks.Location = new System.Drawing.Point(20, 219);
            this.btnDrinks.Name = "btnDrinks";
            this.btnDrinks.Size = new System.Drawing.Size(200, 30);
            this.btnDrinks.TabIndex = 4;
            this.btnDrinks.Text = "Restaurant demand";
            this.btnDrinks.UseVisualStyleBackColor = true;
            // 
            // panelRight
            // 
            this.panelRight.BackColor = System.Drawing.Color.White;
            this.panelRight.Controls.Add(this.btn_delete_meal);
            this.panelRight.Controls.Add(this.btn_edit_meal);
            this.panelRight.Controls.Add(this.btn_create_meal);
            this.panelRight.Controls.Add(this.dataGridViewOrders);
            this.panelRight.Controls.Add(this.lblMenuTitle);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(250, 0);
            this.panelRight.Name = "panelRight";
            this.panelRight.Size = new System.Drawing.Size(734, 700);
            this.panelRight.TabIndex = 1;
            // 
            // lblMenuTitle
            // 
            this.lblMenuTitle.AutoSize = true;
            this.lblMenuTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblMenuTitle.Location = new System.Drawing.Point(20, 20);
            this.lblMenuTitle.Name = "lblMenuTitle";
            this.lblMenuTitle.Size = new System.Drawing.Size(144, 30);
            this.lblMenuTitle.TabIndex = 0;
            this.lblMenuTitle.Text = "YOUR MENU";
            // 
            // btn_create_meals
            // 
            this.btn_create_meals.Location = new System.Drawing.Point(20, 143);
            this.btn_create_meals.Name = "btn_create_meals";
            this.btn_create_meals.Size = new System.Drawing.Size(200, 30);
            this.btn_create_meals.TabIndex = 5;
            this.btn_create_meals.Text = "Create Meals";
            this.btn_create_meals.UseVisualStyleBackColor = true;
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
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dataGridViewOrders
            // 
            this.dataGridViewOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewOrders.Cursor = System.Windows.Forms.Cursors.Default;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(90)))), ((int)(((byte)(41)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewOrders.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridViewOrders.Location = new System.Drawing.Point(25, 112);
            this.dataGridViewOrders.MultiSelect = false;
            this.dataGridViewOrders.Name = "dataGridViewOrders";
            this.dataGridViewOrders.ReadOnly = true;
            this.dataGridViewOrders.RowHeadersWidth = 51;
            this.dataGridViewOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewOrders.Size = new System.Drawing.Size(697, 576);
            this.dataGridViewOrders.TabIndex = 1;
            // 
            // btn_create_meal
            // 
            this.btn_create_meal.Location = new System.Drawing.Point(80, 69);
            this.btn_create_meal.Name = "btn_create_meal";
            this.btn_create_meal.Size = new System.Drawing.Size(116, 30);
            this.btn_create_meal.TabIndex = 6;
            this.btn_create_meal.Text = "Create Meals";
            this.btn_create_meal.UseVisualStyleBackColor = true;
            // 
            // btn_edit_meal
            // 
            this.btn_edit_meal.Location = new System.Drawing.Point(315, 69);
            this.btn_edit_meal.Name = "btn_edit_meal";
            this.btn_edit_meal.Size = new System.Drawing.Size(129, 30);
            this.btn_edit_meal.TabIndex = 7;
            this.btn_edit_meal.Text = "Edit Meal";
            this.btn_edit_meal.UseVisualStyleBackColor = true;
            // 
            // btn_delete_meal
            // 
            this.btn_delete_meal.Location = new System.Drawing.Point(544, 69);
            this.btn_delete_meal.Name = "btn_delete_meal";
            this.btn_delete_meal.Size = new System.Drawing.Size(112, 30);
            this.btn_delete_meal.TabIndex = 8;
            this.btn_delete_meal.Text = "Remove Meal";
            this.btn_delete_meal.UseVisualStyleBackColor = true;
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 700);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelLeft);
            this.Name = "MainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Page";
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewOrders)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.RadioButton radioBuffet;
        private System.Windows.Forms.RadioButton radioDaily;
        private System.Windows.Forms.RadioButton radioFull;
        private System.Windows.Forms.Button btnCreateWorkers;
        private System.Windows.Forms.Button btnDrinks;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblMenuTitle;
        private System.Windows.Forms.Button btn_create_meals;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_delete_meal;
        private System.Windows.Forms.Button btn_edit_meal;
        private System.Windows.Forms.Button btn_create_meal;
        public System.Windows.Forms.DataGridView dataGridViewOrders;
    }
}
