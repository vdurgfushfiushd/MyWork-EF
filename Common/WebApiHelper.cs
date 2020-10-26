using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Common
{
    public static class WebApiHelper 
    {
        static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<dynamic> PostAsync<dynamic>(string url, object data)
        {
            try
            {
                string content = JsonConvert.SerializeObject(data);
                var buffer = Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await Client.PostAsync(url, byteContent).ConfigureAwait(false);
                string result = await response.Content.ReadAsStringAsync();
                if (typeof(dynamic).Name.Equals("Response")) //用于post的增删改操作
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        var message = response.Content.ReadAsAsync<HttpError>().Result.ExceptionMessage;
                        result = JsonConvert.SerializeObject(new Response() { success = false, msg = message });
                        var resp = JsonConvert.DeserializeObject<dynamic>(result);
                        return JsonConvert.DeserializeObject<dynamic>(result);
                    }
                    else
                    {
                        return JsonConvert.DeserializeObject<dynamic>(result);
                    }
                }
                else    //用于post查询结果
                {
                    return JsonConvert.DeserializeObject<dynamic>(result);
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    string responseContent = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    throw new Exception($"response :{responseContent}", ex);
                }
                throw;
            }
        }

        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<dynamic> GetAsync<dynamic>(string url, object data)
        {
            try
            {
                string requestUrl;
                if (data == null)
                {
                    requestUrl = $"{url}";
                }
                else
                {
                    requestUrl = $"{url}?{GetQueryString(data)}";
                }
                var response = await Client.GetAsync(requestUrl).ConfigureAwait(false);
                string result = await response.Content.ReadAsStringAsync();
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    return default(dynamic);
                }
                return JsonConvert.DeserializeObject<dynamic>(result);
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    string responseContent = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    throw new Exception($"Response :{responseContent}", ex);
                }
                throw;
            }
        }

        /// <summary>
        /// 参数转换
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private static string GetQueryString(object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());
            return String.Join("&", properties.ToArray());
        }
    }
}
