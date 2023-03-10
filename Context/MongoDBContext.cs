using Microsoft.Extensions.Options;

using MongoDB.Driver;

using mongodb_example.Attribute;
using mongodb_example.Config;
using mongodb_example.Interface;

namespace mongodb_example.Context;

public class MongoDBContext : IMongoDBContext
{
    public IMongoDatabase _database { get; }

    public MongoDBContext(IOptions<MongoDBOptions> options) {
        MongoClient client = new MongoClient(options.Value.ConnectionURI);

        _database = client.GetDatabase(options.Value.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string name)
    {
        return _database.GetCollection<T>(name);
    }
}