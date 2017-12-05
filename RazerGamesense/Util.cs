using System;
using System.Collections.Generic;
using System.IO;

namespace RGS
{
	public static class Util
	{
		public static bool EmitLogs;

		public static void WriteLog(string msg)
		{
			if (EmitLogs == false) return;
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "RazerInterceptor.log");
			using (var w = new StreamWriter(path, true))
			{
				w.WriteLine($"{DateTime.Now}: {msg}");
			}
		}

		public static List<int> GetKeyLayout()
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "RazerInterceptorKeyLayout.cfg");

			if (File.Exists(path) == false)
			{
				WriteLog("KeyLayout file, loading defaults");
				return KeyboardLayout.HidKeyboardKeys;
			}

			var lines = File.ReadAllLines(path);
			if (lines.Length == 0)
			{
				throw new Exception("KeyLayout file found, but was empty");
			}

			WriteLog($"KeyLayout file found, parsing {lines.Length} line(s)");

			var lst = new List<int>();

			foreach (var line in lines)
			{
				if (line.Trim().Length == 0) continue;

				if (int.TryParse(line.Trim(), out int res))
				{
					lst.Add(res);
				}
				else
				{
					WriteLog("Error parsing key layout file");
					throw new Exception("Error parsing key layout file. Line: " + line);
				}
			}

			return lst;
		}

		public static Dictionary<string, string> GetSettings()
		{
			string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "RazerInterceptor.cfg");

			var cfgDict = new Dictionary<string, string>();
			if (File.Exists(path))
			{
				string[] lines = File.ReadAllLines(path);
				foreach (var l in lines)
				{
					if (string.IsNullOrWhiteSpace(l)) continue;
					string[] split = l.Trim().ToLower().Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
					if (split.Length != 2) continue;
					cfgDict.Add(split[0].Trim(), split[1].Trim());
				}
			}
			else
			{
				cfgDict.Add("emitlogs", "false");
				cfgDict.Add("startkeyoffset", "1");
			}

			return cfgDict;
		}
	}
}
