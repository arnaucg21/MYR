using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace MakeYourRestaurant.Model
{
    public class Repository
    {
        // Your real API base URL
        private readonly string ws1 = "http://172.16.24.106:7171/api/";

        // Main method to validate login
        public bool GetEmployee(string username, string password)
        {
            Employee e = GetEmployeeInfo(username, password);
            return e != null;
        }

        // Get full employee object from API
        public Employee GetEmployeeInfo(string username, string password)
        {
            string url = $"{ws1}Employees/auth/{username}/{password}";
            return (Employee)MakeRequest(url, null, "GET", "text/plain", typeof(Employee));
        }

        // Make request and deserialize result
        public static object MakeRequest(string requestUrl, object JSONRequest, string JSONmethod, string JSONContentType, Type JSONResponseType)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                request.Method = JSONmethod;

                // Correct: Accept header (not ContentType) for GET
                request.Accept = JSONContentType;

                // Only set Content-Type if not GET
                if (JSONmethod != "GET" && JSONRequest != null)
                {
                    request.ContentType = JSONContentType;
                    string json = JsonConvert.SerializeObject(JSONRequest);
                    byte[] data = Encoding.UTF8.GetBytes(json);
                    using (Stream stream = request.GetRequestStream())
                        stream.Write(data, 0, data.Length);
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string result = reader.ReadToEnd();

                    // Optional debug
                    System.Diagnostics.Debug.WriteLine("Response:");
                    System.Diagnostics.Debug.WriteLine(result);

                    return JsonConvert.DeserializeObject(result, JSONResponseType);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error during API request: {ex.Message}", "API Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

    }
}
