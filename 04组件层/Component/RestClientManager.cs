using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;


//RestClientManager rcm = new RestClientManager();
//JObject paras = new JObject
//{
//    ["appid"] = "",
//    ["secret"] = "",
//    ["code"] = "",
//    ["grant_type"] = "authorization_code"
//};
//JObject objToken = rcm.Post("http://172.30.77.87", "/Config/api/SysConfig/LogsUrl", paras);
//string url = objToken["baseurl111"].ToString();
//if (objToken["errcode"] == null)
//{
//}

namespace Component
{
    public class RestClientManager
    {
        public JObject Post(string baseUrl, string url, JObject sendData)
        {
            return Post(baseUrl, url, sendData, string.Empty);
        }

        public JObject Post(string baseUrl, string url, JObject sendData, string token)
        {
            RestClient client = new RestClient(baseUrl);
            var request = new RestRequest(url, Method.POST);
            var contenType = "application/json";
            request.AddHeader("Accept", contenType);
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.AddHeader("token", token);
            }
            if (sendData == null)
            {
                sendData = new JObject();
            }
            request.AddParameter(contenType, sendData, ParameterType.RequestBody);
            var response = client.Execute(request);
            if (string.IsNullOrWhiteSpace(response.Content))
            {
                return null;
            }
            return JObject.Parse(JsonConvert.DeserializeObject(response.Content).ToString());
        }

        public JObject Get(string baseUrl, string url, JObject sendData)
        {
            return Get(baseUrl, url, sendData, string.Empty);
        }

        public JObject Get(string baseUrl, string url, JObject sendData, string token)
        {
            string parames = string.Empty;
            if (sendData != null)
            {
                StringBuilder datas = new StringBuilder();
                foreach (var item in sendData)
                {
                    datas.AppendFormat("{0}={1}", item.Key, item.Value);
                }
                parames = string.Format("?{0}", string.Join("&", datas));
            }
            var client = new RestClient(string.Format("{0}{1}{2}", baseUrl, url, parames));
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);
            object o = JsonConvert.DeserializeObject(response.Content);
            return JObject.Parse(JsonConvert.DeserializeObject(response.Content).ToString());
        }
    }
}
