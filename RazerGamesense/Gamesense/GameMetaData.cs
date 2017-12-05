namespace RGS.Gamesense
{
	using System.Runtime.Serialization;

	[DataContract]
	public class GameMetaData
	{
		[DataMember(Name = "game")]
		public string GameName;
		[DataMember(Name = "game_display_name")]
		public string GameDisplayName;
		[DataMember(Name = "icon_color_id")]
		public int IconColorId;

		public static GameMetaData GetRgsRegistration()
		{
			return new GameMetaData
			{
				GameName = "RAZERGAMESENSE",
				GameDisplayName = "Razer - Gamesense",
				IconColorId = 4
			};
		}
	}
}