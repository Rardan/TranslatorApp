using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TranslatorApp.Services
{
    public class ApiHttpClientService : IApiHttpClientService
    {
        private readonly HttpClient _httpClient;

        public ApiHttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> GetResponseAsync(string query, string language)
        {
            string url = "https://api.funtranslations.com/translate/" + language + ".json?text=" + query;

            return await _httpClient.GetAsync(url);
        }
    }
}
