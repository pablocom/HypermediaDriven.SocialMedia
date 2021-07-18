using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using EventStore.ClientAPI;
using HypermediaDriven.SocialMedia.Core;
using Microsoft.AspNetCore.Http.Extensions;
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