using System.Net.Http;
using System.Threading.Tasks;

namespace TranslatorApp.Services
{
    public interface IApiHttpClientService
    {
        Task<HttpResponseMessage> GetResponseAsync(string query, string language);
    }
}