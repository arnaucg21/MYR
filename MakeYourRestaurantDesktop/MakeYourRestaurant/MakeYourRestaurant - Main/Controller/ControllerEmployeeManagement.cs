using MakeYourRestaurant___Main.Model;
using MakeYourRestaurant___Main.View;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Newtonsoft.Json;
using MakeYourRestaurant___Order.View;

namespace MakeYourRestaurant___Main.Controller
{
    public class ControllerEmployeeManagement
    {
        private readonly Form1 view;
        private readonly Repository repo;
        private readonly int restaurantId;
        private List<EmployeeDTO> currentEmployees;

        public ControllerEmployeeManagement(Form1 form, int? restId)
        {
            view = form;
            restaurantId = restId ?? 0;
            repo = new Repository();

            InitListeners();
            LoadEmployees();
        }

        private void InitListeners()
        {
            view.btn_create_worker.Click += BtnCreate_Click;
            view.btn_edit_worker.Click += BtnEdit_Click;
            view.btn_delete_worker.Click += BtnDelete_Click;
        }

        private void LoadEmployees()
        {
            currentEmployees = repo.GetEmployeesByRestaurant(restaurantId);
            view.dgv_WORKERS.DataSource = null;
            view.dgv_WORKERS.DataSource = currentEmployees;

            // hide wire-internal columns if any
            view.dgv_WORKERS.Columns["Id"].Visible = false;

            view.dgv_WORKERS.Columns["Username"].HeaderText = "Username";
            view.dgv_WORKERS.Columns["Role"].HeaderText = "Role";
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            var form = new AddEmployee();

            // ← auto-fill & lock
            form.tb_restaurantID.Text = restaurantId.ToString();
            form.tb_restaurantID.ReadOnly = true;

            form.cb_role.Items.Clear();
            form.cb_role.Items.AddRange(new[] { "Chef", "Waiter", "Admin" });
            form.cb_role.DropDownStyle = ComboBoxStyle.DropDownList;

            form.btn_accept.Click += (s, args) =>
            {
                // 1) basic validation
                if (string.IsNullOrWhiteSpace(form.tb_username.Text)
                 || string.IsNullOrWhiteSpace(form.tb_password.Text))
                {
                    MessageBox.Show("Username & Password required", "Error",
                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2) build DTO (use controller’s restaurantId, not user input)
                var dto = new EmployeeDTO
                {
                    RestaurantId = restaurantId,
                    Username = form.tb_username.Text,
                    Password = form.tb_password.Text,
                    Role = form.cb_role.Text
                };

                // 3) send to API
                var created = repo.CreateEmployee(dto);
                if (created != null)
                {
                    LoadEmployees();
                    form.Close();
                }
            };

            form.btn_cancel.Click += (_, __) => form.Close();
            form.ShowDialog();
        }


        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (view.dgv_WORKERS.SelectedRows.Count == 0) return;
            var selected = view.dgv_WORKERS.SelectedRows[0].DataBoundItem as EmployeeDTO;
            if (selected == null) return;

            using (var form = new AddEmployee())
            {
                // Auto-fill & lock Restaurant ID
                form.tb_restaurantID.Text = restaurantId.ToString();
                form.tb_restaurantID.ReadOnly = true;

                // Prefill the rest
                form.tb_username.Text = selected.Username;
                form.cb_role.Text = selected.Role;
                // (we leave Password blank for security)

                form.cb_role.Items.Clear();
                form.cb_role.Items.AddRange(new[] { "Chef", "Waiter", "Admin" });
                form.cb_role.DropDownStyle = ComboBoxStyle.DropDownList;

                // Make Accept/Cancel work as DialogResult
                form.btn_accept.DialogResult = DialogResult.OK;
                form.AcceptButton = form.btn_accept;
                form.btn_cancel.DialogResult = DialogResult.Cancel;
                form.CancelButton = form.btn_cancel;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Map changes from the dialog
                    selected.Username = form.tb_username.Text;
                    selected.Role = form.cb_role.Text;
                    if (!string.IsNullOrWhiteSpace(form.tb_password.Text))
                        selected.Password = form.tb_password.Text;

                    // 1) Call the API to persist the update
                    var updated = repo.UpdateEmployee(selected.Id, selected);
                    LoadEmployees();
                }
            }
        }



        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (view.dgv_WORKERS.SelectedRows.Count == 0) return;
            var selected = view.dgv_WORKERS.SelectedRows[0].DataBoundItem as EmployeeDTO;
            if (selected == null) return;

            if (MessageBox.Show(
                   $"Delete {selected.Username}?",
                   "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question
               ) == DialogResult.Yes)
            {
                repo.DeleteEmployee(selected.Id);
                LoadEmployees();
            }
        }
    }
}
