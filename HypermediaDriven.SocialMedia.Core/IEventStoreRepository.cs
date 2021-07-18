using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace HypermediaDriven.SocialMedia.Core
{
    public interface IEventStoreRepository
    {
        Task PersistEventAsync<TEvent>(TEvent @event) where TEvent : IEvent;
        Task<IEnumerable<ResolvedEvent>> ReadEventsAsync();
    }
}