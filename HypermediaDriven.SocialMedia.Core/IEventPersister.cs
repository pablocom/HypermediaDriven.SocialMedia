using System.Threading.Tasks;

namespace HypermediaDriven.SocialMedia.Core
{
    public interface IEventPersister
    {
        Task PersistEventAsync<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : IEvent;
    }
}