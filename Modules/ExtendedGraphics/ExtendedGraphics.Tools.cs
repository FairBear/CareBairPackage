using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CareBairPackage
{
	public static partial class ExtendedGraphics
	{
		static T Get<T>(this Dictionary<string, T> list, string key, T defaultValue)
		{
			if (list.ContainsKey(key))
				return list[key];

			return defaultValue;
		}

		static void Save()
		{
			csv = "";

			foreach (KeyValuePair<string, float> pair in numState)
				csv += $"0{pair.Key}={pair.Value},";

			foreach (KeyValuePair<string, bool> pair in boolState)
				csv += $"1{pair.Key}={(pair.Value ? 1 : 0)},";

			foreach (KeyValuePair<string, string> pair in stringState)
			{
				float.TryParse(pair.Value, out float val);

				csv += $"2{pair.Key}={val},";
			}

			if (csv.Length > 0)
				csv = csv.Substring(0, csv.Length - 1);

			File.WriteAllText(SavePath, csv);
		}

		static void Load()
		{
			numState.Clear();
			boolState.Clear();
			stringState.Clear();

			if (!File.Exists(SavePath))
				return;

			csv = File.ReadAllText(SavePath);

			if (csv == null || csv.Length == 0)
				return;

			foreach (string pair in Regex.Split(csv, ","))
			{
				if (pair.Length == 0)
					continue;

				int i = pair.IndexOf('=');

				if (i == -1)
					continue;

				char n = pair[0];

				switch (n)
				{
					case '0':
						if (int.TryParse(pair.Substring(i + 1), out int vint))
							numState[pair.Substring(1, i - 1)] = vint;
						break;

					case '1':
						if (int.TryParse(pair.Substring(i + 1), out int vbool))
							boolState[pair.Substring(1, i - 1)] = vbool != 0;
						break;

					case '2':
						stringState[pair.Substring(1, i - 1)] = pair.Substring(i + 1);
						break;
				}
			}
		}
	}
}
