using Mc2.CrudTest.Shared.Listener;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Configuration.Exchange;

namespace Mc2.CrudTest.Infra.Listener;

public class RabbitMQListener : IEventListener
{
    private readonly IBusClient _busClient;
    private readonly IServiceScopeFactory _serviceFactory;

    public RabbitMQListener(
        IBusClient busClient,
        IServiceScopeFactory serviceFactory)
    {
        _busClient = busClient;
        _serviceFactory = serviceFactory;
    }

    public void Subscribe(Type type)
    {
        _busClient.SubscribeAsync(
            (Func<INotification, Task>)(async msg =>
            {
                using (IServiceScope scope = _serviceFactory.CreateScope())
                {
                    IMediator? eventBus = scope.ServiceProvider.GetService<IMediator>();
                    await eventBus.Publish(msg);
                }
            }),
            cfg => cfg.UseSubscribeConfiguration(
                c => c
                    .OnDeclaredExchange(GetExchangeDeclaration(type))
                    .FromDeclaredQueue(
                        q => q.WithName(AppDomain.CurrentDomain.FriendlyName.Trim().Trim('_') + "_" + type.Name))
            )
        );
    }

    private Action<IExchangeDeclarationBuilder> GetExchangeDeclaration(Type type)
    {
        string name = GetTypeName(type);

        return GetExchangeDeclaration(name);
    }

    private Action<IExchangeDeclarationBuilder> GetExchangeDeclaration(string name)
    {
        string? exchange = Environment.GetEnvironmentVariable("RABBIT_EXCHANGE");

        return e => e
            .WithName(exchange)
            .WithArgument("key", name);
    }

    private static string GetTypeName(Type type)
    {
        string name = type.FullName.ToLower().Replace("+", ".");

        if (type is INotification) name += "_event";

        return name;
    }
}