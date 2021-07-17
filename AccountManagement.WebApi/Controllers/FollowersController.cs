using AccountManagement.WebApi.Model;
using AccountManagement.WebApi.Representations;
using HypermediaDriven.SocialMedia.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.WebApi.Controllers
{
    [ApiController]
    [Route(RoutingPrefixes.AccountManagement + "accounts/{accountId:int}/[controller]")]
    public class FollowersController : ControllerBase
    {
        private readonly ILogger<FollowersController> _logger;
        private readonly IEventPersister eventPersister;

        public FollowersController(ILogger<FollowersController> logger, IEventPersister eventPersister)
        {
            _logger = logger;
            this.eventPersister = eventPersister;
        }

        [HttpGet]
        public FollowersRepresentation GetFollowers(int accountId)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}/{RoutingPrefixes.AccountManagement}";

            return new FollowersRepresentation(
                new Link(baseUrl + $"accounts/{accountId}/followers", "self"),
                new[] { new Link(baseUrl + $"accounts/{accountId}/followers?page=2", "self") },
                ReadFollowers(accountId)
            );
        }

        [HttpPost("{accountIdToStartFollowing:int}")]
        public async Task<IActionResult> BeginFollowing(int accountId, int accountIdToStartFollowing)
        {
            var domainEvent = new BeganFollowing(accountId, accountIdToStartFollowing);

            await eventPersister.PersistEventAsync(domainEvent);
            return RedirectToAction(nameof(GetFollowers), new { accountId });
        }

        private IEnumerable<Follower> ReadFollowers(int accountId)
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

    public record BeganFollowing(int FollowerAccountId, int FollowedAccountId) : IEvent;
}