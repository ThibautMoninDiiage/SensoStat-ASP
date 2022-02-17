using System;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace SensoStatWeb.Business
{
    public class HttpService
    {
        public async Task<T> SendHttpRequest<T>(string url, HttpMethod httpMethod, string? bearer = null)
        {
            var httpClient = new HttpClient();

            // For OAuth2.0
            if (!string.IsNullOrEmpty(bearer))
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearer);

            var httpRequestMessage = new HttpRequestMessage() { Method = httpMethod, RequestUri = new Uri(url) };

            var response = httpClient.SendAsync(httpRequestMessage);

            if (response.Result.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<T>(await response.Result.Content.ReadAsStringAsync());


            return default(T);
        }
    }
}

