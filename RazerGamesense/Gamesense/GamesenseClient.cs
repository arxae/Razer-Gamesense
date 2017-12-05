namespace RGS.Gamesense
{
	using System;
	using System.Collections.Generic;
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
				Data = new GameEventData()
			};

			for (int i = 0; i < KeyboardLayout.HidKeyboardKeys.Count; i++)
			{
				if (keyIdLayout[i] == 0)
				{
					gEvent.Data.Colors.Add(new List<int> {255, 255, 255});
					continue;
				}

				var c = colList[i];
				gEvent.Data.Colors.Add(new List<int> { c.Red, c.Green, c.Blue });
			}

			gEvent.Data.Hids = keyIdLayout;
			Util.WriteLog("Sending COLORPUSH event:");
			Util.WriteLog(http.Serializer.Serialize(gEvent));
			Util.WriteLog(" ");

			http.PostAsync("game_event", gEvent).ConfigureAwait(true);
		}
	}
}
