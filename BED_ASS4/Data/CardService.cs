using System.Text.Json;

using MongoDB.Bson;
using MongoDB.Driver;
using BED_ASS4;

namespace BED_ASS4.Data;

public class CardService
{

    private readonly IMongoCollection<Card> _collection;
    private readonly MongoService _service;

    public CardService(MongoService service)
    {
        _service = service;
        _collection = service.Client.GetDatabase("mtg").GetCollection<Card>("cards");
    }

    public async Task<IList<Card>> Search(string? setid = null, string? artist = null, int? page = null)
    {
        var builder = Builders<Card>.Filter;
        var filter = builder.Empty;
        var limit = 50;

        if (setid?.Length > 0)
        {
            filter &= builder.Regex(x => x.SetId, new BsonRegularExpression($"/{setid}/i"));
        }

        if (artist?.Length > 0)
        {
            filter &= builder.Regex(x => x.Artist, new BsonRegularExpression($"/{artist}/i"));
        }

        var result = _collection.Find<Card>(filter);
        var count = await result.CountDocumentsAsync();

        if (await result.CountDocumentsAsync() >= limit)
        {
            result = result.Skip(page * limit).Limit(limit);
        }

        return await result.ToListAsync<Card>();
    }

    public async Task<IList<Card>> GetAllCards()
    {
        return await _collection.Find<Card>(Builders<Card>.Filter.Empty).ToListAsync();
    }

    public async void SeedCards(string[] paths)
    {
        foreach (var path in paths)
        {
            using (var file = new StreamReader(path))
            {
                var cards = JsonSerializer.Deserialize<List<Card>>(file.ReadToEnd(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                await _collection.InsertManyAsync(cards);
            }
        }
    }

    public async Task<bool> IsSeeded()
    {
        return await _service.IsSeeded("mtg");
    }
}