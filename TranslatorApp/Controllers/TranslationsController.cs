using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TranslatorApp.Data;
using TranslatorApp.Models;

namespace TranslatorApp.Controllers
{
    public class TranslationsController : Controller
    {
        private readonly ITranslationRepository _translationRepository;

        public TranslationsController(ITranslationRepository translationRepository)
        {
            _translationRepository = translationRepository;
        }

        // GET: Translations
        public async Task<IActionResult> Index()
        {
            return View(await _translationRepository.GetTranslationsAsync());
        }

        // GET: Translations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var translation = await _translationRepository.GetTranslationDetailsAsync(id);
            if (translation == null)
            {
                return NotFound();
            }

            return View(translation);
        }

        // GET: Translations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Translations/Create
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Language")] Translation translation)
        {
            if (ModelState.IsValid)
            {
                await _translationRepository.AddTranslationAsync(translation);
                return RedirectToAction(nameof(Index));
            }
            return View(translation);
        }
    }
}
