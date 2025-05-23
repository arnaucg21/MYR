// ────────────────────────────────────────────────────────────────
// MakeYourRestaurant___Main/Controller/ControllerMain.cs
// ────────────────────────────────────────────────────────────────
using System;
using System.Linq;
using System.Windows.Forms;
using MakeYourRestaurant.View;
using MakeYourRestaurant___Main.Model;
using MakeYourRestaurant___Main.View;
using MakeYourRestaurant___Order.View;
using MakeYourRestaurant___Received.View;
using Microsoft.VisualBasic; 
using QRCoder;
using System.Drawing;

namespace MakeYourRestaurant___Main.Controller
{
    public class ControllerMain
    {
        private readonly MainWindow view;
        private readonly Repository repo;
        private readonly int restaurantId;

        public ControllerMain(int? idRest)
        {
            restaurantId = idRest ?? 0;
            view = new MainWindow();
            repo = new Repository();

            WireEvents();

            // When the window *actually* appears, do the initial load + select
            view.Shown += (s, e) =>
            {
                LoadCategories();

                if (view.dgvCategories.Rows.Count > 0)
                {
                    // Clear any leftover selection, then pick row 0
                    view.dgvCategories.ClearSelection();
                    view.dgvCategories.Rows[0].Selected = true;

                    // And fire the category‐changed logic
                    OnCategoryChanged(null, EventArgs.Empty);
                }
            };

            view.ShowDialog();
        }


        private void WireEvents()
        {
            // ─── Flow #1: Legacy CreateMeal dialog ─────────────────
            view.btn_create_meals.Click += BtnCreateMeals_Click;

            // ─── Flow #2: Category ←→ Meal assignment UI ────────────
            //   Radio buttons trigger the assignment UI
            view.radioBuffet.CheckedChanged += (_, __) => LoadCategories();
            view.radioDaily.CheckedChanged += (_, __) => LoadCategories();
            view.radioFull.CheckedChanged += (_, __) => LoadCategories();

            //   When a category is selected, show its assigned meals
            view.dgvCategories.SelectionChanged += OnCategoryChanged;

            //   Assign / Unassign buttons
            view.btn_add_meal.Click += (_, __) => AssignMeal();
            view.btn_delete_meal.Click += (_, __) => UnassignMeal();

            // ─── Category CRUD ─────────────────────────────────────
            view.btn_add_cat.Click += (_, __) => EditCategory(null);
            view.btn_edit_cat.Click += (_, __) => EditCategory(view.SelectedCategory());
            view.btn_delete_cat.Click += (_, __) => DeleteCategory();
            view.btn_generate_qr.Click += (_, __) => GenerateQr();
            // ─── Worker management (unchanged) ──────────────────────
            view.btnCreateWorkers.Click += (_, __) =>
            {
                var f = new Form1();
                new ControllerEmployeeManagement(f, restaurantId);
                f.ShowDialog();
            };
        }

        // ────────────── Flow #1 handler ─────────────────────────
        private void BtnCreateMeals_Click(object sender, EventArgs e)
        {
            // Launch your existing CreateMeal view+controller
            var form = new CreateMeal();
            new ControllerCreateMeal(form, restaurantId);
            form.ShowDialog();
        }

        // ────────────── Flow #2 methods ─────────────────────────
        private void LoadCategories()
        {
            // 1) bind categories (only show Name)
            var cats = repo.GetCategories(restaurantId);
            view.dgvCategories.DataSource = cats;
            // hide everything except Name
            foreach (DataGridViewColumn col in view.dgvCategories.Columns)
                col.Visible = col.DataPropertyName == nameof(CategoryDTO.Name);
            view.dgvCategories.Columns[nameof(CategoryDTO.Name)].HeaderText = "Category";

            // 2) clear assigned‐meals pane
            view.dgvMealsCategory.DataSource = null;

            // 3) rebind “all meals” with filter
            RefreshAllMealsGrid();

            // 4) also refresh the assigned list (if a category is selected)
            if (view.dgvCategories.SelectedRows.Count > 0)
                OnCategoryChanged(null, EventArgs.Empty);
        }


