using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace BED_ASS4
{
	public class Card
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string _id { get; set; }
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
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string _id { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		[JsonPropertyName("collectibleCount")]
		public int CardCount { get; set; }
	}




	public class CardType
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string _id { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
	}

	public class Class
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string _id { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
	}


	public class Rarities
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string _id { get; set; }
		public int Id { get; set; }
		public string Name { get; set; }
	}

	// DTO til at få Json filen ind som flere forskellige lister
	public class Metadata
    {
		public List<Set> Sets { get; set; }

		public List<Rarities> Rarities { get; set; }

		public List<Class> Classes { get; set; }

		public List<CardType> Types { get; set; }
    }

	public class CardsDTO
    {
		public int Id { get; set; }
		[JsonPropertyName("class")]
		public string ClassType { get; set; }
		public string Type { get; set; }
		public string Set { get; set; }
		public int? SpellSchoolId { get; set; }
		public string Rarity { get; set; }
		public int? Health { get; set; }
		public int? Attack { get; set; }
		public int ManaCost { get; set; }
		[JsonPropertyName("ArtistName")]
		public string Artist { get; set; }
		public string Text { get; set; }
		public string FlavorText { get; set; }
		public string Name { get; set; }

	}



}