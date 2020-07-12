using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranslatorApp.Models
{
    public class Query
    {
        public int Id { get; set; }
        public string Call { get; set; }
        public bool Success { get; set; }

        public SuccessResponse SuccessResponse { get; set; }
        public ErrorResponse ErrorResponse { get; set; }
    }
}
