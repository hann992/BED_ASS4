using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using BED_ASS4;

namespace BED_ASS4.Data;

public class MongoService
{

    private readonly MongoClient _client;

    public MongoClient Client
    {
        get
        {
            return _client;
        }
    }

    public MongoService(string connectionString)
    {
        _client = new MongoClient(connectionString);
    }

    public async Task<bool> IsSeeded(string database)
    {
        var cursor = await _client.GetDatabase(database).ListCollectionsAsync();
        return (await cursor.ToListAsync()).Count > 0;
    }
}