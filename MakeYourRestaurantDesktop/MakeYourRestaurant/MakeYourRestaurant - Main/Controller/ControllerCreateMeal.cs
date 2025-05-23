using MakeYourRestaurant.View;
using MakeYourRestaurant___Main.Model;   // MealDTO
using MakeYourRestaurant___Main.View;
using MakeYourRestaurant___Received.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MakeYourRestaurant___Main.Controller
{
    public class ControllerCreateMeal
    {
        private readonly CreateMeal view;
        private readonly Repository repo;
        private readonly int restaurantId;

        private List<MealDTO> currentMeals;

        public ControllerCreateMeal(CreateMeal form, int? restId)
        {
            view = form;
            restaurantId = restId ?? 0;
            repo = new Repository();

            InitListeners();
            LoadMeals();
        }

        private void InitListeners()
        {
            view.btn_create_meal.Click += BtnCreate_Click;
            view.btn_edit_meal.Click += BtnEdit_Click;
            view.btn_delete_meal.Click += BtnDelete_Click;
        }

        private void LoadMeals()
        {
            currentMeals = repo.GetAllMeals(restaurantId);

            view.dgv_meals.DataSource = null;
            view.dgv_meals.DataSource = currentMeals;

            // Hide DTO‐internal fields
            view.dgv_meals.Columns["ID"].Visible = false;
            view.dgv_meals.Columns["RestaurantId"].Visible = false;
            view.dgv_meals.Columns["CategoryId"].Visible = false;
            view.dgv_meals.Columns["PhotoFile"].Visible = false;

            // Rename columns
            view.dgv_meals.Columns["Name"].HeaderText = "Meal";
            view.dgv_meals.Columns["Price"].HeaderText = "Price";
            view.dgv_meals.Columns["Details"].HeaderText = "Description";
            view.dgv_meals.Columns["BestFood"].HeaderText = "Best?";
            view.dgv_meals.Columns["MenuType"].HeaderText = "Menu Type";
        }

        private void BtnCreate_Click(object sender, EventArgs e)
        {
            var form = new AddMeal();
            form.tb_restaurantID.Text = restaurantId.ToString();
            form.tb_restaurantID.ReadOnly = true;

            // Populate the checked‐list when the dialog opens
            form.clb_menutype.Items.Clear();
            form.clb_menutype.Items.AddRange(new[] { "Buffet", "DailyMenu", "FullMenu" });

            form.btn_accept.Click += (s, args) =>
            {
                if (!double.TryParse(form.tb_price.Text?.Trim(), out var priceValue))
                    priceValue = 0;

                var dto = new MealDTO
                {
                    RestaurantId = restaurantId,
                    Name = form.tb_name.Text,
                    Details = form.tb_details.Text,
                    BestFood = form.cb_best_food.Checked,
                    PhotoFile = form.tb_photo_route.Text,
                    Price = priceValue,
                    MenuType = string.Join(",",
                        form.clb_menutype.CheckedItems
                            .OfType<string>()
                            .Select(z => z.Trim()))
                };

                var created = repo.CreateMeal(dto);
                if (created != null)
                {
                    LoadMeals();
                    form.Close();
                }
                else
                {
                    MessageBox.Show("Failed to create meal.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            form.btn_cancel.Click += (_, __) => form.Close();
            form.ShowDialog();
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (view.dgv_meals.SelectedRows.Count == 0) return;
            var selected = view.dgv_meals.SelectedRows[0].DataBoundItem as MealDTO;
            if (selected == null) return;

            using (var form = new AddMeal())
            {
                // Auto‐fill & lock Restaurant ID
                form.tb_restaurantID.Text = restaurantId.ToString();
                form.tb_restaurantID.ReadOnly = true;

                // Prefill fields
                form.tb_name.Text = selected.Name;
                form.tb_details.Text = selected.Details;
                form.cb_best_food.Checked = selected.BestFood;
                form.tb_photo_route.Text = selected.PhotoFile;
                form.tb_price.Text = selected.Price?.ToString("0.00");

                // Populate and pre‐check the menu‐types
                form.clb_menutype.Items.Clear();
                var types = new[] { "Buffet", "DailyMenu", "FullMenu" };
                form.clb_menutype.Items.AddRange(types);
                var existing = (selected.MenuType ?? "")
                               .Split(',')
                               .Select(x => x.Trim())
                               .ToHashSet(StringComparer.OrdinalIgnoreCase);
                for (int i = 0; i < types.Length; i++)
                {
                    if (existing.Contains(types[i]))
                        form.clb_menutype.SetItemChecked(i, true);
                }

                // DialogResult wiring
                form.btn_accept.DialogResult = DialogResult.OK;
                form.AcceptButton = form.btn_accept;
                form.btn_cancel.DialogResult = DialogResult.Cancel;
                form.CancelButton = form.btn_cancel;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // Build updated DTO

                    var dto = new MealDTO
                    {
                        RestaurantId = restaurantId,
                        Name = form.tb_name.Text,
                        Details = form.tb_details.Text,
                        BestFood = form.cb_best_food.Checked,
                        PhotoFile = form.tb_photo_route.Text,
                        Price = double.Parse(form.tb_price.Text),
                        MenuType = string.Join(",",
                            form.clb_menutype.CheckedItems
                                .OfType<string>()
                                .Select(z => z.Trim()))
                    };

                    var updated = repo.UpdateMeal(selected.ID, dto);
                    LoadMeals();
                    form.Close();
                }
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (view.dgv_meals.SelectedRows.Count == 0) return;
            var selected = view.dgv_meals.SelectedRows[0].DataBoundItem as MealDTO;
            if (selected == null) return;

            if (MessageBox.Show(
                    $"Delete {selected.Name}?",
                    "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                repo.DeleteMeal(selected.ID);
                LoadMeals();
            }
        }

        private int? TryParseInt(string txt)
        {
            return int.TryParse(txt, out var n) ? (int?)n : null;
        }
    }
}
