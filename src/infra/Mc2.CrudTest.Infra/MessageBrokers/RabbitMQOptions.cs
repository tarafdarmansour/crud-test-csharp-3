using RawRabbit.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infra.MessageBrokers
{
    public class RabbitMQOptions : RawRabbitConfiguration
    {
        public QueueOptions Queue { get; set; }
        public ExchangeOptions Exchange { get; set; }
    }

    public class QueueOptions : GeneralQueueConfiguration
    {
        public string Name { get; set; }
    }

    public class ExchangeOptions : GeneralExchangeConfiguration
    {
        public string Name { get; set; }
    }
}
