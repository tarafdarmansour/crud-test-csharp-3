using Mc2.CrudTest.Shared.Exceptions;

namespace Mc2.CrudTest.Core.Domain.Exceptions;

public class AggregateNotFoundException : BaseException
{
    public AggregateNotFoundException(string id) : base($"{id} is invalid for AggregateRootId")
    {
        Id = id;
    }

    public string Id { get; }
}