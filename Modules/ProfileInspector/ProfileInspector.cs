using BepInEx.Configuration;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class ProfileInspector
	{
		const string SECTION = "Profile Inspector（游戏数据导出）";

		const string DESCRIPTION_ENABLED =
			//"Exposes some relevant profiles within the game's resources. " +
			//"Requires a restart when disabling/enabling.";
			"导出游戏数据到UserData/CBP_ProfileInspector.csv文件中" +
			"禁用/启用该功能时需要重新启动游戏";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<int> WindowID { get; set; }
		internal static ConfigEntry<KeyboardShortcut> Key { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#1是否启用", true, DESCRIPTION_ENABLED);
			WindowID = Config.Bind(SECTION, "#2Window ID", 7893);

			Key = Config.Bind(SECTION, /*"Toggle Key"*/"01.快捷键", new KeyboardShortcut(KeyCode.Keypad7));

			Subscription.Subscribe(typeof(ProfileInspector), Enabled, Update, null, OnGUI);
		}
	}
}
