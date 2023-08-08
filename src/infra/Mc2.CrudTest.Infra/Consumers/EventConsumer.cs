

using Mc2.CrudTest.Core.Domain.Events;
using Mc2.CrudTest.Shared.Consumers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit;
using RawRabbit.Extensions.BulkGet;

namespace Mc2.CrudTest.Infra.Consumers
{
    public class EventConsumer : IEventConsumer
    {
        private readonly IBusClient _busClient;
        private readonly IServiceProvider _serviceProvider;

        public EventConsumer(IBusClient busClient, IServiceProvider serviceProvider)
        {
            _busClient = busClient;
            _serviceProvider = serviceProvider;
        }

        public void Consume(string topic)
        {
            _busClient.SubscribeAsync<CustomerCreatedEvent>(
                async (msg, msgcontext) =>
                {
                    //add logging
                    using var scope = _serviceProvider.CreateScope();
                    var internalBus = scope.ServiceProvider.GetRequiredService<IMediator>();
                    await internalBus.Publish(msg);
                },
                cfg =>
                    cfg.WithExchange(excfg =>
                        {
                            excfg.WithName(topic);
                            excfg.WithType(RawRabbit.Configuration.Exchange.ExchangeType.Topic);
                            excfg.WithArgument("key", "customercreatedevent");
                        })
                        .WithQueue(qcfg =>
                        {
                            qcfg.WithName("customer-query-customercreatedevent");
                        })
            );
        }
    }
}
