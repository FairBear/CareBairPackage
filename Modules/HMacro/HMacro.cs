using BepInEx.Configuration;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class HMacro
	{
		const string SECTION = "H Macro（H动画快速切换）";

		const string DESCRIPTION_ENABLED =
			//"Switch to different animation categories with hotkeys. " +
			//"Animation is randomly selected within the category.";
			"使用快捷键切换到不同的H动画种类 " +
			"动画是在该种类中随机选择的";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<KeyboardShortcut> CaressKey { get; set; }
		internal static ConfigEntry<KeyboardShortcut> ServiceKey { get; set; }
		internal static ConfigEntry<KeyboardShortcut> IntercourseKey { get; set; }
		internal static ConfigEntry<KeyboardShortcut> SpecialKey { get; set; }
		internal static ConfigEntry<KeyboardShortcut> LesbianKey { get; set; }
		internal static ConfigEntry<KeyboardShortcut> GroupKey { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#是否启用", true, DESCRIPTION_ENABLED);

			CaressKey = Config.Bind(SECTION, /*"Caress"*/"01.爱抚", new KeyboardShortcut(KeyCode.Keypad4));
			ServiceKey = Config.Bind(SECTION, /*"Service"*/"02.侍奉", new KeyboardShortcut(KeyCode.Keypad5));
			IntercourseKey = Config.Bind(SECTION, /*"Intercourse"*/"03.插入", new KeyboardShortcut(KeyCode.Keypad6));
			SpecialKey = Config.Bind(SECTION, /*"Special"*/"04.特殊", new KeyboardShortcut(KeyCode.Keypad1));
			LesbianKey = Config.Bind(SECTION, /*"Lesbian"*/"05.百合", new KeyboardShortcut(KeyCode.Keypad2));
			GroupKey = Config.Bind(SECTION, /*"Group"*/"06.多人", new KeyboardShortcut(KeyCode.Keypad3));

			Subscription.Subscribe(typeof(HMacro), Enabled, Update);
		}
	}
}
