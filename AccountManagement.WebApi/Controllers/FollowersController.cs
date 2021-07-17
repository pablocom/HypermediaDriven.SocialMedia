using System.Collections.Generic;
using AccountManagement.WebApi.Model;
using AccountManagement.WebApi.Representations;
using HypermediaDriven.SocialMedia.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccountManagement.WebApi.Controllers
{
    [ApiController]
    [Route(RoutingPrefixes.AccountManagement + "accounts/{accountId}/[controller]")]
    public class FollowersController : ControllerBase
    {
        private readonly ILogger<FollowersController> _logger;

        public FollowersController(ILogger<FollowersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public FollowersRepresentation GetFollowers(string accountId)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}/{RoutingPrefixes.AccountManagement}";
            
            return new FollowersRepresentation(
                new Link(baseUrl + $"accounts/{accountId}/followers", "self"),
                new[] {new Link(baseUrl + $"accounts/{accountId}/followers?page=2", "self")},
                ReadFollowers(accountId)
            );
        }

        private IEnumerable<Follower> ReadFollowers(string accountId)
        {
            _logger.LogInformation($"Reading followers for account {accountId}");
            
            return new[]
            {
                new Follower("User1"),
                new Follower("User2"),
                new Follower("User3")
            };
        }
    }
}