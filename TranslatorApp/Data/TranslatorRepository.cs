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

        public async Task<IEnumerable<Query>> GetQueriesAsync()
        {
            return await _translatorDbContext.Queries.Include(t => t.Translation).ToListAsync();
        }

        public SuccessResponse GetSuccessResponse(int? queryId)
        {
            return _translatorDbContext.SuccessResponses.Include(q => q.Query).FirstOrDefault(r => r.QueryId == queryId);
        }

        public ErrorResponse GetErrorResponse(int? queryId)
        {
            return _translatorDbContext.ErrorResponses.Include(q => q.Query).FirstOrDefault(r => r.QueryId == queryId);
        }

        public async Task AddResponseAsync(string call, int translatonId, SuccessResponse success = null, ErrorResponse error = null)
        {
            var query = new Query
            {
                Call = call,
                Success = true,
                TranslationId = translatonId
            };
            await _translatorDbContext.Queries.AddAsync(query);
            await SaveChangesAsync();

            if (success != null && error == null)
            {
                success.QueryId = query.Id;
                await _translatorDbContext.SuccessResponses.AddAsync(success);
                await SaveChangesAsync();
            }

            if (success == null && error != null)
            {
                query.Success = false;
                _translatorDbContext.Queries.Update(query);
                error.QueryId = query.Id;
                await _translatorDbContext.ErrorResponses.AddAsync(error);
                await SaveChangesAsync();
            }
        }

        public async Task SaveChangesAsync()
        {
            await _translatorDbContext.SaveChangesAsync();
        }
    }
}
