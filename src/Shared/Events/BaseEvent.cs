using Mc2.CrudTest.Shared.Messages;

namespace Mc2.CrudTest.Shared.Events;

public abstract class BaseEvent : Message
{
    protected BaseEvent(string type)
    {
        Type = type;
    }

    public string Type { get; set; }
    public int Version { get; set; }
}