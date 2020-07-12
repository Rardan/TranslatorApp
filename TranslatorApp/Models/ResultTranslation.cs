using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranslatorApp.Models
{
    public class ResultTranslation
    {
        public int Id { get; set; }
        public string Translated { get; set; }
        public string Text { get; set; }
        public string Translation { get; set; }
    }
}
