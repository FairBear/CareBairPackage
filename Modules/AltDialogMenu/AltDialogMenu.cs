﻿using BepInEx.Configuration;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class AltDialogMenu
	{
		const string SECTION = "Alt Dialog Menu";

		const string DESCRIPTION_ENABLED =
			"Creates another conversation dialog that is used for modules. " +
			"Disabling this might make other modules useless. " +
			"You can toggle the visibility of the window by pressing the 'Toggle Visibility Key.'";
		const string DESCRIPTION_SHOW_IMMEDIATELY =
			"When enabled, dialogs will be shown immediately when available. " +
			"Dialogs are hidden at the left side of your screen where only its edge is visible.";
		const string DESCRIPTION_WIN_HEIGHT =
			"Height of the window. Cannot exceed screen height.";
		const string DESCRIPTION_WIN_WIDTH =
			"Width of the window.";

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
			Enabled = Config.AddSetting(SECTION, $"_{SECTION} Enabled", true, DESCRIPTION_ENABLED);
			WindowID = Config.AddSetting(SECTION, $"_{SECTION} Window ID", 7890);

			ShowImmediately = Config.AddSetting(SECTION, $"{SECTION} Show Dialog ASAP", true, DESCRIPTION_SHOW_IMMEDIATELY);
			WinHeight = Config.AddSetting(SECTION, $"{SECTION} Window Height", 300, DESCRIPTION_WIN_HEIGHT);
			WinWidth = Config.AddSetting(SECTION, $"{SECTION} Window Width", 200, DESCRIPTION_WIN_WIDTH);
			ScrollUpKey = Config.AddSetting(SECTION, $"{SECTION} Scroll Up", new KeyboardShortcut(KeyCode.W));
			ScrollDownKey = Config.AddSetting(SECTION, $"{SECTION} Scroll Down", new KeyboardShortcut(KeyCode.S));
			DecreaseKey = Config.AddSetting(SECTION, $"{SECTION} Slider Decrease Value", new KeyboardShortcut(KeyCode.A));
			IncreaseKey = Config.AddSetting(SECTION, $"{SECTION} Slider Increase Value", new KeyboardShortcut(KeyCode.D));
			ToggleKey = Config.AddSetting(SECTION, $"{SECTION} Toggle Visibility Key", new KeyboardShortcut(KeyCode.Tab));

			Subscription.Subscribe(typeof(AltDialogMenu), Enabled, Update, null, OnGUI);
		}
	}
}
