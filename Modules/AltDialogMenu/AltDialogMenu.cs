using BepInEx.Configuration;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class AltDialogMenu
	{
		const string SECTION = "Alt Dialog Menu（快捷菜单）";

		const string DESCRIPTION_ENABLED =
			//"Creates another conversation dialog that is used for modules. " +
			//"Disabling this might make other modules useless. " +
			//"You can toggle the visibility of the window by pressing the 'Toggle Visibility Key.'";
			"所有快捷菜单的依赖选项\n" +
			"如果禁用它，会导致所有快捷菜单不可用\n" +
			"您可以通过按“隐藏/显示 快捷菜单键”来呼出和隐藏快捷菜单窗口。'";
		const string DESCRIPTION_SHOW_IMMEDIATELY =
			//"When enabled, dialogs will be shown immediately when available. " +
			//"Dialogs are hidden at the left side of your screen where only its edge is visible.";
			"设置快捷菜单的默认显示状态：显示/隐藏\n" +
			"设为显示时，窗口会在可用时直接显示，但有时会挡住游戏UI\n" +
			"设为隐藏时，窗口会在可用时缩小在屏幕边缘，以免挡住游戏UI";
		const string DESCRIPTION_WIN_HEIGHT =
			//"Height of the window. Cannot exceed screen height.";
			"快捷菜单的窗口高度，该高度不能超过屏幕高度";
		const string DESCRIPTION_WIN_WIDTH =
			//"Width of the window.";
			"快捷菜单的窗口宽度";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<int> WindowID { get; set; }
		internal static ConfigEntry<bool> ShowImmediately { get; set; }
		internal static ConfigEntry<int> WinHeight { get; set; }
		internal static ConfigEntry<int> WinWidth { get; set; }
		internal static ConfigEntry<KeyboardShortcut> ScrollUpKey { get; set; }
		internal static ConfigEntry<KeyboardShortcut> ScrollDownKey { get; set; }
		internal static ConfigEntry<KeyboardShortcut> DecreaseKey { get; set; }
		internal static ConfigEntry<KeyboardShortcut> IncreaseKey { get; set; }
		internal static ConfigEntry<KeyboardShortcut> ToggleKey { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#1是否启用", true, DESCRIPTION_ENABLED);
			WindowID = Config.Bind(SECTION, "#2Window ID", 7890);
			ShowImmediately = Config.Bind(SECTION,/* "Show Dialog ASAP"*/"01.默认窗口状态", true, DESCRIPTION_SHOW_IMMEDIATELY);
			WinHeight = Config.Bind(SECTION, /*"Window Height"*/"02.快捷菜单的高度", 300, DESCRIPTION_WIN_HEIGHT);
			WinWidth = Config.Bind(SECTION, /*"Window Width"*/"03.快捷菜单的宽度", 200, DESCRIPTION_WIN_WIDTH);
			ScrollUpKey = Config.Bind(SECTION,/* "Scroll Up"*/"04.选项上移", new KeyboardShortcut(KeyCode.W));
			ScrollDownKey = Config.Bind(SECTION,/* "Scroll Down"*/"05.选项下移", new KeyboardShortcut(KeyCode.S));
			DecreaseKey = Config.Bind(SECTION, /*"Slider Decrease Value"*/"06.滑块值左移", new KeyboardShortcut(KeyCode.A));
			IncreaseKey = Config.Bind(SECTION, /*"Slider Increase Value"*/"07.滑块值右移(同时也是选项确定键)", new KeyboardShortcut(KeyCode.D));
			ToggleKey = Config.Bind(SECTION, /*"Toggle Visibility Key"*/"08.隐藏/显示 菜单键", new KeyboardShortcut(KeyCode.Tab));

			Subscription.Subscribe(typeof(AltDialogMenu), Enabled, Update, null, OnGUI);
		}
	}
}
