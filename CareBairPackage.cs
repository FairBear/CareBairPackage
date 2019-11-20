using AIProject;
using BepInEx;
using BepInEx.Configuration;
using System;
using UnityEngine;

namespace CareBairPackage
{
	[BepInPlugin(GUID, Name, Version)]
	public partial class CareBairPackage : BaseUnityPlugin
	{
		const string GUID = "com.fairbair.carebairpackage";
		const string Name = "Care Bair Package";
		const string Version = "1.1.2";

		const string SECTION = "_General";

		const string DESCRIPTION_NOTIFY_CRASH =
			"Send a message on the bottom-right side of the screen when a module crashes.";

		internal static ConfigEntry<bool> NotifyCrash { get; set; }

		private void Awake()
		{
			if (Application.productName != "AI-Syoujyo")
				return;

			NotifyCrash = Config.AddSetting(SECTION, $"Notify In-Game on Crash", true, DESCRIPTION_NOTIFY_CRASH);

			AltDialogMenu.Awake(Config);
			ClothState.Awake(Config);
			FasterWarp.Awake(Config);
			HMacro.Awake(Config);
			OutfitRework.Awake(Config);
			PanningHelper.Awake(Config);
			UIMacro.Awake(Config);
			UnrestrictedH.Awake(Config);
		}

		private void Update()
		{
			Subscription.Update();
		}

		private void LateUpdate()
		{
			Subscription.LateUpdate();
		}

		private void OnGUI()
		{
			Subscription.OnGUI();
		}

		internal static void InitSetting<T>(ConfigEntry<T> entry, Action setter)
		{
			setter();

			entry.SettingChanged += (sender, args) => setter();
		}

		internal static void Log(Type source, string text)
		{
			string prefix = $"[{Name} {Version}]";
			Debug.Log($"{prefix} {text}");

			if (NotifyCrash.Value && MapUIContainer.IsInstance())
				MapUIContainer.AddNotify($"{prefix} '{source.Name}' crashed! Disabling then enabling will reload it.");
		}
	}
}
