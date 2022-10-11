using Newtonsoft.Json;
using ServiceConnectUtils.Enums;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace ServiceConnectUtils
{
    public static class ServiceConnect
    {

        public static  string Get<T>(ServiceTypeEnum serviceType,string methodName, HttpMethod methodType, T requestData)
        {
            var port = ((int)serviceType).ToString();

            string baseUrl = string.Concat("http://localhost:", port, "/api/", methodName);
            using (var client = new HttpClient { Timeout = TimeSpan.FromMinutes(60) })
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var request = new HttpRequestMessage
                {
                    Method = methodType,
                    RequestUri = new Uri(baseUrl),
                };

                if (requestData != null)
                {
                    var jsonRequest = JsonConvert.SerializeObject(requestData);
                    request.Content = new StringContent(jsonRequest, Encoding.UTF8, MediaTypeNames.Application.Json /* or "application/json" in older versions */);
                }

                HttpResponseMessage response = new HttpResponseMessage();
                HttpContent content;
                string x=String.Empty;
                try
                {
                    response = client.SendAsync(request).Result;
                    content = response.Content;

                    x = content.ReadAsStringAsync().Result;
                }
                catch (Exception e)
                {
                    throw new Exception("Connection service is not accessible. Message: " + e.Message);
                }
                return x;
            }
        }

        public static string Get(ServiceTypeEnum serviceType, string methodName)
        {
            var port = ((int)serviceType).ToString();
            string baseUrl = string.Concat("http://localhost:", port, "/api/", methodName);
            using (var client = new HttpClient { Timeout = TimeSpan.FromMinutes(60) })
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(baseUrl),
                };

                HttpResponseMessage response = new HttpResponseMessage();
                HttpContent content;
                string x = String.Empty;
                try
                {
                    response = client.SendAsync(request).Result;
                    content = response.Content;

                    x = content.ReadAsStringAsync().Result;
                }
                catch (Exception e)
                {
                    throw new Exception("Connection service is not accessible. Message: " + e.Message);
                }
                return x;

            }
        }
    }
}
