using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class Translator
	{
		const string SECTION = "Translator";

		const string DESCRIPTION_ENABLED =
			"Translates some non-english words provided by this plugin mod. " +
			"Requires 'XUnity.AutoTranslator' plugin.";

		internal static ConfigEntry<bool> Enabled { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "__Enabled", true, DESCRIPTION_ENABLED);

			Subscription.Subscribe(typeof(Translator), Enabled);
		}
	}
}
