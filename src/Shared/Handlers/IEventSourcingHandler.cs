using Mc2.CrudTest.Shared.Domain;

namespace Mc2.CrudTest.Shared.Handlers;

public interface IEventSourcingHandler<T>
{
    Task SaveAsync(AggregateRoot aggregate);
    Task<T> GetByIdAsync(Guid aggregateId);
}