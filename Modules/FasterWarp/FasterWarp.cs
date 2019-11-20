using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class FasterWarp
	{
		const string SECTION = "Faster Warp";

		const string DESCRIPTION_ENABLED =
			"Allows you to warp to the girls or discovered locations without the fade animation.";

		internal static ConfigEntry<bool> Enabled { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.AddSetting(SECTION, $"_{SECTION} Enabled", true, DESCRIPTION_ENABLED);

			Subscription.Subscribe(typeof(FasterWarp), Enabled, Update);
		}
	}
}
