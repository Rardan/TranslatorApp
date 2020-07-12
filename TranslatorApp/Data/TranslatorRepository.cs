using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslatorApp.Models;

namespace TranslatorApp.Data
{
    public class TranslatorRepository : ITranslatorRepository
    {
        private readonly TranslatorDbContext _translatorDbContext;

        public TranslatorRepository(TranslatorDbContext translatorDbContext)
        {
            _translatorDbContext = translatorDbContext;
        }
        public async Task<IEnumerable<ResultTranslation>> GetAllTranslationsAsync()
        {
            return await _translatorDbContext.ResultTranslations.ToListAsync();
        }

        public async Task<IEnumerable<ResultTranslation>> GetTranslationsGroupAsync(string type)
        {
            return await _translatorDbContext.ResultTranslations.Where(r => r.Translation == type).ToListAsync();
        }

        public async Task AddTranslationAsync(ResultTranslation resultTranslation)
        {
            await _translatorDbContext.AddAsync(resultTranslation);
        }

        public async Task SaveChangesAsync()
        {
            await _translatorDbContext.SaveChangesAsync();
        }
    }
}
