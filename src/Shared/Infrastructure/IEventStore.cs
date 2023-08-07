using Mc2.CrudTest.Shared.Events;

namespace Mc2.CrudTest.Shared.Infrastructure;

public interface IEventStore
{
    Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion);
    Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId);
}