using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Utility.RestSharp
{
    public static class HttpHelper
    {
        public static T GetApi<T>(string Url,int regattaId, string apiName, string pragm = "")
        {
            var client = new RestSharpClient($"{Url}");
            var apiNameStr = string.Format($"{apiName}", regattaId);

            var request = client.Execute(string.IsNullOrEmpty(pragm)
                ? new RestRequest(apiNameStr, Method.GET)
                : new RestRequest($"{apiNameStr}/{pragm}", Method.GET));

            if (request.StatusCode != HttpStatusCode.OK)
            {
                return (T)Convert.ChangeType(request.ErrorMessage, typeof(T));
            }

            T result = (T)Convert.ChangeType(request.Content, typeof(T));

            return result;
        }

        public static T PostApi<T>(int regattaId, int id, string url, string alias)
        {
            var client = new RestClient($"{url}");
            IRestRequest queest = new RestRequest();
            queest.Method = Method.POST;
            queest.AddHeader("Accept", "application/json");
            queest.RequestFormat = DataFormat.Json;
            queest.AddBody(new { userid = id, Url = url, alias = alias, count = 1 }); // uses JsonSerializer
            var result = client.Execute(queest);
            if (result.StatusCode != HttpStatusCode.OK)
            {
                return (T)Convert.ChangeType(result.ErrorMessage, typeof(T));
            }

            T request = (T)Convert.ChangeType(result.Content, typeof(T));
            return request;
        }
    }
}
