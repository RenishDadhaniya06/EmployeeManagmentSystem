using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Helpers
{
    /// <summary>
    /// API Helper
    /// </summary>
    public static class APIHelpers
    {
        /// <summary>
        /// The get
        /// </summary>
        public const string GET = "GET";

        /// <summary>
        /// The post
        /// </summary>
        public const string POST = "POST";

        /// <summary>
        /// The put
        /// </summary>
        public const string PUT = "PUT";

        /// <summary>
        /// The delete
        /// </summary>
        public const string DELETE = "DELETE";

        /// <summary>Gets the asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(string uri)
        {
            try
            {
                var response = await CallApi(uri, "", GET);
                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
                }
                else
                {
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        /// <summary>Posts the asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static async Task<T> PostAsync<T>(string uri, T data)
        {
            var response = await CallApi(uri, JsonConvert.SerializeObject(data), POST);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>Puts the asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public static async Task<T> PutAsync<T>(string uri, T data)
        {
            var response = await CallApi(uri, JsonConvert.SerializeObject(data), PUT);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>Deletes the asynchronous.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public static async Task<T> DeleteAsync<T>(string uri)
        {

            var response = await CallApi(uri, "", DELETE);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Calls the API.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="data">The data.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> CallApi(string url, string data, string type)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            client.BaseAddress = new Uri(CommonHelper.ApiURL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            switch (type)
            {
                case GET:
                    response = await client.GetAsync(url);
                    break;
                case POST:
                    var buffer = System.Text.Encoding.UTF8.GetBytes(data);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    response = await client.PostAsync(url, byteContent);
                    break;
                case PUT:
                    var putBuffer = System.Text.Encoding.UTF8.GetBytes(data);
                    var putByteContent = new ByteArrayContent(putBuffer);
                    putByteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    response = await client.PutAsync(url, putByteContent);
                    break;
                case DELETE:
                    response = await client.DeleteAsync(url);
                    break;
            }
            return response;
        }
    }
}
