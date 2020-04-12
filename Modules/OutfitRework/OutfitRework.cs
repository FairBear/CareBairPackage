using BepInEx.Configuration;
using BepInEx.Harmony;
using HarmonyLib;

namespace CareBairPackage
{
	public static partial class OutfitRework
	{
		const string SECTION = "Outfit Rework（洗澡、H后服装保留）";

		const string DESCRIPTION_KEEP_OUTFIT =
			//"Chance to keep their outfit via bathing or closet. " +
			//"100% means they will always retain their outfit.";
			"洗澡或者换衣服时，有概率不换服装\n" +
			"100%意味着会一直穿着她们的服装";
		const string DESCRIPTION_KEEP_H_OUTFIT =
			//"Chance to keep their outfit after the H scene. " +
			//"100% means they will always retain their outfit.";
			"有机会在H之后她们不会去换服装\n" +
			"100%意味着会一直穿着她们的服装";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<int> KeepOutfit { get; set; }
		internal static ConfigEntry<int> KeepHOutfit { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#是否启用", true);

			KeepOutfit = Config.Bind(SECTION, /*"Keep Outfit"*/"01.洗澡、换衣服装保留概率", 50, new ConfigDescription(DESCRIPTION_KEEP_OUTFIT, new AcceptableValueRange<int>(0, 100)));
			KeepHOutfit = Config.Bind(SECTION, /*"Keep Outfit After H"*/"02.H后装保留概率", 100, new ConfigDescription(DESCRIPTION_KEEP_H_OUTFIT, new AcceptableValueRange<int>(0, 100)));

			Subscription.Subscribe(typeof(OutfitRework), Enabled, patch: true);
		}
	}
}
