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
        Task PersistEventAsync<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IEvent;
    }

    public class EventPersister : IEventPersister
    {
        private readonly IEventStoreConnection _connection;

        public EventPersister()
        {
            var connectionSettings = ConnectionSettings.Create()
                .DisableTls().DisableServerCertificateValidation().EnableVerboseLogging();
            _connection = EventStoreConnection.Create(connectionSettings, new Uri("tcp://admin:changeit@localhost:1113"));
            _connection.ConnectAsync().GetAwaiter().GetResult();
        }

        public async Task PersistEventAsync<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IEvent
        {
            var serializedEvent = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(domainEvent));
            var eventData = new EventData(Guid.NewGuid(), domainEvent.GetType().Name, true, serializedEvent, new byte[0]);

            await _connection.AppendToStreamAsync("DomainEvent", ExpectedVersion.Any, eventData).ConfigureAwait(false);
        }
    }
}