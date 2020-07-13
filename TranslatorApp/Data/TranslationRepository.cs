using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslatorApp.Models;

namespace TranslatorApp.Data
{
    public class TranslationRepository : ITranslationRepository
    {
        private readonly TranslatorDbContext _translatorDbContext;

        public TranslationRepository(TranslatorDbContext translatorDbContext)
        {
            _translatorDbContext = translatorDbContext;
        }

        public async Task<IEnumerable<Translation>> GetTranslationsAsync()
        {
            return await _translatorDbContext.Translations.ToListAsync();
        }

        public async Task<Translation> GetTranslationDetailsAsync(int? id)
        {
            return await _translatorDbContext.Translations.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTranslationAsync(Translation translation)
        {
            await _translatorDbContext.AddAsync(translation);
            await _translatorDbContext.SaveChangesAsync();
        }
    }
}
