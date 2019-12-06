using BepInEx.Configuration;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class ProfileInspector
	{
		const string SECTION = "Profile Inspector";

		const string DESCRIPTION_ENABLED =
			"Exposes some relevant profiles within the game's resources. " +
			"Requires a restart when disabling/enabling.";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<int> WindowID { get; set; }
		internal static ConfigEntry<KeyboardShortcut> Key { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "__Enabled", true, DESCRIPTION_ENABLED);
			WindowID = Config.Bind(SECTION, "__Window ID", 7893);

			Key = Config.Bind(SECTION, "Toggle Key", new KeyboardShortcut(KeyCode.Keypad7));

			Subscription.Subscribe(typeof(RunKey), Enabled, Update, null, OnGUI);
		}
	}
}
