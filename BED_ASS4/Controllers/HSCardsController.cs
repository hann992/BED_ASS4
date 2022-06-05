using Microsoft.AspNetCore.Mvc;

namespace BED_ASS4.Controllers
{
    [ApiController]
    public class HSCardsController : ControllerBase
    {
        public int Page { get; set; }

        public int SetId { get; set; }

        public string Artist { get; set; }

        public int ClassId { get; set; }

        public int RarityId { get; set; }


        private readonly ILogger<HSCardsController> _logger;

        public HSCardsController(ILogger<HSCardsController> logger)
        {
            _logger = logger;
        }

        
        [HttpGet("/cards")]
        public IEnumerable<Card> Get(int? page,  int? setid, string? artist, int? classid, int? rarityid)
        {
            _logger.LogInformation("API: Performing search, Requesting page: {page}, id: {setid}, artist: {artist}", page, setid, artist, classid, rarityid);

            // Hvis ingen side valgt, så side 1!
            if(page != null)
            {
                Page = (int)page;
            }
            else
            {
                Page = 1;
            }

            // Hvis brugt, så sæt...
            if (setid != null)
            {
                SetId = (int)setid;
            }

            if (artist != null)
            {
                Artist = (string)artist;
            }

            if (classid != null)
            {
                ClassId = (int)classid;
            }

            if (rarityid != null)
            {
                RarityId = (int)rarityid;
            }

            // Returner input, som test...
            return Enumerable.Range(1, 1).Select(index => new Card
            {
                Id = SetId,
                Artist = Artist,
                ClassId = ClassId,
                RarityId = RarityId,

            })
            .ToArray();
        }


        [HttpGet("/sets")]
        public List<Set> GetSets()
        {

            // Skal returner ALLE sets
            return new List<Set>()
            {
                new Set() { Id = 1, CardCount = 99, Name = "Det vilde set!", Type = "dude" },
            };
        }

        
        [HttpGet("/rarities")]
        public List<Rarity> GetRarity()
        {

            // Skal returner ALLE rarities
            return new List<Rarity>()
            {
                new Rarity() { Id = 1, Name = "Superrare" },
            };
        }

        
        [HttpGet("/classes")]
        public List<Class> GetClasses()
        {

            // Skal returner ALLE sets
            return new List<Class>()
            {
                new Class(){ Id = 1, Name = "Klassekort"},
            };
        }

        
        [HttpGet("/types")]
        public List<CardType> GetTypes()
        {

            // Skal returner ALLE sets
            return new List<CardType>()
            {
                new CardType(){ Id = 1, Name = "Typeofcard..." },
            };
        }
    }
}