using Mc2.CrudTest.Shared.Events;

namespace Mc2.CrudTest.Shared.Producers;

public interface IEventProducer
{
    Task ProduceAsync<T>(string exchangeName, T @event) where T : BaseEvent;
}