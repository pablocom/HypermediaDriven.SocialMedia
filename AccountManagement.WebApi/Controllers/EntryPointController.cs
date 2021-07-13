using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccountManagement.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EntryPointController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public EntryPointController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public ActionResult<EndpointInfo> Get()
        {
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";
            return Ok(new EndpointInfo
            {
                Href = baseUrl + "accountManagement", 
                Rel = "self",
                Links = new List<Link>
                {
                    new() {Href = baseUrl + "accountManagement", Rel = "accounts"}
                }
            });
        }

        public class Link
        {
            public string Href { get; init; }
            public string Rel { get; init; }
        }
        
        public class EndpointInfo
        {
            public string Href { get; init; }
            public string Rel { get; init; }
            public IEnumerable<Link> Links { get; init; }
        };
    }
}