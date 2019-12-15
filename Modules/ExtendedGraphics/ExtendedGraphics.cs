using BepInEx.Configuration;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class ExtendedGraphics
	{
		const string SECTION = "Extended Graphics";

		const string DESCRIPTION_ENABLED =
			"Allows you to edit some hidden graphics options in the game.";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<int> WindowID { get; set; }
		internal static ConfigEntry<KeyboardShortcut> Key { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "__Enabled", true, DESCRIPTION_ENABLED);
			WindowID = Config.Bind(SECTION, "__Window ID", 620938);

			Key = Config.Bind(SECTION, "Toggle Key", new KeyboardShortcut(KeyCode.O));

			Subscription.Subscribe(typeof(ExtendedGraphics), Enabled, Update, null, OnGUI, true);

			Load();
		}
	}
}
