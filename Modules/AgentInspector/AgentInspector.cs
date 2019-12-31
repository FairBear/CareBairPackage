using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class AgentInspector
	{
		const string SECTION = "Agent Inspector";

		const string DESCRIPTION_ENABLED =
			"Get the current agent being inspected by the player. " +
			"Disabling this may make other modules useless.";

		internal static ConfigEntry<bool> Enabled { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "__Enabled", true, DESCRIPTION_ENABLED);

			Subscription.Subscribe(typeof(AgentInspector), Enabled, Update);
		}
	}
}
