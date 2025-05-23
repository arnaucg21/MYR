using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourRestaurant___Order.Model
{
    public partial class OrderlineDTO
    {
        public string MealName { get; set; }
        public double? MealPrice { get; set; }
        public int? Quantity { get; set; }

        public OrderlineDTO(OrderLine orderline)
        {
            this.MealName = FindMealName(orderline.MealId);
            this.MealPrice = FindMealPrice(orderline.MealId);
            this.Quantity = orderline.Quantity;
        }

        public string FindMealName(int? mealId)
        {
            //string ws1 = "https://localhost:7133/api/";
            string ws1 = "http://172.16.24.182:45455/api/";

            string requestUrl = string.Concat(ws1, "MealNameById/", mealId);
            return GetFieldFromJson(requestUrl, "name");
        }

        public double? FindMealPrice(int? mealId)
        {
            //string ws1 = "https://localhost:7133/api/";
            string ws1 = "http://172.16.24.182:45455/api/";

            string requestUrl = string.Concat(ws1, "MealPriceById/", mealId);
            return (double?)MakeRequest(requestUrl, null, "GET", "application/json", typeof(double));
        }

        public static string GetFieldFromJson(string requestUrl, string fieldName)
        {
            try
            {
                string jsonResponse = (string)MakeRequest(requestUrl, null, "GET", "application/json", typeof(string));
                if (!string.IsNullOrEmpty(jsonResponse))
                {
                    return jsonResponse;
                }
                return "";
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine($"Error deserializando JSON: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public static object MakeRequest(string requestUrl, object JSONRequest, string JSONmethod, string JSONContentType, Type JSONResponseType)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                string sb = JsonConvert.SerializeObject(JSONRequest);
                request.Method = JSONmethod;

                if (JSONmethod != "GET")
                {
                    request.ContentType = JSONContentType;
                    Byte[] bt = Encoding.UTF8.GetBytes(sb);
                    Stream st = request.GetRequestStream();
                    st.Write(bt, 0, bt.Length);
                    st.Close();
                }

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        string strsb = sr.ReadToEnd();
                        if (JSONResponseType == typeof(string))
                        {
                            return strsb;
                        }
                        object objResponse = JsonConvert.DeserializeObject(strsb, JSONResponseType);
                        return objResponse;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error en la solicitud: {e.Message}");
                return null;
            }
        }
    }
}
