using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TranslatorApp.Models;

namespace TranslatorApp.Data
{
    public class TranslatorDbContext : DbContext
    {
        public TranslatorDbContext(DbContextOptions<TranslatorDbContext> options) : base(options)
        {
        }

        public DbSet<Query> Queries { get; set; }
        public DbSet<SuccessResponse> SuccessResponses { get; set; }
        public DbSet<ErrorResponse> ErrorResponses { get; set; }

    }
}
