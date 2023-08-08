using Mc2.CrudTest.Core.Application.Commands.NewCustomer;
using Mc2.CrudTest.Core.Domain.Aggregates;
using Mc2.CrudTest.Core.Domain.Events;
using Mc2.CrudTest.Core.Domain.Repositories;
using Mc2.CrudTest.Infra.Configs;
using Mc2.CrudTest.Infra.DataAccess;
using Mc2.CrudTest.Infra.Handlers;
using Mc2.CrudTest.Infra.Listener;
using Mc2.CrudTest.Infra.Producers;
using Mc2.CrudTest.Infra.Repositories;
using Mc2.CrudTest.Infra.Store;
using Mc2.CrudTest.Presentation.Server.Filters;
using Mc2.CrudTest.Shared.Domain;
using Mc2.CrudTest.Shared.Events;
using Mc2.CrudTest.Shared.Handlers;
using Mc2.CrudTest.Shared.Infrastructure;
using Mc2.CrudTest.Shared.Listener;
using Mc2.CrudTest.Shared.Producers;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
using RawRabbit.DependencyInjection.ServiceCollection;
using Mc2.CrudTest.Presentation.Shared.Extensions;

namespace Mc2.CrudTest.Presentation;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        BsonClassMap.RegisterClassMap<BaseEvent>();
        BsonClassMap.RegisterClassMap<CustomerCreatedEvent>();


        Action<DbContextOptionsBuilder> configureDbContext =
            o => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
        builder.Services.AddDbContext<DatabaseContext>(configureDbContext);
        builder.Services.AddSingleton(new DatabaseContextFactory(configureDbContext));

        DatabaseContext dataContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
        dataContext.Database.EnsureCreated();

        builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
        builder.Services.AddTransient<IEventStoreRepository, EventStoreRepository>();
        builder.Services.AddTransient<IEventStore, EventStore>();
        builder.Services.AddTransient<IEventProducer, EventProducer>();
        builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
        builder.Services.AddTransient<IEventSourcingHandler<CustomerAggregate>, EventSourcingHandler>();
        
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(NewCustomerCommand).Assembly));

        builder.Services.AddRabbitMQ(builder.Configuration);

        builder.Services.AddSingleton<IStartupFilter, StartupFilter>();

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseBlazorFrameworkFiles();
        app.UseStaticFiles();

        app.UseRouting();


        app.MapRazorPages();
        app.MapControllers();
        app.MapFallbackToFile("index.html");

        app.Run();
    }
}