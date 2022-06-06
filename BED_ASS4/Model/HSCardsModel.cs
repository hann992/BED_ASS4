using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BED_ASS4
{
	public class Card
	{

		public int Id { get; set; }
		[JsonPropertyName("ClassId")]
		public int ClassId { get; set; }
		[JsonPropertyName("cardTypeId")]
		public int TypeId { get; set; }
		[JsonPropertyName("cardSetId")]
		public int SetId { get; set; }
		[JsonPropertyName("SpellSchoolId")]
		public int? SpellSchoolId { get; set; }
		[JsonPropertyName("RarityId")]
		public int RarityId { get; set; }
		public int? Health { get; set; }
		public int? Attack { get; set; }
		[JsonPropertyName("ManaCost")]
		public int ManaCost { get; set; }
		[JsonPropertyName("ArtistName")]
		public string Artist { get; set; }
		public string Text { get; set; }
		public string FlavorText { get; set; }
		public string Name { get; set; }
	}


	public class Set
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		[JsonPropertyName("collectibleCount")]
		public int CardCount { get; set; }
	}




	public class CardType
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class Class
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}


	public class Rarities
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class Metadata
    {
		public List<Set> Sets { get; set; }

		public List<Rarities> Rarities { get; set; }

		public List<Class> Classes { get; set; }

		public List<CardType> Types { get; set; }
    }

}