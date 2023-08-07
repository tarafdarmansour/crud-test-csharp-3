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

        public async Task ProduceAsync<T>(string exchangeName, T @event, string eventType) where T : BaseEvent
        {
            var value = JsonSerializer.Serialize(@event, @event.GetType());
            await _busClient.PublishAsync(value, Guid.NewGuid(),
                cfg =>
                {
                    cfg.WithExchange(excfg =>
                            {
                                excfg.WithDurability(true);
                                excfg.WithName(exchangeName);
                            });
                });
        }
    }
}
