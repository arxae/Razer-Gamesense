namespace RGS.Gamesense
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	[DataContract]
	public class GameEventData
	{
		[DataMember(Name = "hids")]
		public List<int> Hids { get; set; }

		[DataMember(Name = "colors")]
		public List<List<int>> Colors { get; set; }

		public GameEventData()
		{
			Colors = new List<List<int>>();
			Hids = new List<int>();
		}
	}
}