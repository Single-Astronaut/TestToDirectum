using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Patagames.Ocr;
using Patagames.Ocr.Enums;
using TestToDirectum.Models;
using System.Text.Json;

namespace TestToDirectum.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecognitionController : ControllerBase
    {
        private readonly ILogger<RecognitionController> _logger;

        public RecognitionController(ILogger<RecognitionController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Метод распознавания текста на изображении
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetText(string path)
        {
            try
            {
                var answer = new AnswerModel();

                using (var api = OcrApi.Create())
                {
                    api.Init(Languages.English, "./");
                    answer.OutText = api.GetTextFromImage(path);
                }

                return JsonSerializer.Serialize(answer);
            }
            catch(Exception exception)
            {
                _logger.LogError("Ошибка выполнения GetText {exception}", exception);
                return null;
            }
        }
    }
}
