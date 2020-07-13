using System.Collections.Generic;
using System.Threading.Tasks;
using TranslatorApp.Models;

namespace TranslatorApp.Data
{
    public interface ITranslatorRepository
    {
        Task AddResponseAsync(string call, int translatonId, SuccessResponse success = null, ErrorResponse error = null);
        ErrorResponse GetErrorResponse(int? queryId);
        Task<IEnumerable<Query>> GetQueriesAsync();
        Task<Query> GetQueryAsync(int? id);
        SuccessResponse GetSuccessResponse(int? queryId);
        Task SaveChangesAsync();
    }
}