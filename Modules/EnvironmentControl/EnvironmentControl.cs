using BepInEx.Configuration;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class EnvironmentControl
	{
		const string SECTION = "Environment Control（环境控制）";

		const string DESCRIPTION_ENABLED =
			//"Allows you to control the time and weather.";
			"允许你控制时间和天气";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<int> WindowID { get; set; }
		internal static ConfigEntry<KeyboardShortcut> Key { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#1是否启用", true, DESCRIPTION_ENABLED);
			WindowID = Config.Bind(SECTION, "#2Window ID", 696969);

			Key = Config.Bind(SECTION, /*"Toggle Key"*/"01.快捷键", new KeyboardShortcut(KeyCode.F10));

			Subscription.Subscribe(typeof(EnvironmentControl), Enabled, Update, null, OnGUI);
		}
	}
}
