using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class ClothState
	{
		const string SECTION = "Cloth State";

		const string DESCRIPTION_ENABLED =
			"Allows you to change the clothing state of the girl you are talking to.";

		internal static ConfigEntry<bool> Enabled { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "__Enabled", true, DESCRIPTION_ENABLED);

			Subscription.Subscribe(typeof(ClothState), Enabled, Update);
		}
	}
}
