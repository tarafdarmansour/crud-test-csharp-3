using Mc2.CrudTest.Shared.Events;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Mc2.CrudTest.Shared.Producers;
using RawRabbit;

namespace Mc2.CrudTest.Infra.Producers
{
    public class EventProducer : IEventProducer
    {
        private readonly IBusClient _busClient;

        public EventProducer(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task ProduceAsync<T>(string exchangeName, T @event) where T : BaseEvent
        {
            string eventType = @event.GetType().Name;
            var value = JsonSerializer.Serialize(@event, @event.GetType());
            await _busClient.PublishAsync(value,
                cfg => cfg.UsePublishConfiguration(
                    c => c
                        .OnDeclaredExchange(GetExchangeDeclaration(eventType))
                ));
        }
        private Action<RawRabbit.Configuration.Exchange.IExchangeDeclarationBuilder> GetExchangeDeclaration(string name)
        {
            string? exchange = Environment.GetEnvironmentVariable("RABBIT_EXCHANGE");
            return e => e
                .WithName(exchange)
                .WithArgument("key", name);
        }
    }
}
