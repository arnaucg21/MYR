using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MakeYourRestaurant___Received.Model
{
    public partial class OrderDTO
    {
        public int ID { get; set; }
        public Nullable<int> TableNumber { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public string ClientName { get; set; }

        public OrderDTO(Order order)
        {
            this.ID = order.ID;
            this.TableNumber = order.TableNumber;
            this.Date = order.Date;
            this.TotalPrice = order.TotalPrice;
            this.ClientName = FindClientName(order.ClientID);
        }

        public string FindClientName(int? clientId)
        {
            //string ws1 = "https://localhost:7133/api/";
            string ws1 = "http://172.16.24.182:45455/api/";

            Client c;
            c = (Client)MakeRequest(string.Concat(ws1, "Clients/", clientId), null, "GET", "application/json", typeof(Client));
            return c.NameComplete;
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
