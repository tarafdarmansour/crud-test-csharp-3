using Mc2.CrudTest.Core.Domain.Aggregates;
using Mc2.CrudTest.Shared.Domain;
using Mc2.CrudTest.Shared.Handlers;
using Mc2.CrudTest.Shared.Infrastructure;

namespace Mc2.CrudTest.Infra.Handlers
{
    public class EventSourcingHandler : IEventSourcingHandler<CustomerAggregate>
    {
        private readonly IEventStore _eventStore;

        public EventSourcingHandler(IEventStore eventStore)
        {
            _eventStore = eventStore;
        }

        public async Task<CustomerAggregate> GetByIdAsync(Guid aggregateId)
        {
            var aggregate = new CustomerAggregate();
            var events = await _eventStore.GetEventsAsync(aggregateId);

            if (events == null || !events.Any()) return aggregate;

            aggregate.ReplayEvents(events);
            aggregate.Version = events.Select(x => x.Version).Max();

            return aggregate;
        }

        public async Task SaveAsync(AggregateRoot aggregate)
        {
            await _eventStore.SaveEventsAsync(aggregate.Id, aggregate.GetUncommittedChanges(), aggregate.Version);
            aggregate.MarkChangesAsCommitted();
        }
    }
}