using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class UnrestrictedH
	{
		const string SECTION = "Unrestricted H";

		const string DESCRIPTION_ENABLED =
			"Changes the behavior and restrictions during H scene.";
		const string DESCRIPTION_ALL_H_POS =
			"Removes restrictions for H positions and H points. " +
			"Selecting lesbian positions with only 1 girl or " +
			"group positions with only 2 people will break the H scene. " +
			"Leaving the H scene will fix it.";
		const string DESCRIPTION_VISIBLE_AUTO_H =
			"Forces the auto H toggle to be visible.";
		const string DESCRIPTION_REAL_TIME =
			"Allows non-participants to move and act during H scene. " +
			"This will not make them visible. " +
			"This includes the merchant.";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<bool> AllHPos { get; set; }
		internal static ConfigEntry<bool> VisibleAutoH { get; set; }
		internal static ConfigEntry<bool> RealTime { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "__Enabled", true, DESCRIPTION_ENABLED);

			AllHPos = Config.Bind(SECTION, "All H Positions", true, DESCRIPTION_ALL_H_POS);
			VisibleAutoH = Config.Bind(SECTION, "Auto H Always Visible", true, DESCRIPTION_VISIBLE_AUTO_H);
			RealTime = Config.Bind(SECTION, "Dont Freeze Non-Participants", true, DESCRIPTION_REAL_TIME);

			Subscription.Subscribe(typeof(UnrestrictedH), Enabled, patch: true);
		}
	}
}
