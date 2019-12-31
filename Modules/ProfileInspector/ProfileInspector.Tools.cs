using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CareBairPackage
{
	public static partial class ProfileInspector
	{
		static ProfileEntry Entry(object target,
								  string label,
								  string property,
								  string key,
								  Type type,
								  Action act = null)
		{
			return entries[key] = new ProfileEntry(
				target,
				label,
				property,
				key,
				new Traverse(target).Field(property).GetValue(),
				type,
				act
			);
		}

		static void Batch<T>(object target, string prefix, params string[] properties)
		{
			foreach (string property in properties)
				Entry(
					target,
					$"{prefix}\n{property}",
					property,
					$"{prefix} {property}",
					typeof(T)
				).Invoke();
		}

		static void Struct<T>(object target, Action act, string prefix, params string[] properties)
		{
			foreach (string property in properties)
				Entry(
					target,
					$"{prefix}\n{property}",
					property,
					$"{prefix} {property}",
					typeof(T),
					act
				);

			act();
		}

		static void SetValue(object target, string property, object value)
		{
			new Traverse(target).Field(property).SetValue(value);
		}

		static void Save()
		{
			string csv = "";

			foreach (KeyValuePair<string, ProfileEntry> entry in entries)
				if (entry.Value.value != entry.Value.defaultValue)
					csv += $"{entry.Key}={entry.Value.value},";

			File.WriteAllText(SavePath, csv);
		}

		static void Load()
		{
			save = new Dictionary<string, string>();
			string path = SavePath;

			if (!File.Exists(path))
				return;

			string csv = File.ReadAllText(path);

			if (csv == null || csv.Length == 0)
				return;

			foreach (string pair in Regex.Split(csv, ","))
			{
				if (pair.Length == 0)
					continue;

				int i = pair.IndexOf('=');

				if (i == -1)
					continue;

				save[pair.Substring(0, i)] = pair.Substring(i + 1);
			}
		}
	}
}
