
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace testePdf.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        [HttpGet, Route("test")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        //[Authorize]
        public async Task<IActionResult> TestDocument([FromBody] string base64)
        {
            byte[] documentInByte = Convert.FromBase64String(base64);
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(documentInByte, 0, documentInByte.Length);
            memoryStream.Position = 0;
            PdfReader reader = new PdfReader(memoryStream);
            var text = string.Empty;

            ITextExtractionStrategy its = new SimpleTextExtractionStrategy();
            string s = PdfTextExtractor.GetTextFromPage(reader, reader.NumberOfPages, its);
            return Ok(s);
        }
    }
}
