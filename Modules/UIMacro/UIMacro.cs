using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class UIMacro
	{
		const string SECTION = "UI Macro";

		const string DESCRIPTION_ENABLED =
			"Allows you to open several UIs from the phone menu.";

		internal static ConfigEntry<bool> Enabled { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.AddSetting(SECTION, $"_{SECTION} Enabled", true, DESCRIPTION_ENABLED);

			Subscription.Subscribe(typeof(UIMacro), Enabled, Update, LateUpdate);
		}
	}
}
