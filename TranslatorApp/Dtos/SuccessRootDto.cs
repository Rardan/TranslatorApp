using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TranslatorApp.Dtos
{
    public class SuccessRootDto
    {
        public SuccessDto Success { get; set; }
        public ContentsDto Contents { get; set; }
    }
}
