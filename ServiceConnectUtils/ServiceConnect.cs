using Newtonsoft.Json;
using ServiceConnectUtils.BaseModels;
using ServiceConnectUtils.Enums;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;

namespace ServiceConnectUtils
{
    public static class ServiceConnect
    {

        public static TResponse Get<TResponse>(ServiceTypeEnum serviceType, string methodName, IReturn<TResponse> request)
        {

            var port = ((int)serviceType).ToString();

            string baseUrl = string.Concat("http://localhost:", port, "/api/", methodName);
            using (var client = new HttpClient { Timeout = TimeSpan.FromMinutes(60) })
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var jsonRequest = JsonConvert.SerializeObject(request);

                var stringContent = new StringContent(jsonRequest, Encoding.UTF8, MediaTypeNames.Application.Json);


                HttpResponseMessage response = new HttpResponseMessage();
                HttpContent content;
                string x = String.Empty;
                try
                {
                    response = client.PostAsync(baseUrl, stringContent).Result;
                    content = response.Content;

                    x = content.ReadAsStringAsync().Result;
                }
                catch (Exception e)
                {
                    throw new Exception("Connection service is not accessible. Message: " + e.Message);
                }

                if (response.IsSuccessStatusCode)
                {
                    try
                    {

                        return JsonConvert.DeserializeObject<TResponse>(x);
                    }
                    catch (Exception e)
                    {

                        throw new Exception(e.Message);
                    }
                }
                throw new Exception(string.Concat(response.StatusCode.ToString(), ": Request - ", methodName
                    , ", ", x));
            }
        }

    }
}
