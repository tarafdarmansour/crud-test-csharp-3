using Mc2.CrudTest.Shared.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Mc2.CrudTest.Infra.Consumers;

public class ConsumerHostedService : IHostedService
{
    private readonly ILogger<ConsumerHostedService> _logger;
    private readonly IServiceProvider _serviceProvider;

    public ConsumerHostedService(ILogger<ConsumerHostedService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event Consumer Service running.");

        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            IEventConsumer eventConsumer = scope.ServiceProvider.GetRequiredService<IEventConsumer>();
            string? topic = Environment.GetEnvironmentVariable("RABBIT_EXCHANGE");

            Task.Run(() => eventConsumer.Consume(topic), cancellationToken);
        }

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Event Consumer Service Stopped");

        return Task.CompletedTask;
    }
}