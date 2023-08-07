using Mc2.CrudTest.Infra.Configs;
using Mc2.CrudTest.Shared.Domain;
using Mc2.CrudTest.Shared.Events;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Mc2.CrudTest.Infra.Repositories;

public class EventStoreRepository : IEventStoreRepository
{
    private readonly IMongoCollection<EventModel> _eventStoreCollection;

    public EventStoreRepository(IOptions<MongoDbConfig> config)
    {
        MongoClient mongoClient = new MongoClient(config.Value.ConnectionString);
        IMongoDatabase? mongoDatabase = mongoClient.GetDatabase(config.Value.Database);

        _eventStoreCollection = mongoDatabase.GetCollection<EventModel>(config.Value.Collection);
    }

    public async Task<List<EventModel>> FindByAggregateId(Guid aggregateId)
    {
        return await _eventStoreCollection.Find(x => x.AggregateIdentifier == aggregateId).ToListAsync()
            .ConfigureAwait(false);
    }

    public async Task SaveAsync(EventModel @event)
    {
        await _eventStoreCollection.InsertOneAsync(@event).ConfigureAwait(false);
    }
}