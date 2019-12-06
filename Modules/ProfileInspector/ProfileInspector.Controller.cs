using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class ProfileInspector
	{
		const string SAVENAME = "CBP_ProfileInspector.csv";

		const float MARGIN_TOP = 20f;
		const float MARGIN_BOTTOM = 10f;
		const float MARGIN_LEFT = 10f;
		const float MARGIN_RIGHT = 10f;
		const float WIDTH = 400f;
		const float HEIGHT = 400f;
		const float INNER_WIDTH = WIDTH - MARGIN_LEFT - MARGIN_RIGHT;
		const float INNER_HEIGHT = HEIGHT - MARGIN_TOP - MARGIN_BOTTOM;

		static Rect rect = new Rect(Screen.width - WIDTH, (Screen.height - HEIGHT) / 2, WIDTH, HEIGHT);
		static Rect innerRect = new Rect(MARGIN_LEFT, MARGIN_TOP, INNER_WIDTH, INNER_HEIGHT);
		static Rect dragRect = new Rect(0f, 0f, WIDTH, 20f);
		static Vector2 scroll = new Vector2();

		static Dictionary<string, ProfileEntry> entries;
		static Dictionary<string, string> save;
		static bool toggle = false;

		static string SavePath => Path.Combine(Directory.GetCurrentDirectory(), "UserData", SAVENAME);

		public static void Update()
		{
			if (Key.Value.IsDown())
				toggle = !toggle;
		}

		public static void OnGUI()
		{
			if (!Manager.Resources.IsInstance())
				return;

			if (entries == null)
			{
				entries = new Dictionary<string, ProfileEntry>();

				Load();
				AgentProfile();
				LocomotionProfile();
				PlayerProfile();
				StatusProfile();
			}

			if (!toggle)
				return;

			rect = GUI.Window(
				WindowID.Value,
				rect,
				Draw,
				"Profile Inspector"
			);
		}

		public static void Draw(int id)
		{
			GUI.DragWindow(dragRect);
			GUILayout.BeginArea(innerRect);
			{
				GUILayout.BeginVertical();
				{
					scroll = GUILayout.BeginScrollView(scroll);
					{
						foreach (KeyValuePair<string, ProfileEntry> entry in entries)
							entry.Value.Draw();
					}
					GUILayout.EndScrollView();
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndArea();
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
