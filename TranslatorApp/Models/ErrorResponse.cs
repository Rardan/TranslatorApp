using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranslatorApp.Models
{
    public class ErrorResponse
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public int QueryId { get; set; }

        public Query Query { get; set; }
    }
}
