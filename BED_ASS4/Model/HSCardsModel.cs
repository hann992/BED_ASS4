using System.Text.Json.Serialization;

namespace BED_ASS4
{
	public class Card
	{
		public int Id { get; set; }
		public int ClassId { get; set; }
		public int TypeId { get; set; }
		public int SetId { get; set; }
		public int? SpellSchoolId { get; set; }
		public int RarityId { get; set; }
		public int? Health { get; set; }
		public int? Attack { get; set; }
		public int ManaCost { get; set; }
		public String Artist { get; set; }
		public String Text { get; set; }
		public String FlavorText { get; set; }
	}


	public class Set
	{
		public int Id { get; set; }
		public String Name { get; set; }
		public String Type { get; set; }
		[JsonPropertyName("collectibleCount")]
		public int CardCount { get; set; }
	}




	public class CardType
	{
		public int Id { get; set; }
		public String Name { get; set; }
	}

	public class Class
	{
		public int Id { get; set; }
		public String Name { get; set; }
	}


	public class Rarity
	{
		public int Id { get; set; }
		public String Name { get; set; }
	}
}