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
        public async Task<IList<CardsDTO>> GetAsync(int? typeid = null, int? setid = null, int? classid = null, int? rarityid = null, string? artist = null, int? page = null)
        {

            var searchResults = await _cardService.Search(setid, classid, rarityid, typeid, artist, page);

            List<CardsDTO> cardsReturned = new List<CardsDTO>();

            foreach(var card in searchResults)
            {
                cardsReturned.Add( await _cardService.GetCardWithMeta(card) );
            }

            _logger.LogInformation("API: Getting Card search with {0} results", cardsReturned.Count());

            return cardsReturned;
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