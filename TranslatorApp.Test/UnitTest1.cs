using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TranslatorApp.Controllers;
using TranslatorApp.Data;
using TranslatorApp.Models;
using TranslatorApp.Services;
using TranslatorApp.ViewModels;
using Xunit;

namespace TranslatorApp.Test
{
    public class UnitTest1
    {
        [Fact]
        public async Task Translator_Index_ReturnsAViewResult_WithListOfQUeries()
        {
            //Arrange
            var mockRepo = new Mock<ITranslatorRepository>();
            mockRepo.Setup(repo => repo.GetQueriesAsync())
                .ReturnsAsync(GetTestQueries());
            var mockRepo2 = new Mock<ITranslationRepository>();
            mockRepo2.Setup(repo => repo.GetTranslationsAsync())
                .ReturnsAsync(GetTestTranslations());
            var mockService = new Mock<IApiHttpClientService>();
            mockService.Setup(service => service.GetResponseAsync("", ""))
                .ReturnsAsync(GetTestResponse());
            var controller = new TranslatorController(mockService.Object, mockRepo.Object, mockRepo2.Object);

            //Act
            var result = await controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Query>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Translator_Translate_Get_ReturnsAView()
        {
            //Arrange
            var mockRepo = new Mock<ITranslatorRepository>();
            mockRepo.Setup(repo => repo.GetQueriesAsync())
                .ReturnsAsync(GetTestQueries());
            var mockRepo2 = new Mock<ITranslationRepository>();
            mockRepo2.Setup(repo => repo.GetTranslationsAsync())
                .ReturnsAsync(GetTestTranslations());
            var mockService = new Mock<IApiHttpClientService>();
            mockService.Setup(service => service.GetResponseAsync("", ""))
                .ReturnsAsync(GetTestResponse());
            var controller = new TranslatorController(mockService.Object, mockRepo.Object, mockRepo2.Object);

            //Act
            var result = await controller.Translate();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            
        }

        [Fact]
        public async Task Translator_Translate_Post_ReturnsView_WhenModelStateIsInvalid()
        {
            //Arrange
            var mockRepo = new Mock<ITranslatorRepository>();
            mockRepo.Setup(repo => repo.GetQueriesAsync())
                .ReturnsAsync(GetTestQueries());
            var mockRepo2 = new Mock<ITranslationRepository>();
            mockRepo2.Setup(repo => repo.GetTranslationsAsync())
                .ReturnsAsync(GetTestTranslations());
            var mockService = new Mock<IApiHttpClientService>();
            mockService.Setup(service => service.GetResponseAsync("", ""))
                .ReturnsAsync(GetTestResponse());
            var controller = new TranslatorController(mockService.Object, mockRepo.Object, mockRepo2.Object);
            controller.ModelState.AddModelError("Text", "Required");
            controller.ModelState.AddModelError("Id", "Required");
            var viewModel = new TranslatorViewModel();

            //Act
            var result = await controller.Translate(viewModel);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<TranslatorViewModel>(
                viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Translator_Translate_Post_ReturnsRedirectToAction_WhereModelStateIsValid()
        {
            //Arrange
            var mockRepo = new Mock<ITranslatorRepository>();
            mockRepo.Setup(repo => repo.AddResponseAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<SuccessResponse>(), It.IsAny<ErrorResponse>()))
                .Returns(Task.CompletedTask)
                .Verifiable();
            var mockRepo2 = new Mock<ITranslationRepository>();
            mockRepo2.Setup(repo => repo.GetTranslationsAsync())
                .ReturnsAsync(GetTestTranslations());
            var mockService = new Mock<IApiHttpClientService>();
            mockService.Setup(service => service.GetResponseAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<HttpResponseMessage>());
            var controller = new TranslatorController(mockService.Object, mockRepo.Object, mockRepo2.Object);

            var viewModel = new TranslatorViewModel
            {
                Id = 1,
                Text = "test"
            };

            //Act
            var result = await controller.Translate(viewModel);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Details", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }



        private HttpResponseMessage GetTestResponse()
        {
            HttpResponseMessage message = new HttpResponseMessage();
            return message;
        }

        private List<Translation> GetTestTranslations()
        {
            var list = new List<Translation>();
            list.Add(new Translation
            {
                Id = 1,
                Language = "test language"
            });
            return list;
        }

        private List<Query> GetTestQueries()
        {
            //var translation = new Translation
            //{
            //    Id = 1,
            //    Language = "test"
            //};
            var list = new List<Query>();
            list.Add(new Query()
            {
                Id = 1,
                Call = "test",
                Success = true,
                TranslationId = 1
                //Translation = translation
            });

            list.Add(new Query()
            {
                Id = 2,
                Call = "test2",
                Success = false,
                TranslationId = 1
                //Translation = translation
            });

            return list;
        }

        private SuccessResponse GetSuccessResponse()
        {
            return new SuccessResponse
            {
                Id = 1,
                Translation = "test",
                Text = "test",
                Translated = "test",
                QueryId = 1
            };
        }

        private ErrorResponse GetErrorResponse()
        {
            return new ErrorResponse
            {
                Id = 1,
                Code = 400,
                Message = "message",
                QueryId = 2
            };
        }
    }
}
