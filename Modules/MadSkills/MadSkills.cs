using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class MadSkills
	{
		const string SECTION = "Mad Skills";

		const string DESCRIPTION_ENABLED =
			"Allows you to set as many skills as you want. " +
			"You can only give 1 of each skill per character.";

		const string DESCRIPTION_CONSUME_ITEM =
			"When enabled, giving a skill to a girl will remove it from your inventory. " +
			"Removing a skill item will return the skill item to you. " +
			"Disabling this will not remove the item, " +
			"but will still require you to have the skill on your inventory.";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<int> WindowID { get; set; }
		internal static ConfigEntry<bool> ConsumeItem { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "__Enabled", true, DESCRIPTION_ENABLED);
			WindowID = Config.Bind(SECTION, "__Window ID", 7893);

			ConsumeItem = Config.Bind(SECTION, "Consume Item", true, DESCRIPTION_CONSUME_ITEM);

			Subscription.Subscribe(typeof(MadSkills), Enabled, onGUI: OnGUI);
		}
	}
}
