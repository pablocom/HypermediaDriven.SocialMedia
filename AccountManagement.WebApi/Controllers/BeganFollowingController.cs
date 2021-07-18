using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.ServiceModel.Syndication;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using EventStore.ClientAPI;
using HypermediaDriven.SocialMedia.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AccountManagement.WebApi.Controllers
{
    [ApiController]
    [Route(RoutingPrefixes.AccountManagement + "/[controller]")]
    public class BeganFollowingController : ControllerBase
    {
        private readonly ILogger<BeganFollowingController> logger;
        private readonly IEventStoreRepository eventStoreRepository;


        public BeganFollowingController(ILogger<BeganFollowingController> logger, IEventStoreRepository eventStoreRepository)
        {
            this.logger = logger;
            this.eventStoreRepository = eventStoreRepository;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> CreateFeed()
        {
            var feedUri = new Uri($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/{RoutingPrefixes.AccountManagement}/BeganFollowing");
            
            var feed = new SyndicationFeed("BeganFollowing", "Began following domain events", feedUri);
            feed.Authors.Add(new SyndicationPerson("p.c.ramirez5150@gmail.com"));
            
            var events = await eventStoreRepository.ReadEventsAsync();
            feed.Items = events.Select(MapToFeedItem);

            // set feed as response - always atom+xml - no HAL
            var feedContent = GetFeedContent(feed);
            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    feedContent,
                    Encoding.UTF8,
                    "application/atom+xml"
                )
            };

            return response;
        }

        private string GetFeedContent(SyndicationFeed feed)
        {
            return JsonSerializer.Serialize(feed);
            
            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter);
            
            feed.GetAtom10Formatter().WriteTo(xmlWriter);

            return stringWriter.ToString();
        }

        private SyndicationItem MapToFeedItem(ResolvedEvent resolvedEvent)
        {
            return new(
                "BeganFollowingEvent",
                Encoding.UTF8.GetString(resolvedEvent.Event.Data),
                new Uri(Request.GetDisplayUrl() + $"/{resolvedEvent}"),
                resolvedEvent.Event.EventId.ToString(),
                DateTime.Now
            );
        }
    }
}