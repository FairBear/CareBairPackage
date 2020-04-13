using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class MadSkills
	{
		const string SECTION = "Mad Skills（更好的技能系统）";

		const string DESCRIPTION_ENABLED =
			//"Allows you to set as many skills as you want. " +
			//"You can only give 1 of each skill per character.";
			"允许你设置你想要的技能\n" +
			"你只能给每个角色一个技能";

		const string DESCRIPTION_CONSUME_ITEM =
			//"When enabled, giving a skill to a girl will remove it from your inventory. " +
			//"Removing a skill item will return the skill item to you. " +
			//"Disabling this will not remove the item, " +
			//"but will still require you to have the skill on your inventory.";
			"当启用时，给予女孩技能将从你的库存中移除" +
			"移除一个技能项目将会返回该技能项目给你" +
			"禁用此功能不会删除该物品，"+
			"但仍然需要你的拥有这个技能才能把它给予女孩";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<int> WindowID { get; set; }
		internal static ConfigEntry<bool> ConsumeItem { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#1是否启用", true, DESCRIPTION_ENABLED);
			WindowID = Config.Bind(SECTION, "#2Window ID", 7893);

			ConsumeItem = Config.Bind(SECTION, /*"Consume Item"*/"消耗物品", true, DESCRIPTION_CONSUME_ITEM);

			Subscription.Subscribe(typeof(MadSkills), Enabled, onGUI: OnGUI);
		}
	}
}
