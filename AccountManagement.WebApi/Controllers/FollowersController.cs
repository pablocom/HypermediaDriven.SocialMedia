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
    [Route(RoutingPrefixes.AccountManagement + "/accounts/{accountId:int}/[controller]")]
    public class FollowersController : ControllerBase
    {
        private readonly ILogger<FollowersController> _logger;
        private readonly IEventStoreRepository eventStoreRepository;

        public FollowersController(ILogger<FollowersController> logger, IEventStoreRepository eventStoreRepository)
        {
            _logger = logger;
            this.eventStoreRepository = eventStoreRepository;
        }

        [HttpGet]
        public FollowersRepresentation GetFollowers(int accountId)
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}/{RoutingPrefixes.AccountManagement}";

            return new FollowersRepresentation(
                new Link(baseUrl + $"/accounts/{accountId}/followers", "self"),
                new[] { new Link(baseUrl + $"/accounts/{accountId}/followers?page=2", "self") },
                ReadFollowers(accountId)
            );
        }

        [HttpPost("{accountIdToStartFollowing:int}")]
        public async Task<IActionResult> BeginFollowing(int accountId, int accountIdToStartFollowing)
        {
            var domainEvent = new BeganFollowing(accountId, accountIdToStartFollowing);

            await eventStoreRepository.PersistEventAsync(domainEvent);
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

    public class BeganFollowing : IEvent
    {
        public int FollowerAccountId { get; set; }

        public int FollowedAccountId { get; set; }

        public BeganFollowing(int followerAccountId, int followedAccountId)
        {
            this.FollowerAccountId = followerAccountId;
            this.FollowedAccountId = followedAccountId;
        }
    }
}