using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace HypermediaDriven.SocialMedia.Core
{
    public class EventStoreRepository : IEventStoreRepository
    {
        private readonly IEventStoreConnection connection;

        public EventStoreRepository()
        {
            var connectionSettings = ConnectionSettings.Create()
                .DisableTls().DisableServerCertificateValidation().EnableVerboseLogging();
            connection = EventStoreConnection.Create(connectionSettings, new Uri("tcp://admin:changeit@localhost:1113"));
            connection.ConnectAsync().GetAwaiter().GetResult();
        }

        public async Task PersistEventAsync<TEvent>(TEvent @event) where TEvent : IEvent
        {
            var serializedEvent = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(@event));
            var eventData = new EventData(
                Guid.NewGuid(), 
                @event.GetType().Name, 
                true, 
                serializedEvent, 
                Array.Empty<byte>()
            );

            await connection.AppendToStreamAsync("TEST", ExpectedVersion.Any, eventData).ConfigureAwait(false);
        }

        public async Task<IEnumerable<TEvent>> ReadEventsAsync<TEvent>() where TEvent : IEvent 
        {
            var readResult = await connection.ReadStreamEventsBackwardAsync("TEST", 0, 20, false);
            return readResult.Events.Select(Deserialize<TEvent>);
        }

        private static TEvent Deserialize<TEvent>(ResolvedEvent resolvedEvent) where TEvent : IEvent
        {
            var serializedEvent = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
            var domainEvent = JsonSerializer.Deserialize<TEvent>(serializedEvent);
            if (domainEvent is null)
                throw new SerializationException($"Cannot deserialize event of type {typeof(TEvent).Name}");
            
            return domainEvent;
        }
    }
}