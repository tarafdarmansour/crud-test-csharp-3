using Mc2.CrudTest.Core.Domain.Aggregates;
using Mc2.CrudTest.Core.Domain.Exceptions;
using Mc2.CrudTest.Shared.Domain;
using Mc2.CrudTest.Shared.Events;
using Mc2.CrudTest.Shared.Infrastructure;
using Mc2.CrudTest.Shared.Producers;

namespace Mc2.CrudTest.Infra.Store;

public class EventStore : IEventStore
{
    private readonly IEventStoreRepository _eventStoreRepository;
    private readonly IEventProducer _eventProducer;

    public EventStore(IEventStoreRepository eventStoreRepository, IEventProducer eventProducer)
    {
        _eventStoreRepository = eventStoreRepository;
        _eventProducer = eventProducer;
    }

    public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
    {
        List<EventModel>? eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

        if (eventStream == null || !eventStream.Any())
            throw new AggregateNotFoundException(aggregateId.ToString());

        return eventStream.OrderBy(x => x.Version).Select(x => x.EventData).ToList();
    }

    public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
    {
        List<EventModel> eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

        if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
            throw new ConcurrencyException(aggregateId.ToString());

        int version = expectedVersion;

        foreach (BaseEvent @event in events)
        {
            version++;
            @event.Version = version;
            string eventType = @event.GetType().Name;
            EventModel eventModel = new()
            {
                TimeStamp = DateTime.Now,
                AggregateIdentifier = aggregateId,
                AggregateType = nameof(CustomerAggregate),
                Version = version,
                EventType = eventType,
                EventData = @event
            };
            await _eventStoreRepository.SaveAsync(eventModel);

            var topic = Environment.GetEnvironmentVariable("RABBIT_EXCHANGE");
            await _eventProducer.ProduceAsync(topic, @event, eventType);
        }
    }
}