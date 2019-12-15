using BepInEx.Configuration;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class EnvironmentControl
	{
		const string SECTION = "Environment Control";

		const string DESCRIPTION_ENABLED =
			"Allows you to control the time and weather.";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<int> WindowID { get; set; }
		internal static ConfigEntry<KeyboardShortcut> Key { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "__Enabled", true, DESCRIPTION_ENABLED);
			WindowID = Config.Bind(SECTION, "__Window ID", 696969);

			Key = Config.Bind(SECTION, "Toggle Key", new KeyboardShortcut(KeyCode.F10));

			Subscription.Subscribe(typeof(EnvironmentControl), Enabled, Update, null, OnGUI);
		}
	}
}
