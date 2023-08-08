using Mc2.CrudTest.Presentation.Shared.Extensions;

namespace Mc2.CrudTest.Presentation.Server.Filters
{
    public class StartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                app.UseSubscribeAllEvents();
                next(app);
            };
        }
    }
}
