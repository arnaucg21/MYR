using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourRestaurant___Order.Model
{
    public class Repository
    {
        //string ws1 = "https://localhost:7133/api/";
        string ws1 = "http://172.16.24.106:7171/api/";

        public List<OrderDTO> GetAllOrders(int? idRestaurant)
        {
            List<Order> tempOrders;
            List<OrderDTO> finalOrders = new List<OrderDTO>();

            string url = string.Concat(ws1, "GetAllOrders/", idRestaurant);
            tempOrders = (List<Order>)MakeRequest(url, null, "GET", "application/json", typeof(List<Order>));

            foreach (Order o in tempOrders)
            {
                OrderDTO od = new OrderDTO(o);
                finalOrders.Add(od);
            }

            return finalOrders;
        }

        public void DeleteOrder(int orderId)
        {
            MakeRequest(string.Concat(ws1, "DeleteOrder/", orderId), null, "DELETE", "application/json", typeof(void));
        }

        public List<OrderDTO> FilterOrders(int? restId, int orderId)
        {
            List<Order> orders;
            List<OrderDTO> modifiedOrders = new List<OrderDTO>();

            orders = (List<Order>)MakeRequest(string.Concat(ws1, "GetOrdersId/", restId, "/", orderId), null, "GET", "application/json", typeof(List<Order>));

            foreach (Order o in orders)
            {
                OrderDTO od = new OrderDTO(o);
                modifiedOrders.Add(od);
            }

            return modifiedOrders;
        }

        public List<OrderlineDTO> GetOrderLines(int idOrder)
        {
            List<OrderLine> tempOrderlines;
            List<OrderlineDTO> finalOrderlines = new List<OrderlineDTO>();

            string url = string.Concat(ws1, "OrderLines/api/GetOrderLinesByOrderId/", idOrder);
            tempOrderlines = (List<OrderLine>)MakeRequest(url, null, "GET", "application/json", typeof(List<OrderLine>));

            foreach (OrderLine o in tempOrderlines)
            {
                OrderlineDTO od = new OrderlineDTO(o);
                finalOrderlines.Add(od);
            }

            return finalOrderlines;
        }

        public static object MakeRequest(string requestUrl, object JSONRequest, string JSONmethod, string JSONContentType, Type JSONResponseType)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest; //WebRequest WR = WebRequest.Create(requestUrl);   
                string sb = JsonConvert.SerializeObject(JSONRequest);
                request.Method = JSONmethod;  // "GET"/"POST"/"PUT"/"DELETE";  
                if (JSONmethod != "GET")
                {
                    request.ContentType = JSONContentType; // "application/json";   
                    Byte[] bt = Encoding.UTF8.GetBytes(sb);
                    Stream st = request.GetRequestStream();
                    st.Write(bt, 0, bt.Length);
                    st.Close();
                }
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
                    Stream stream1 = response.GetResponseStream();
                    StreamReader sr = new StreamReader(stream1);
                    string strsb = sr.ReadToEnd();
                    object objResponse = JsonConvert.DeserializeObject(strsb, JSONResponseType);
                    return objResponse;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
