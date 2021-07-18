using System.Threading.Tasks;
using HypermediaDriven.SocialMedia.Core;
using Microsoft.AspNetCore.Mvc;

namespace AccountManagement.WebApi.Controllers
{
    [ApiController]
    [Route(RoutingPrefixes.AccountManagement + "/[controller]")]
    public class BeganFollowingController : ControllerBase
    {
        private readonly IEventStoreRepository eventStoreRepository;
        
        public BeganFollowingController(IEventStoreRepository eventStoreRepository)
        {
            this.eventStoreRepository = eventStoreRepository;
        }

        [HttpGet]
        public async Task<IActionResult> CreateFeed()
        {
            var resolvedEvents = await eventStoreRepository.ReadEventsAsync<BeganFollowing>();
            return Ok(resolvedEvents);
        }
    }
}