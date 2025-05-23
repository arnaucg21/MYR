// ────────────────────────────────────────────────────────────────
// MakeYourRestaurant___Main/Model/Repository.cs
// ────────────────────────────────────────────────────────────────
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace MakeYourRestaurant___Main.Model
{
    public class Repository
    {
        public string baseUrl = "http://172.16.24.106:7171/api/";

        // ─── CATEGORY ────────────────────────────────────────────────

        // 1) Get all categories for a given restaurant
        public List<CategoryDTO> GetCategories(int restaurantId)
        {
            var url = $"{baseUrl}Categories/restaurant/{restaurantId}";
            return (List<CategoryDTO>)MakeRequest(url, null, "GET", typeof(List<CategoryDTO>));
        }

        // 2) Create a new category
        public CategoryDTO CreateCategory(CategoryDTO dto)
        {
            var url = $"{baseUrl}Categories";
            return (CategoryDTO)MakeRequest(url, dto, "POST", typeof(CategoryDTO));
        }

        // 3) Update an existing category
        public bool UpdateCategory(int categoryId, CategoryDTO dto)
        {
            var url = $"{baseUrl}Categories/{categoryId}";
            return MakeRequest(url, dto, "PUT", typeof(object)) != null;
        }

        // 4) Delete a category
        public bool DeleteCategory(int categoryId)
        {
            var url = $"{baseUrl}Categories/{categoryId}";
            return MakeRequest(url, null, "DELETE", typeof(object)) != null;
        }

        // 5) Get all meals *in* a given category
        public List<MealDTO> GetMealsByCategory(int categoryId)
        {
            var url = $"{baseUrl}Categories/{categoryId}/Meals";

            // Call your helper
            var raw = MakeRequest(url, null, "GET", typeof(List<MealDTO>));
            if (raw == null)
            {
                // We assume a 404 → “no meals in this category”
                return new List<MealDTO>();
            }

            return (List<MealDTO>)raw;
        }


        // 6) Assign a meal to a category
        public bool AddMealToCategory(int categoryId, int mealId)
        {
            var url = $"{baseUrl}Categories/{categoryId}/Meals/{mealId}";
            return MakeRequest(url, null, "POST", typeof(object)) != null;
        }

        // 7) Un-assign a meal from its category
        public bool RemoveMealFromCategory(int categoryId, int mealId)
        {
            var url = $"{baseUrl}Categories/{categoryId}/Meals/{mealId}";
            return MakeRequest(url, null, "DELETE", typeof(object)) != null;
        }

        // ─── MEAL ────────────────────────────────────────────────────

        // 8) Get all meals for a restaurant (for “assign” list)
        public List<MealDTO> GetAllMeals(int restaurantId)
        {
            var url = $"{baseUrl}Meals/restaurant/{restaurantId}";
            return (List<MealDTO>)MakeRequest(url, null, "GET", typeof(List<MealDTO>));
        }

        // 9) Create a new meal
        public MealDTO CreateMeal(MealDTO dto)
        {
            var url = $"{baseUrl}Meals";
            return (MealDTO)MakeRequest(url, dto, "POST", typeof(MealDTO));
        }

        // 10) Update an existing meal
        public MealDTO UpdateMeal(int mealId, MealDTO dto)
        {
            var url = $"{baseUrl}Meals/{mealId}";
            return (MealDTO)MakeRequest(url, dto, "PUT", typeof(MealDTO));
        }

        // 11) Delete a meal
        public bool DeleteMeal(int mealId)
        {
            var url = $"{baseUrl}Meals/{mealId}";
            return MakeRequest(url, null, "DELETE", typeof(object)) != null;
        }

        // ─── EMPLOYEE ───────────────────────────────────────────────

        // 12) Get all employees for this restaurant
        public List<EmployeeDTO> GetEmployeesByRestaurant(int restaurantId)
        {
            var url = $"{baseUrl}Employees/restaurant/{restaurantId}";
            return (List<EmployeeDTO>)MakeRequest(url, null, "GET", typeof(List<EmployeeDTO>));
        }

        // 13) Create a new employee
        public EmployeeDTO CreateEmployee(EmployeeDTO dto)
        {
            var url = $"{baseUrl}Employees";
            return (EmployeeDTO)MakeRequest(url, dto, "POST", typeof(EmployeeDTO));
        }

        // 14) Update an existing employee
        public EmployeeDTO UpdateEmployee(int employeeId, EmployeeDTO dto)
        {
            var url = $"{baseUrl}Employees/{employeeId}";
            return (EmployeeDTO)MakeRequest(url, dto, "PUT", typeof(EmployeeDTO));
        }

        // 15) Delete an employee
        public bool DeleteEmployee(int employeeId)
        {
            var url = $"{baseUrl}Employees/{employeeId}";
            return MakeRequest(url, null, "DELETE", typeof(object)) != null;
        }

        // ─── QRCODE ────────────────────────────────────────────────────

        public QrCodeDTO CreateQrCode(QrCodeDTO dto)
        {
            var url = $"{baseUrl}Qrcodes";
            return (QrCodeDTO)MakeRequest(url, dto, "POST", typeof(QrCodeDTO));
        }

        public JObject ResolveQr(int qrId, string menuType)
        {
            var url = $"{baseUrl}Qrcodes/resolveqr/{qrId}"
                    + $"?menuType={Uri.EscapeDataString(menuType)}";
            return (JObject)MakeRequest(url, null, "GET", typeof(JObject));
        }

        // ─── JSON / HTTP HELPER ─────────────────────────────────────
        private object MakeRequest(string url, object body, string method, Type returnType)
        {
            try
            {
                var req = WebRequest.Create(url) as HttpWebRequest;
                req.Method = method;

                if (method != "GET" && body != null)
                {
                    req.ContentType = "application/json";
                    var bytes = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(body));
                    using (var s = req.GetRequestStream())
                        s.Write(bytes, 0, bytes.Length);
                }

                using (var resp = req.GetResponse() as HttpWebResponse)
                using (var sr = new StreamReader(resp.GetResponseStream()))
                {
                    var json = sr.ReadToEnd();
                    return JsonConvert.DeserializeObject(json, returnType);
                }
            }
            catch (WebException wex) when (wex.Response is HttpWebResponse err)
            {
                // If this was a GET expecting a List<T> and the server returned 404,
                // swallow it and return an empty List<T> instead of an error dialog.
                if (method == "GET"
                 && returnType.IsGenericType
                 && returnType.GetGenericTypeDefinition() == typeof(List<>)
                 && err.StatusCode == HttpStatusCode.NotFound)
                {
                    // Create and return an empty List<T>
                    return Activator.CreateInstance(returnType);
                }

                // Otherwise, show the error details
                using (var sr = new StreamReader(err.GetResponseStream()))
                {
                    var bodyText = sr.ReadToEnd();
                    MessageBox.Show(
                        $"API {err.StatusCode}: {err.StatusDescription}\n\n{bodyText}",
                        "API Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Request failed:\n{ex.Message}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
