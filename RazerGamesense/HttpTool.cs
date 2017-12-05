
namespace RGS
{
	using System;
	using System.IO;
	using System.Net.Http;
	using System.Text;
	using System.Threading.Tasks;
	using System.Web.Script.Serialization;

	using Gamesense;

	public class HttpTool
	{
		readonly HttpClient _client;

		public readonly JavaScriptSerializer Serializer;

		public HttpTool()
		{
			Serializer = new JavaScriptSerializer();
			Serializer.RegisterConverters(new JavaScriptConverter[] { new DataContractJavascriptConverter(true) });


			var programDataPath = Environment.GetEnvironmentVariable("PROGRAMDATA");

			if (programDataPath == null)
			{
				Util.WriteLog("FATAL: Could not get PROGRAMDATA folder");
				throw new FileNotFoundException("Could not get PROGRAMDATA folder");
			}

			string cpPath = Path.Combine(programDataPath, "SteelSeries", "SteelSeries Engine 3", "coreProps.json");
			string json = File.ReadAllText(cpPath);
			Util.WriteLog("coreProps.json contents: " + json);

			var cprops = Serializer.Deserialize<CoreProps>(json);
			Util.WriteLog("Deserialized coreprosp");

			_client = new HttpClient { BaseAddress = cprops.GetBaseAddressUri() };
			Util.WriteLog("Done httptool init");
		}

		public async Task<string> PostAsync<T>(string url, T data)
		{
			var req = new HttpRequestMessage(HttpMethod.Post, _client.BaseAddress + url);
			Util.WriteLog("Posting to: " + req.RequestUri);

			using (var content = new StringContent(Serializer.Serialize(data), Encoding.UTF8, "application/json"))
			{
				req.Content = content;

				var response = await _client.SendAsync(req);
				return await response.Content.ReadAsStringAsync();
			}
		}
	}
}
