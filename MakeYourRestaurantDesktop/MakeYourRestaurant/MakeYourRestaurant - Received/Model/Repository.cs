using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MakeYourRestaurant___Received.Model
{
    public class Repository
    {
        //string ws1 = "https://localhost:7133/api/";
        string ws1 = "http://172.16.24.182:45455/api/";

        public int GetNumBilledOrders(int idRestaurant)
        {
            int numBilledOrders = 0;
            numBilledOrders = (int)MakeRequest(string.Concat(ws1, "GetNumBilledOrders/", idRestaurant), null, "GET", "application/json", typeof(int));
            return numBilledOrders;
        }

        public void DeleteOrder(int orderId)
        {
            MakeRequest(string.Concat(ws1, "DeleteOrder/", orderId), null, "DELETE", "application/json", typeof(void));
        }

        public int GetNumReceivedOrders(int idRestaurant)
        {
            int numReceivedOrders = 0;
            numReceivedOrders = (int)MakeRequest(string.Concat(ws1, "GetNumReceivedOrders/", idRestaurant), null, "GET", "application/json", typeof(int));
            return numReceivedOrders;
        }

        public List<int?> GetTables()
        {
            List<int?> tables = (List<int?>)MakeRequest(string.Concat(ws1, "GetTablesWithOrdersReceived/" + 1), null, "GET", "application/json", typeof(List<int?>));
            return tables;
        }

        public List<OrderDTO> GetReceivedOrders(int idRestaurant)
        {
            List<Order> receivedOrders;
            List<OrderDTO> modifiedOrders = new List<OrderDTO>();

            receivedOrders = (List<Order>)MakeRequest(string.Concat(ws1, "GetReceivedOrders/", idRestaurant), null, "GET", "application/json", typeof(List<Order>));

            foreach (Order o in receivedOrders)
            {
                OrderDTO od = new OrderDTO(o);
                modifiedOrders.Add(od);
            }

            return modifiedOrders;
        }


        public List<OrderDTO> GetOrdersByTable(int numTable, int? idRestaurant)
        {
            List<Order> tableOrders;
            List<OrderDTO> modifiedOrders = new List<OrderDTO>();

            string url = string.Concat(ws1, "OrdersReceivedByTableNumber/", idRestaurant);
            string lastUrl = string.Concat(url, "/", numTable);

            tableOrders = (List<Order>)MakeRequest(lastUrl, null, "GET", "application/json", typeof(List<Order>));
            foreach (Order o in tableOrders)
            {
                OrderDTO od = new OrderDTO(o);
                modifiedOrders.Add(od);
            }

            return modifiedOrders;
        }

        public List<OrderDTO> FilterOrders(int restId, int orderId)
        {
            List<Order> orders;
            List<OrderDTO> modifiedOrders = new List<OrderDTO>();

            orders = (List<Order>)MakeRequest(string.Concat(ws1, "GetReceivedOrdersId/", restId, "/", orderId), null, "GET", "application/json", typeof(List<Order>));

            foreach (Order o in orders)
            {
                OrderDTO od = new OrderDTO(o);
                modifiedOrders.Add(od);
            }

            return modifiedOrders;
        }
        public List<int?> GetTablesId(int restId, int orderId)
        {
            List<int?> tables = (List<int?>)MakeRequest(string.Concat(ws1, "GetReceivedMesaNumbersId/", restId, "/", orderId), null, "GET", "application/json", typeof(List<int?>));
            return tables;
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
