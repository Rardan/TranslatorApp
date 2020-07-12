using System.Collections.Generic;
using System.Threading.Tasks;
using TranslatorApp.Models;

namespace TranslatorApp.Data
{
    public interface ITranslatorRepository
    {
        Task AddTranslationAsync(ResultTranslation resultTranslation);
        Task<IEnumerable<ResultTranslation>> GetAllTranslationsAsync();
        Task<IEnumerable<ResultTranslation>> GetTranslationsGroupAsync(string type);
        Task SaveChangesAsync();
    }
}