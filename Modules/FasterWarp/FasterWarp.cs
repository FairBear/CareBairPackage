using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class FasterWarp
	{
		const string SECTION = "Faster Warp（快速传送）";

		const string DESCRIPTION_ENABLED =
			//"Allows you to warp to the girls or discovered locations without the fade animation.";
			"允许你瞬间传送到女孩身旁或是据点上";

		internal static ConfigEntry<bool> Enabled { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#是否启用", true, DESCRIPTION_ENABLED);

			Subscription.Subscribe(typeof(FasterWarp), Enabled, Update);
		}
	}
}
