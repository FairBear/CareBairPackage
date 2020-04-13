using AIProject;
using BepInEx;
using BepInEx.Configuration;
using System;
using UnityEngine;

namespace CareBairPackage
{
	[BepInPlugin(GUID, Name, Version)]
	[BepInProcess("AI-Syoujyo")]
	public partial class CareBairPackage : BaseUnityPlugin
	{
		const string GUID = "com.fairbair.carebairpackage";
		const string Name = "Care Bair Package（GenesisAN@ZOD汉化）";
		const string Version = "1.4.3";

		const string SECTION = "#General（Care Bair Package设置）";

		const string DESCRIPTION_NOTIFY_CRASH =
			"Send a message on the bottom-right side of the screen when a module crashes.";

		internal static ConfigEntry<bool> NotifyCrash { get; set; }

		private void Awake()
		{
			NotifyCrash = Config.Bind(SECTION, $"Notify In-Game on Crash", true, DESCRIPTION_NOTIFY_CRASH);

			AgentInspector.Awake(Config);
			AltDialogMenu.Awake(Config);
			HOverhaul.Awake(Config);
			MadSkills.Awake(Config);
			ClothState.Awake(Config);
			EnvironmentControl.Awake(Config);
			ExtendedGraphics.Awake(Config);
			FasterWarp.Awake(Config);
			HMacro.Awake(Config);
			OutfitRework.Awake(Config);
			PanningHelper.Awake(Config);
			ProfileInspector.Awake(Config);
			RunKey.Awake(Config);
			Translator.Awake(Config);
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
		
		internal static void LogError(Type source, string text)
		{
			string prefix = $"[{Name}]";

			Debug.LogError($"{prefix}\n{text}");

			if (NotifyCrash.Value && MapUIContainer.IsInstance())
				MapUIContainer.AddNotify($"{prefix} '{source.Name}' crashed! Disabling then enabling will reload it.");
		}
	}
}
