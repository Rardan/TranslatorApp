using System.Collections.Generic;
using System.Threading.Tasks;
using TranslatorApp.Models;

namespace TranslatorApp.Data
{
    public interface ITranslationRepository
    {
        Task AddTranslationAsync(Translation translation);
        Task<Translation> GetTranslationDetailsAsync(int? id);
        Task<IEnumerable<Translation>> GetTranslationsAsync();
    }
}