using Mc2.CrudTest.Shared.Exceptions;

namespace Mc2.CrudTest.Core.Domain.Exceptions;

public class ConcurrencyException : BaseException
{
    public ConcurrencyException(string id) : base($"Concurrency access on aggregateId: {id}")
    {
        Id = id;
    }

    public string Id { get; }
}