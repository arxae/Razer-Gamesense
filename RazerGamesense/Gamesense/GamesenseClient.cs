namespace RGS.Gamesense
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using System.Linq;
	using Razer;

	public class GamesenseClient
	{
		static HttpTool http;
		static Dictionary<string, string> Config;

		static List<int> keyIdLayout;

		internal static void Initialize()
		{
			Config = Util.GetSettings();
			Util.EmitLogs = Config["emitlogs"] == "true";

			Util.WriteLog("### RAZER -> GAMESENSE ###");

			keyIdLayout = Util.GetKeyLayout();

			AppDomain.CurrentDomain.UnhandledException += (s, e) =>
			{
				Util.WriteLog($"[EXCEPTION] - {((Exception)e.ExceptionObject).Message}");
			};

			http = new HttpTool();

			Util.WriteLog("Refreshing SSE registration");
			http.PostAsync("remove_game", new { game = GameMetaData.GetRgsRegistration().GameName }).ConfigureAwait(true);
			http.PostAsync("game_metadata", GameMetaData.GetRgsRegistration()).ConfigureAwait(true);

			Util.WriteLog("Done");
		}

		internal static void ReplicateKeyboardEffect(Keyboard.EFFECT_TYPE effect, Keyboard.CUSTOM_EFFECT_TYPE customEffectType)
		{
			Util.WriteLog("Incoming effect: " + http.Serializer.Serialize(customEffectType));
			var colList = Colors.RGBA.FromUintArray(customEffectType.Color);

			Util.WriteLog("Creating new event");
			var gEvent = new GameEvent
			{
				Game = GameMetaData.GetRgsRegistration().GameName,
				Event = "COLORPUSH",
				DataInt = new GameEventDataInt()
			};

			for (int i = 0; i < KeyboardLayout.HidKeyboardKeys.Count; i++)
			{
				if (KeyboardLayout.HidKeyboardKeys[i] == 0)
				{
					gEvent.DataInt.Colors.Add(new List<int> {255, 255, 255});
					continue;
				}

				var c = colList[i];
				gEvent.DataInt.Colors.Add(new List<int> { c.Red, c.Green, c.Blue });
			}

			gEvent.DataInt.Hids = KeyboardLayout.HidKeyboardKeys;

			Util.WriteLog("Sending COLORPUSH event:");
			Util.WriteLog(http.Serializer.Serialize(gEvent));
			Util.WriteLog(" ");

			http.PostAsync("game_event", gEvent).ConfigureAwait(true);
		}

		static uint[] OrderByKeyCode(uint[] uintColorArray)
		{
			var outputDict = new Dictionary<int, uint>();
			Keyboard.RZKEY currentrzkey = Keyboard.RZKEY.A;
			try
			{
				for (int i = 0; i < uintColorArray.Length; i++)
				{
					currentrzkey = (Keyboard.RZKEY)i;
					Enum.TryParse(currentrzkey.ToString(), out HidKeys hidkey);
					int index = (int)hidkey;
					if (outputDict.ContainsKey(index) == false)
					{
						outputDict.Add(index, uintColorArray[i]);
					}
				}
			}
			catch (Exception ex)
			{
				Util.WriteLog("Exception while processing rzkey: " + currentrzkey);
				Util.WriteLog(ex.Message);
				Util.WriteLog(ex.StackTrace);
			}

			return outputDict
				.OrderBy(val => val.Key)
				.Select(val => val.Value)
				.ToArray();
		}
	}
}
