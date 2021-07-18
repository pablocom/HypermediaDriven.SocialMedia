using HypermediaDriven.SocialMedia.Core;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.WebApi.Controllers
{
    [ApiController]
    [Route(RoutingPrefixes.AccountManagement + "/[controller]")]
    public class EntryPointController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{RoutingPrefixes.AccountManagement}";

            return Ok(new Representation(
                new Link(baseUrl, "self"),
                new Link[]
                {
                    new(baseUrl + "/accounts", "accounts"),
                    new(baseUrl + "/BeganFollowing", "BeganFollowing")
                })
            );
        }
    }
}