using Mc2.CrudTest.Shared.Listener;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Mc2.CrudTest.Infra.MessageBrokers;

public static class MessageBrokersExtensions
{
    public static IApplicationBuilder UseSubscribeEvent(this IApplicationBuilder app, Type type)
    {
        app.ApplicationServices.GetRequiredService<IEventListener>().Subscribe(type);

        return app;
    }
}