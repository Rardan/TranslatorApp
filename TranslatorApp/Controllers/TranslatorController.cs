using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TranslatorApp.Data;
using TranslatorApp.Dtos;
using TranslatorApp.Models;
using TranslatorApp.Services;
using TranslatorApp.ViewModels;

namespace TranslatorApp.Controllers
{
    public class TranslatorController : Controller
    {
        private readonly IApiHttpClientService _client;
        private readonly ITranslatorRepository _translatorRepository;
        private readonly ITranslationRepository _translationRepository;

        public TranslatorController(IApiHttpClientService client, ITranslatorRepository translatorRepository, ITranslationRepository translationRepository)
        {
            _client = client;
            _translatorRepository = translatorRepository;
            _translationRepository = translationRepository;
        }

        // GET: Translations
        public async Task<IActionResult> Index()
        {
            return View(await _translatorRepository.GetQueriesAsync());
        }

        // GET: Translator/Translate
        [HttpGet]
        public async Task<IActionResult> Translate()
        {
            var translations = await _translationRepository.GetTranslationsAsync();
            ViewData["translations"] = new SelectList(translations, "Id", "Language");
            return View();
        }

        // POST: Translator/Translate
        [HttpPost]
        public async Task<IActionResult> Translate([Bind("Text,Id")]TranslatorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var language = await _translationRepository.GetTranslationDetailsAsync(model.Id);
                var response = await _client.GetResponseAsync(model.Text, language.Language);
                var resString = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    var success = JsonConvert.DeserializeObject<SuccessRootDto>(resString);

                    var successModel = new SuccessResponse
                    {
                        Translation = success.Contents.Translation,
                        Text = success.Contents.Text,
                        Translated = success.Contents.Translated
                    };

                    await _translatorRepository.AddResponseAsync(model.Text, model.Id, successModel, null);
                    return RedirectToAction("Details", new { id = successModel.QueryId });
                }
                else
                {
                    var error = JsonConvert.DeserializeObject<ErrorRootDto>(resString);

                    var errorModel = new ErrorResponse
                    {
                        Code = error.Error.Code,
                        Message = error.Error.Message
                    };

                    await _translatorRepository.AddResponseAsync(model.Text, model.Id, null, errorModel);
                    return RedirectToAction("Details", new { id = errorModel.QueryId });
                }
            }

            return View(model);
        }

        // GET: Translator/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = await _translatorRepository.GetQueryAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            if (result.Success)
            {
                return RedirectToAction("DetailsSuccess", new { result.Id });
            }

            return RedirectToAction("DetailError", new { result.Id });
        }

        // GET: Translator/DetailsSuccess/5
        public IActionResult DetailsSuccess(int? id)
        {
            return View(_translatorRepository.GetSuccessResponse(id));
        }

        // GET: Translator/DetailsError/5
        public IActionResult DetailError(int? id)
        {
            return View(_translatorRepository.GetErrorResponse(id));
        }
    }
}