using System.Text.Json;

using MongoDB.Bson;
using MongoDB.Driver;
using BED_ASS4;

namespace BED_ASS4.Data;

public class CardService
{

    private readonly IMongoCollection<Card> _collection;
    private readonly IMongoCollection<Set> _SetCollection;
    private readonly IMongoCollection<Class> _ClassCollection;
    private readonly IMongoCollection<CardType> _TypeCollection;
    private readonly IMongoCollection<Rarities> _RaritiesCollection;
    
    private readonly MongoService _service;

    public CardService(MongoService service)
    {
        _service = service;
        _collection = service.Client.GetDatabase("mtg").GetCollection<Card>("cards");
        _SetCollection = service.Client.GetDatabase("mtg").GetCollection<Set>("sets");
        _ClassCollection = service.Client.GetDatabase("mtg").GetCollection<Class>("classes");
        _TypeCollection = service.Client.GetDatabase("mtg").GetCollection<CardType>("types");
        _RaritiesCollection = service.Client.GetDatabase("mtg").GetCollection<Rarities>("rarities");
    }

    public async Task<IList<Card>> Search(int? setid = null, int? classid = null, int? rarityid = null, int? typeid = null, string? artist = null, int? page = null)
    {
        var builder = Builders<Card>.Filter;
        var filter = builder.Empty;
        var limit = 100;

        if (classid != null)
        {
            filter &= builder.Eq(x => x.ClassId, classid);
        }

        if (typeid != null)
        {
            filter &= builder.Eq(x => x.TypeId, typeid);
        }

        if (rarityid != null)
        {
            filter &= builder.Eq(x => x.RarityId, rarityid);
        }

        if (setid != null)
        {
            filter &= builder.Eq(x => x.SetId, setid);
        }

        if (artist?.Length > 0)
        {
            filter &= builder.Regex(x => x.Artist, new BsonRegularExpression($"/{artist}/i"));
        }

        var result = _collection.Find<Card>(filter);
        var count = await result.CountDocumentsAsync();


        // Hvis Page er brugt, så laver vi pagination, ellers gør vi ikke.
        if (page != null)
        {
            if (await result.CountDocumentsAsync() >= limit)
            {
                result = result.Skip(page * limit).Limit(limit);
            }
        }

        return await result.ToListAsync();
    }

    public async Task<CardsDTO> GetCardWithMeta(Card card)
    {

        CardsDTO newCard = new CardsDTO();

        newCard.Attack = card.Attack;
        newCard.Health = card.Health;
        newCard.Name = card.Name;
        newCard.FlavorText = card.FlavorText;
        newCard.Id = card.Id;
        newCard.SpellSchoolId = card.SpellSchoolId;
        newCard.Artist = card.Artist;
        newCard.ManaCost = card.ManaCost;

        // Erstatning af IDs med deres Names:
        newCard.Set = _SetCollection.Find<Set>(x => x.Id == card.SetId).FirstOrDefault().Name;
        newCard.ClassType = _ClassCollection.Find<Class>(x => x.Id == card.ClassId).FirstOrDefault().Name;
        newCard.Rarity = _RaritiesCollection.Find<Rarities>(x => x.Id == card.RarityId).FirstOrDefault().Name;
        newCard.Type = _TypeCollection.Find<CardType>(x => x.Id == card.TypeId).FirstOrDefault().Name;



        return newCard;
    }

    public async Task<IList<Set>> GetAllSets()
    {

        return await _SetCollection.Find<Set>(Builders<Set>.Filter.Empty).ToListAsync();
    }

    public async Task<IList<Class>> GetAllClasses()
    {

        return await _ClassCollection.Find<Class>(Builders<Class>.Filter.Empty).ToListAsync();
    }

    public async Task<IList<Rarities>> GetAllRarities()
    {

        return await _RaritiesCollection.Find<Rarities>(Builders<Rarities>.Filter.Empty).ToListAsync();
    }

    public async Task<IList<CardType>> GetAllTypes()
    {

        return await _TypeCollection.Find<CardType>(Builders<CardType>.Filter.Empty).ToListAsync();
    }


    public async void SeedCards()
    {
        // Oprettelse af Json filstier
        string[] JsonPaths = { "cards.json", "metadata.json" };

        // Læg Metadata filen ind i MongoDB
        using (var file = new StreamReader(JsonPaths[1]))
        {
            var metadata = JsonSerializer.Deserialize<Metadata>(file.ReadToEnd(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            await _RaritiesCollection.InsertManyAsync(metadata.Rarities.ToList());
            await _SetCollection.InsertManyAsync(metadata.Sets.ToList());
            await _ClassCollection.InsertManyAsync(metadata.Classes.ToList());
            await _TypeCollection.InsertManyAsync(metadata.Types.ToList());
        }

        // Læg kort filen ind i MongoDB
        using (var file = new StreamReader(JsonPaths[0]))
        {
            var cards = JsonSerializer.Deserialize<List<Card>>(file.ReadToEnd(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            await _collection.InsertManyAsync(cards);
        }



    }

    public async Task<bool> IsSeeded()
    {
        return await _service.IsSeeded("mtg");
    }
}