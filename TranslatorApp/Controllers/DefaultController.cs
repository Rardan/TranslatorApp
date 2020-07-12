using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TranslatorApp.Data;
using TranslatorApp.Dtos;
using TranslatorApp.Models;
using TranslatorApp.Services;

namespace TranslatorApp.Controllers
{
    public class DefaultController<T> : Controller where T : class
    {
        private readonly IApiHttpClientService _client;
        private readonly ITranslatorRepository _translatorRepository;

        public DefaultController(IApiHttpClientService client, ITranslatorRepository translatorRepository)
        {
            _client = client;
            _translatorRepository = translatorRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _translatorRepository.GetQueriesAsync();

            return View(list);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string text)
        {
            var response = await _client.GetResponseAsync(text);
            var resString = await response.Content.ReadAsStringAsync();
            if(response.IsSuccessStatusCode)
            {
                var success = JsonConvert.DeserializeObject<SuccessRootDto>(resString);

                var successModel = new SuccessResponse
                {
                    Translation = success.Contents.Translation,
                    Text = success.Contents.Text,
                    Translated = success.Contents.Translated
                };

                await _translatorRepository.AddResponseAsync(text, successModel, null);
            }
            else
            {
                var error = JsonConvert.DeserializeObject<ErrorRootDto>(resString);

                var errorModel = new ErrorResponse
                {
                    Code = error.Error.Code,
                    Message = error.Error.Message
                };

                await _translatorRepository.AddResponseAsync(text, null, errorModel);
            }
            var list = await _translatorRepository.GetQueriesAsync();

            return View(list);
        }

        public IActionResult Details(int id)
        {
            var result = _translatorRepository.GetSuccessResponse(id);

            if(result == null)
            {
                var error = _translatorRepository.GetErrorResponse(id);

                return View(error);
            }

            return View(result);
        }
    }
}