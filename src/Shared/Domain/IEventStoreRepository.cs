using Mc2.CrudTest.Shared.Events;

namespace Mc2.CrudTest.Shared.Domain;

public interface IEventStoreRepository
{
    Task SaveAsync(EventModel @event);
    Task<List<EventModel>> FindByAggregateId(Guid aggregateId);
}