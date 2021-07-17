using EventStore.ClientAPI;
using HypermediaDriven.SocialMedia.Core;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AccountManagement.WebApi.Controllers
{
    public interface IEventPersister
    {
        Task PersistEventAsync(object domainEvent);
    }

    public class EventPersister : IEventPersister
    {
        private readonly IEventStoreConnection _connection;
        private readonly ITimeService timeService;

        public EventPersister(ITimeService timeService)
        {
            this.timeService = timeService;
            _connection = EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113"));
            _connection.ConnectAsync().GetAwaiter().GetResult();
        }


        public async Task PersistEventAsync(object domainEvent)
        {
            var eventMetadata = new EventMetadata(timeService.UtcNow, Guid.NewGuid().ToString());

            var eventData = new EventData(
                Guid.NewGuid(),
                "DomainEvent",
                true,
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(domainEvent)),
                Encoding.UTF8.GetBytes(JsonSerializer.Serialize(eventMetadata))
            );
            await _connection.AppendToStreamAsync(nameof(BeganFollowing), ExpectedVersion.Any, eventData);
        }
    }
}