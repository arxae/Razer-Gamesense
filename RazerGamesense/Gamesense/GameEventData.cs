namespace RGS.Gamesense
{
	using System.Collections.Generic;
	using System.Runtime.Serialization;

	[DataContract]
	public class GameEventDataInt
	{
		[DataMember(Name = "hids")]
		public List<int> Hids { get; set; }

		[DataMember(Name = "colors")]
		public List<List<int>> Colors { get; set; }

		public GameEventDataInt()
		{
			Colors = new List<List<int>>();
			Hids = new List<int>();
		}
	}

	[DataContract]
	public class GameEventDataString
	{
		[DataMember(Name = "hids")]
		public List<string> Hids { get; set; }

		[DataMember(Name = "colors")]
		public List<List<int>> Colors { get; set; }

		public GameEventDataString()
		{
			Colors = new List<List<int>>();
			Hids = new List<string>();
		}
	}
}