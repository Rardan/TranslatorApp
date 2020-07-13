﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TranslatorApp.ViewModels
{
    public class TranslatorViewModel
    {
        [Required]
        public string Text { get; set; }
        public int Id { get; set; }
    }
}
