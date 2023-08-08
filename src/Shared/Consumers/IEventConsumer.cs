namespace Mc2.CrudTest.Shared.Consumers;

public interface IEventConsumer
{
    void Consume(string topic);
}