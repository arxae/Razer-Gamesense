namespace RGS.Gamesense
{
	using System.Runtime.Serialization;

	[DataContract]
	public class CoreProps
	{
		[DataMember(Name = "address")]
		public string BaseAddress { get; set; }

		public System.Uri GetBaseAddressUri()
		{
			string addr = BaseAddress;
			if (addr.StartsWith("http") == false)
			{
				addr = "http://" + addr;
			}

			return new System.Uri(addr);
		}
	}
}