        private void OnCategoryChanged(object sender, EventArgs e)
        {
            var c = view.SelectedCategory();
            if (c == null) return;

            var filter = GetSelectedMenuType();
            var inCat = repo.GetMealsByCategory(c.Id)
                .Where(m =>
                    string.IsNullOrEmpty(filter)
                 || (m.MenuType ?? "")
                        .Split(',', (char)StringSplitOptions.RemoveEmptyEntries)
                        .Any(t => t.Trim().Equals(filter, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            view.dgvMealsCategory.DataSource = inCat;

            // now hide raw columns & rename headers exactly like RefreshAllMealsGrid
            view.dgvMealsCategory.Columns[nameof(MealDTO.ID)].Visible = false;
            view.dgvMealsCategory.Columns[nameof(MealDTO.RestaurantId)].Visible = false;
            view.dgvMealsCategory.Columns[nameof(MealDTO.CategoryId)].Visible = false;
            view.dgvMealsCategory.Columns[nameof(MealDTO.PhotoFile)].Visible = false;

            view.dgvMealsCategory.Columns[nameof(MealDTO.CategoryName)].Visible = false;

            view.dgvMealsCategory.Columns[nameof(MealDTO.Name)].HeaderText = "Meal";
            view.dgvMealsCategory.Columns[nameof(MealDTO.Price)].HeaderText = "Price";
            view.dgvMealsCategory.Columns[nameof(MealDTO.Details)].HeaderText = "Description";
            view.dgvMealsCategory.Columns[nameof(MealDTO.BestFood)].HeaderText = "Best?";
            view.dgvMealsCategory.Columns[nameof(MealDTO.MenuType)].HeaderText = "Menu Type";
        }

        private void AssignMeal()
        {
            var c = view.SelectedCategory();
            var m = view.SelectedAllMeal();
            if (c == null || m == null) return;

            // 1) Tell the API
            repo.AddMealToCategory(c.Id, m.ID);

            RefreshAllMealsGrid();
            OnCategoryChanged(null, null);
        }

        private void UnassignMeal()
        {
            var c = view.SelectedCategory();
            var m = view.SelectedCatMeal();
            if (c == null || m == null) return;

            repo.RemoveMealFromCategory(c.Id, m.ID);

            RefreshAllMealsGrid();
            OnCategoryChanged(null, null);
        }
        private void RefreshAllMealsGrid()
        {
            var all = repo.GetAllMeals(restaurantId);
            var filter = GetSelectedMenuType();
            var list = all
                .Where(m =>
                    string.IsNullOrEmpty(filter)
                 || (m.MenuType ?? "")
                        .Split(',', (char)StringSplitOptions.RemoveEmptyEntries)
                        .Any(t => t.Trim().Equals(filter, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            view.dgvMeals.DataSource = list;

            // now hide ID, RestaurantId, CategoryId, PhotoFile
            view.dgvMeals.Columns[nameof(MealDTO.ID)].Visible = false;
            view.dgvMeals.Columns[nameof(MealDTO.RestaurantId)].Visible = false;
            view.dgvMeals.Columns[nameof(MealDTO.CategoryId)].Visible = false;
            view.dgvMeals.Columns[nameof(MealDTO.PhotoFile)].Visible = false;

            // show CategoryName instead of CategoryId (make sure your DTO has it!)
            view.dgvMeals.Columns[nameof(MealDTO.CategoryName)].Visible = true;
            view.dgvMeals.Columns[nameof(MealDTO.CategoryName)].HeaderText = "Category";

            // friendly headers for the rest
            view.dgvMeals.Columns[nameof(MealDTO.Name)].HeaderText = "Meal";
            view.dgvMeals.Columns[nameof(MealDTO.Price)].HeaderText = "Price";
            view.dgvMeals.Columns[nameof(MealDTO.Details)].HeaderText = "Description";
            view.dgvMeals.Columns[nameof(MealDTO.BestFood)].HeaderText = "Best?";
            view.dgvMeals.Columns[nameof(MealDTO.MenuType)].HeaderText = "Menu Type";
        }


        // ────────────── Category CRUD ───────────────────────────
        private void EditCategory(CategoryDTO existing)
        {
            // 1) Instantiate your dialog
            var dlg = new CategoryDialog();
            dlg.Text = existing == null ? "New Category" : "Edit Category";

            // 2) Auto-fill & lock the Restaurant ID
            dlg.tb_restaurant.Text = restaurantId.ToString();
            dlg.tb_restaurant.ReadOnly = true;

            // 3) If editing, pre-populate the name
            if (existing != null)
                dlg.tb_name.Text = existing.Name;

            // 4) Wire up your Accept button
            dlg.btn_accept.Click += (s, e) =>
            {
                // Build DTO
                var dto = new CategoryDTO
                {
                    Id = existing?.Id ?? 0,
                    RestaurantId = restaurantId,
                    Name = dlg.tb_name.Text.Trim()
                };

                // Create or update
                if (existing == null)
                    repo.CreateCategory(dto);
                else
                    repo.UpdateCategory(existing.Id, dto);

                // Refresh and close
                LoadCategories();
                dlg.Close();

                // Refresh the middle grid
                if (view.dgvCategories.SelectedRows.Count > 0)
                    OnCategoryChanged(null, EventArgs.Empty);
            };

            // 5) Wire up Cancel
            dlg.btn_cancel.Click += (s, e) =>
            {
                dlg.Close();
            };

            // 6) Show it
            dlg.ShowDialog();
        }

        private void DeleteCategory()
        {
            var c = view.SelectedCategory();
            if (c == null) return;

            // 1) Check for any meals still assigned
            var assigned = repo.GetMealsByCategory(c.Id);
            if (assigned != null && assigned.Count > 0)
            {
                MessageBox.Show(
                    $"Cannot delete category “{c.Name}” because it has {assigned.Count} meal(s) assigned.\n" +
                    "Please unassign all meals first.",
                    "Delete Not Allowed",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 2) Single confirmation prompt
            if (MessageBox.Show(
                    $"Are you sure you want to delete “{c.Name}”?",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                ) == DialogResult.Yes)
            {
                // 3) Perform the delete
                repo.DeleteCategory(c.Id);

                // 4) Refresh the category (and all‐meals) grids
                LoadCategories();

                // Refresh the middle grid
                if (view.dgvCategories.SelectedRows.Count > 0)
                    OnCategoryChanged(null, EventArgs.Empty);
            }

        }

        private void GenerateQr()
        {
            try
            {
                // 1) Prompt for table number
                string input = Interaction.InputBox("Enter table number:", "Generate QR", "1");
                if (!int.TryParse(input, out int tableNumber))
                {
                    MessageBox.Show(
                        "Please enter a valid table number.",
                        "Invalid Input",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                // 2) Create the QR record
                var qrDto = new QrCodeDTO { RestaurantId = restaurantId, TableNumber = tableNumber };
                var created = repo.CreateQrCode(qrDto);
                if (created == null)
                    return;

                // 3) Build the URL to encode
                string menuType = GetSelectedMenuType();
                string url = repo.baseUrl
                             + "Qrcodes/resolveqr/" + created.Id
                             + "?menuType=" + Uri.EscapeDataString(menuType);

                // 4) Generate the QR code bitmap
                Bitmap qrImage;
                using (var generator = new QRCoder.QRCodeGenerator())
                {
                    var data = generator.CreateQrCode(url, QRCoder.QRCodeGenerator.ECCLevel.Q);
                    using (data)
                    using (var code = new QRCoder.QRCode(data))
                    {
                        // 5 pixels/module for a moderate size
                        qrImage = code.GetGraphic(5);
                    }
                }

                // 5) Build and show the popup with Save button
                var popup = new Form
                {
                    Text = "Scan or Save this QR",
                    StartPosition = FormStartPosition.CenterParent,
                    ClientSize = new Size(qrImage.Width + 20, qrImage.Height + 60)
                };

                // PictureBox at top
                var pic = new PictureBox
                {
                    Image = qrImage,
                    SizeMode = PictureBoxSizeMode.Zoom,
                    Dock = DockStyle.Top,
                    Height = qrImage.Height
                };
                popup.Controls.Add(pic);

                // Save As… button at bottom
                var btnSave = new Button
                {
                    Text = "Save As…",
                    Dock = DockStyle.Bottom,
                    Height = 30
                };
                btnSave.Click += (s, e) =>
                {
                    try
                    {
                        // 1) Determine a path on the user’s Desktop
                        string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                        string filename = $"QR_{created.Id}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                        string fullPath = System.IO.Path.Combine(desktop, filename);

                        // 2) Save the image there
                        qrImage.Save(fullPath, System.Drawing.Imaging.ImageFormat.Png);

                        // 3) Reveal it in Explorer (selected)
                        System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{fullPath}\"");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            "Could not save QR to Desktop:\n" + ex.Message,
                            "Save Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                };

                popup.Controls.Add(btnSave);

                // Show the QR window
                popup.ShowDialog();
                // no explicit Dispose() needed; GC will clean up
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Unexpected error generating QR:\n" + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }


        // ────────────── Helpers ─────────────────────────────────
        private string GetSelectedMenuType()
        {
            if (view.radioBuffet.Checked) return "Buffet";
            if (view.radioDaily.Checked) return "DailyMenu";
            if (view.radioFull.Checked) return "FullMenu";
            return "";
        }
    }

    // ────────────── MainWindow extension helpers ──────────────
    public static class MainWindowExtensions
    {
        public static CategoryDTO SelectedCategory(this MainWindow v)
            => v.dgvCategories.SelectedRows.Count == 0
               ? null
               : v.dgvCategories.SelectedRows[0].DataBoundItem as CategoryDTO;

        public static MealDTO SelectedAllMeal(this MainWindow v)
            => v.dgvMeals.SelectedRows.Count == 0
               ? null
               : v.dgvMeals.SelectedRows[0].DataBoundItem as MealDTO;

        public static MealDTO SelectedCatMeal(this MainWindow v)
            => v.dgvMealsCategory.SelectedRows.Count == 0
               ? null
               : v.dgvMealsCategory.SelectedRows[0].DataBoundItem as MealDTO;
    }
}
