using BepInEx.Configuration;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class ExtendedGraphics
	{
		const string SECTION = "Extended Graphics（画面设置扩展）";

		const string DESCRIPTION_ENABLED =
			//"Allows you to edit some hidden graphics options in the game.";
			"允许你编辑一些游戏内被隐藏的画面设置选项";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<int> WindowID { get; set; }
		internal static ConfigEntry<KeyboardShortcut> Key { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#1是否启用", true, DESCRIPTION_ENABLED);
			WindowID = Config.Bind(SECTION, "#2Window ID", 620938);

			Key = Config.Bind(SECTION, /*"Toggle Key",*/"01.快捷键", new KeyboardShortcut(KeyCode.O));

			Subscription.Subscribe(typeof(ExtendedGraphics), Enabled, Update, null, OnGUI, true);

			Load();
		}
	}
}
