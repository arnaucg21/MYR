using MakeYourRestaurant___Order.Model;
using MakeYourRestaurant___Order.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeYourRestaurant___Order.Controller
{
    public class ControllerOrderlines
    {
        WelcomeWindow f;
        Repository r;

        Timer timer;
        int? restId;

        public ControllerOrderlines(int? idRest)
        {
            restId = idRest;
            f = new WelcomeWindow();
            r = new Repository();
            InitListeners();
            LoadData();
            f.ShowDialog();
        }

        public void InitListeners()
        {
            f.buttonSignOut.Click += ButtonSignOut_Click;
            f.buttonDelete.Click += ButtonDelete_Click;
            f.buttonRefresh.Click += ButtonRefresh_Click;
            f.buttonStatus.Click += ButtonStatus_Click;
            f.dataGridViewOrders.SelectionChanged += DataGridViewOrders_SelectionChanged;


            f.textBoxFilter.Enter += TextBoxFilter_Enter;
            f.textBoxFilter.Leave += TextBoxFilter_Leave;
            f.textBoxFilter.TextChanged += TextBoxFilter_TextChanged;
        }

        private void TextBoxFilter_TextChanged(object sender, EventArgs e)
        {
            string filter = f.textBoxFilter.Text;

            if (filter != "Enter order ID  to search a order.")
            {
                if (!string.IsNullOrEmpty(filter) && !IsPositiveInteger(filter))
                {
                    MessageBox.Show("El valor ingresado no es un número entero positivo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    f.textBoxFilter.Text = "";
                    return;
                }

                if (!string.IsNullOrEmpty(filter))
                {
                    f.dataGridViewOrders.DataSource = r.FilterOrders(restId, int.Parse(filter));
                }

                if (filter.Equals(""))
                {
                    f.dataGridViewOrders.DataSource = r.GetAllOrders(restId);
                }
            }
        }

        private bool IsPositiveInteger(string value)
        {
            int number;
            return int.TryParse(value, out number) && number > 0;
        }

        private void TextBoxFilter_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(f.textBoxFilter.Text))
            {
                f.textBoxFilter.Text = "Enter order ID  to search a order.";
            }
        }


        private void OnTimedEvent(Object source, EventArgs e)
        {
            if (string.IsNullOrEmpty(f.textBoxFilter.Text) || f.textBoxFilter.Text == "" || f.textBoxFilter.Text == "Enter order ID  to search a order.")
            {
                f.dataGridViewOrders.DataSource = null;
                f.dataGridViewLines.DataSource = null;
                LoadData();
            }
            else
            {
                f.dataGridViewOrders.DataSource = null;
                f.dataGridViewLines.DataSource = null;

                f.dataGridViewOrders.DataSource = r.GetAllOrders(restId); // CAMBIAR ID RESTAURANTE
            }

            AutoSizeColumns(f.dataGridViewOrders);
            AutoSizeColumnsLines(f.dataGridViewLines);
        }
        public void StartTimer()
        {
            timer = new Timer();
            timer.Interval = 30000;
            timer.Tick += OnTimedEvent;
            timer.Start();
        }

        private void TextBoxFilter_Enter(object sender, EventArgs e)
        {
            if (f.textBoxFilter.Text.Equals("Enter order ID  to search a order."))
            {
                f.textBoxFilter.Text = "";
            }
        }
        private void ButtonStatus_Click(object sender, EventArgs e)
        {
            if (f.buttonStatus.Text.Equals("Online"))
            {
                f.buttonStatus.Text = "Offline";

                f.textBoxFilter.Enabled = false;
                f.dataGridViewOrders.DataSource = null;
                f.dataGridViewLines.DataSource = null;
                //f.panel3.Controls.Clear();

                f.buttonReport.Enabled = false;
                f.buttonRefresh.Enabled = false;
            }
            else
            {
                f.buttonStatus.Text = "Online";

                f.textBoxFilter.Enabled = true;
                LoadOrders();

                f.buttonReport.Enabled = true;
                f.buttonRefresh.Enabled = true;
            }
        }

        private void ButtonRefresh_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(f.textBoxFilter.Text) || f.textBoxFilter.Text == "" || f.textBoxFilter.Text == "Enter order ID  to search a order.")
            {
                f.dataGridViewOrders.DataSource = null;
                f.dataGridViewLines.DataSource = null;
                LoadData();
            }
            else
            {
                f.dataGridViewOrders.DataSource = null;
                f.dataGridViewLines.DataSource = null;

                f.dataGridViewOrders.DataSource = r.GetAllOrders(1); // CAMBIAR ID RESTAURANTE
            }

            AutoSizeColumns(f.dataGridViewOrders);
            AutoSizeColumnsLines(f.dataGridViewLines);
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (f.dataGridViewOrders.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = f.dataGridViewOrders.SelectedRows[0];
                OrderDTO selectedOrder = selectedRow.DataBoundItem as OrderDTO;

                if (selectedOrder != null)
                {
                    int orderId = selectedOrder.ID;
                    r.DeleteOrder(orderId);

                }
            }
        }

        private void DataGridViewOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (f.dataGridViewOrders.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = f.dataGridViewOrders.SelectedRows[0];
                OrderDTO selectedOrder = selectedRow.DataBoundItem as OrderDTO;

                if (selectedOrder != null)
                {
                    int orderId = selectedOrder.ID;

                    f.dataGridViewLines.DataSource = r.GetOrderLines(orderId);

                    AutoSizeColumnsLines(f.dataGridViewLines);
                }
            }
        }

        private void ButtonSignOut_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public void LoadData()
        {
            LoadOrders();
        }
        public void LoadOrders()
        {
            f.dataGridViewOrders.Dock = DockStyle.Fill;
            f.dataGridViewOrders.DataSource = r.GetAllOrders(1); // CAMBIAR ID RESTAURANTE
            f.dataGridViewOrders.Columns["ID"].HeaderText = "Bill No.";
            f.dataGridViewOrders.Columns["TableNumber"].HeaderText = "Table No.";
            f.dataGridViewOrders.Columns["Date"].HeaderText = "Ordered At";
            f.dataGridViewOrders.Columns["TotalPrice"].HeaderText = "Amount";
            f.dataGridViewOrders.Columns["ClientName"].HeaderText = "Ordered By";

            AutoSizeColumns(f.dataGridViewOrders);
        }

        public void AutoSizeColumns(DataGridView dgv)
        {
            dgv.Columns["ID"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns["TableNumber"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns["Date"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns["TotalPrice"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns["ClientName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
        public void AutoSizeColumnsLines(DataGridView dgv)
        {
            dgv.Columns["MealName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns["MealPrice"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
    }
}
