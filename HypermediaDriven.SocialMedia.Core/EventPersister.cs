using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace HypermediaDriven.SocialMedia.Core
{
    public class EventPersister : IEventPersister
    {
        private readonly IEventStoreConnection connection;

        public EventPersister()
        {
            var connectionSettings = ConnectionSettings.Create()
                .DisableTls().DisableServerCertificateValidation().EnableVerboseLogging();
            connection = EventStoreConnection.Create(connectionSettings, new Uri("tcp://admin:changeit@localhost:1113"));
            connection.ConnectAsync().GetAwaiter().GetResult();
        }

        public async Task PersistEventAsync<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IEvent
        {
            var serializedEvent = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(domainEvent));
            var eventData = new EventData(Guid.NewGuid(), domainEvent.GetType().Name, true, serializedEvent, new byte[0]);

            await connection.AppendToStreamAsync("DomainEvent", ExpectedVersion.Any, eventData).ConfigureAwait(false);
        }
    }
}