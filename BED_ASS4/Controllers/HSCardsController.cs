using BED_ASS4.Data;
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

        private readonly CardService _cardService;

        public HSCardsController(ILogger<HSCardsController> logger, CardService cardService)
        {
            _logger = logger;
            _cardService = cardService;
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
        public async Task<IList<Set>> GetSets()
        {
            _logger.LogInformation("API: Getting all Sets list");
            // Skal returner ALLE sets
            return await _cardService.GetAllSets();
        }

        
        [HttpGet("/rarities")]
        public async Task<IList<Rarities>> GetAllRarities()
        {
            _logger.LogInformation("API: Getting all Rarities list");
            // Skal returner ALLE sets
            return await _cardService.GetAllRarities();
        }


        [HttpGet("/classes")]
        public async Task<IList<Class>> GetAllClasses()
        {
            _logger.LogInformation("API: Getting all Classes list");
            // Skal returner ALLE sets
            return await _cardService.GetAllClasses();
        }


        [HttpGet("/types")]
        public async Task<IList<CardType>> GetAllTypes()
        {
            _logger.LogInformation("API: Getting all Types list");
            // Skal returner ALLE sets
            return await _cardService.GetAllTypes();
        }
    }
}