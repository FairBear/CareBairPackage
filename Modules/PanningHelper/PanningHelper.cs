using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class PanningHelper
	{
		const string SECTION = "Panning Helper";

		const string DESCRIPTION_ENABLED =
			"Locks the mouse position when dragging with the left, right, or middle mouse button.";
		const string DESCRIPTION_LMB =
			"Left Mouse Button";
		const string DESCRIPTION_RMB =
			"Right Mouse Button";
		const string DESCRIPTION_MMB =
			"Middle Mouse Button";


		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<bool> LMB { get; set; }
		internal static ConfigEntry<bool> RMB { get; set; }
		internal static ConfigEntry<bool> MMB { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "__Enabled", true, DESCRIPTION_ENABLED);
			LMB = Config.Bind(SECTION, "LMB", true, DESCRIPTION_LMB);
			RMB = Config.Bind(SECTION, "RMB", true, DESCRIPTION_RMB);
			MMB = Config.Bind(SECTION, "MMB", true, DESCRIPTION_MMB);

			Subscription.Subscribe(typeof(PanningHelper), Enabled, null, LateUpdate);
		}
	}
}
