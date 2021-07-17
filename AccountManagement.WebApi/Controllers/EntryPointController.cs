using System.Collections.Generic;
using HypermediaDriven.SocialMedia.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccountManagement.WebApi.Controllers
{
    [ApiController]
    [Route(RoutingPrefixes.AccountManagement + "accountManagement/[controller]")]
    public class EntryPointController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public EntryPointController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public ActionResult Get()
        {
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/";

            return Ok(new Links
            {
                Self = new Link
                {
                    Href = baseUrl + "accountManagement", 
                    Rel = "self",
                },
                Accounts = new List<Link>
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
        
        public class Links
        {
            public Link Self { get; init; }
            public IEnumerable<Link> Accounts { get; init; }
        };
    }
}