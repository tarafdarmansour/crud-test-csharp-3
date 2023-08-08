using Mc2.CrudTest.Infra.Listener;
using Mc2.CrudTest.Infra.MessageBrokers;
using Mc2.CrudTest.Shared.Listener;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.DependencyInjection.ServiceCollection;
using RawRabbit.Instantiation;

namespace Mc2.CrudTest.Presentation.Shared.Extensions;

public static class RabbitMQExtensions
{
    public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration Configuration)
    {
        RabbitMQOptions options = new RabbitMQOptions();
        Configuration.GetSection(nameof(MessageBrokersOptions)).Bind(options);
        services.Configure<RabbitMQOptions>(Configuration.GetSection(nameof(MessageBrokersOptions)));

        services.AddRawRabbit(new RawRabbitOptions
        {
            ClientConfiguration = options
        });

        services.AddSingleton<IEventListener, RabbitMQListener>();

        return services;
    }
}