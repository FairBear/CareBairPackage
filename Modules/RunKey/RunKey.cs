using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class RunKey
	{
		const string SECTION = "Run Key";

		const string DESCRIPTION_ENABLED =
			"Allows you to move faster with a toggle key.";
		const string DESCRIPTION_RUN_FORCE =
			"This is multiplied to the player character's forward force. " +
			"Setting this to 0 will give no additional speed.";
		const string DESCRIPTION_TOGGLE_KEY =
			"Pressing this key will change the player's move speed. " +
			"Speed depends on the 'Run Multiplier' value.";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<float> RunMultiplier { get; set; }
		internal static ConfigEntry<KeyboardShortcut> ToggleKey { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "__Enabled", true, DESCRIPTION_ENABLED);

			RunMultiplier = Config.Bind(SECTION, "Run Mutliplier", 2f, DESCRIPTION_RUN_FORCE);
			ToggleKey = Config.Bind(SECTION, "Toggle Key", new KeyboardShortcut(UnityEngine.KeyCode.LeftAlt), DESCRIPTION_TOGGLE_KEY);

			Subscription.Subscribe(typeof(RunKey), Enabled, Update);
		}
	}
}
