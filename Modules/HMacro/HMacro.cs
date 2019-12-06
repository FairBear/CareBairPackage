using BepInEx.Configuration;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class HMacro
	{
		const string SECTION = "H Macro";

		const string DESCRIPTION_ENABLED =
			"Switch to different animation categories with hotkeys. " +
			"Animation is randomly selected within the category.";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<KeyboardShortcut> CaressKey { get; set; }
		internal static ConfigEntry<KeyboardShortcut> ServiceKey { get; set; }
		internal static ConfigEntry<KeyboardShortcut> IntercourseKey { get; set; }
		internal static ConfigEntry<KeyboardShortcut> SpecialKey { get; set; }
		internal static ConfigEntry<KeyboardShortcut> LesbianKey { get; set; }
		internal static ConfigEntry<KeyboardShortcut> GroupKey { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "__Enabled", true, DESCRIPTION_ENABLED);

			CaressKey = Config.Bind(SECTION, "Caress", new KeyboardShortcut(KeyCode.Keypad4));
			ServiceKey = Config.Bind(SECTION, "Service", new KeyboardShortcut(KeyCode.Keypad5));
			IntercourseKey = Config.Bind(SECTION, "Intercourse", new KeyboardShortcut(KeyCode.Keypad6));
			SpecialKey = Config.Bind(SECTION, "Special", new KeyboardShortcut(KeyCode.Keypad1));
			LesbianKey = Config.Bind(SECTION, "Lesbian", new KeyboardShortcut(KeyCode.Keypad2));
			GroupKey = Config.Bind(SECTION, "Group", new KeyboardShortcut(KeyCode.Keypad3));

			Subscription.Subscribe(typeof(HMacro), Enabled, Update);
		}
	}
}
