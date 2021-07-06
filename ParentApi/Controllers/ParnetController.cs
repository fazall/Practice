using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ParentApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParnetController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        //public ParnetController()
        //{
        //    HttpClientFactory = httpClientFactory;
        //}
        private readonly ILogger<ParnetController> _logger;

        public IHttpClientFactory HttpClientFactory { get; }

        public ParnetController(ILogger<ParnetController> logger, IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var client = HttpClientFactory.CreateClient("ErrorApp");
            var getInfo = await client.GetAsync("/WeatherForecast");

            return JsonConvert.DeserializeObject<string[]>(await getInfo.Content.ReadAsStringAsync());
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    Date = DateTime.Now.AddDays(index),
            //    TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //})
            //.ToArray();
        }
    }
}
