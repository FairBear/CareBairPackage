using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class UnrestrictedH
	{
		const string SECTION = "Unrestricted H";

		const string DESCRIPTION_ENABLED =
			"Allows you to use any kind of position on any location. " +
			"Selecting lesbian positions with only 1 girl or " +
			"group positions with only 2 people will break the H scene. " +
			"Leaving the H scene will fix it.";

		internal static ConfigEntry<bool> Enabled { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.AddSetting(SECTION, $"_{SECTION} Enabled", true, DESCRIPTION_ENABLED);

			Subscription.Subscribe(typeof(UnrestrictedH), Enabled, patch: true);
		}
	}
}
