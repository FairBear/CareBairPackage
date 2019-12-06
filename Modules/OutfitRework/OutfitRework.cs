using BepInEx.Configuration;
using BepInEx.Harmony;
using HarmonyLib;

namespace CareBairPackage
{
	public static partial class OutfitRework
	{
		const string SECTION = "Outfit Rework";

		const string DESCRIPTION_KEEP_OUTFIT =
			"Chance to keep their outfit via bathing or closet. " +
			"100% means they will always retain their outfit.";
		const string DESCRIPTION_KEEP_H_OUTFIT =
			"Chance to keep their outfit after the H scene. " +
			"100% means they will always retain their outfit.";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<int> KeepOutfit { get; set; }
		internal static ConfigEntry<int> KeepHOutfit { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "__Enabled", true);

			KeepOutfit = Config.Bind(SECTION, "Keep Outfit", 50, new ConfigDescription(DESCRIPTION_KEEP_OUTFIT, new AcceptableValueRange<int>(0, 100)));
			KeepHOutfit = Config.Bind(SECTION, "Keep Outfit After H", 100, new ConfigDescription(DESCRIPTION_KEEP_H_OUTFIT, new AcceptableValueRange<int>(0, 100)));

			Subscription.Subscribe(typeof(OutfitRework), Enabled, patch: true);
		}
	}
}
