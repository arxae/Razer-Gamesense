namespace RGS.Gamesense
{
	using System.Runtime.Serialization;

	[DataContract]
	public class GameEvent
	{
		[DataMember(Name = "game")]
		public string Game { get; set; }

		[DataMember(Name = "event")]
		public string Event { get; set; }

		[DataMember(Name = "data")]
		public GameEventDataInt DataInt { get; set; }
	}
}