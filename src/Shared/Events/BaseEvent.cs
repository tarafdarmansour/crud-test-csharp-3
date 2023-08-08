using Mc2.CrudTest.Shared.Messages;
using MediatR;

namespace Mc2.CrudTest.Shared.Events;

public abstract class BaseEvent : Message, INotification
{
    protected BaseEvent(string type)
    {
        Type = type;
    }

    public string Type { get; set; }
    public int Version { get; set; }
}