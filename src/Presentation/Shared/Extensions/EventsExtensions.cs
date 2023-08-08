using System.Reflection;
using Mc2.CrudTest.Core.Domain.Events;
using Mc2.CrudTest.Infra.MessageBrokers;
using Mc2.CrudTest.Shared.Events;
using MediatR;
using Microsoft.AspNetCore.Builder;

namespace Mc2.CrudTest.Presentation.Shared.Extensions;

public static class EventsExtensions
{
    public static IApplicationBuilder UseSubscribeAllEvents(this IApplicationBuilder app)
    {
        IEnumerable<Type> types = typeof(CustomerCreatedEvent).GetTypeInfo().Assembly.GetTypes()
            .Where(mytype => mytype.GetInterfaces().Contains(typeof(INotification)));

        foreach (Type type in types) app.UseSubscribeEvent(type);

        return app;
    }
}