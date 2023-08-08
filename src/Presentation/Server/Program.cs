using Mc2.CrudTest.Core.Application.Commands.NewCustomer;
using Mc2.CrudTest.Core.Domain.Aggregates;
using Mc2.CrudTest.Core.Domain.Events;
using Mc2.CrudTest.Core.Domain.Repositories;
using Mc2.CrudTest.Infra.Configs;
using Mc2.CrudTest.Infra.Consumers;
using Mc2.CrudTest.Infra.DataAccess;
using Mc2.CrudTest.Infra.Handlers;
using Mc2.CrudTest.Infra.Producers;
using Mc2.CrudTest.Infra.Repositories;
using Mc2.CrudTest.Infra.Store;
using Mc2.CrudTest.Shared.Consumers;
using Mc2.CrudTest.Shared.Domain;
using Mc2.CrudTest.Shared.Events;
using Mc2.CrudTest.Shared.Handlers;
using Mc2.CrudTest.Shared.Infrastructure;
using Mc2.CrudTest.Shared.Producers;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
using RawRabbit.Extensions.Client;

namespace Mc2.CrudTest.Presentation
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            BsonClassMap.RegisterClassMap<BaseEvent>();
            BsonClassMap.RegisterClassMap<CustomerCreatedEvent>();

            Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
            builder.Services.AddDbContext<DatabaseContext>(configureDbContext);
            builder.Services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));

            var dataContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
            dataContext.Database.EnsureCreated();
            
            builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
            builder.Services.AddTransient<IEventStoreRepository, EventStoreRepository>();
            builder.Services.AddTransient<IEventStore, EventStore>();
            builder.Services.AddTransient<IEventProducer, EventProducer>();
            builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
            builder.Services.AddTransient<IEventConsumer, EventConsumer>();
            builder.Services.AddTransient<IEventSourcingHandler<CustomerAggregate>, EventSourcingHandler>();
            builder.Services.AddHostedService<ConsumerHostedService>();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(NewCustomerCommand).Assembly));
            builder.Services.AddRawRabbit(
                cfg => cfg.AddJsonFile(
                    builder.Environment.EnvironmentName == "Production" ? "rabbit.Production.json" : "rabbit.Development.json")
            );
            var app = builder.Build();

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
}