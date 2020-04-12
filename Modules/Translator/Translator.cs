using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class Translator
	{
		const string SECTION = "Translator（翻译插件）";

		const string DESCRIPTION_ENABLED =
			//"Translates some non-english words provided by this plugin mod. " +
			//"Requires 'XUnity.AutoTranslator' plugin.";
			"翻译一些由这个插件mod提供的非英语单词\n" +
			"需要安装 'XUnity.AutoTranslator' 插件";

		internal static ConfigEntry<bool> Enabled { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#是否启用", true, DESCRIPTION_ENABLED);

			Subscription.Subscribe(typeof(Translator), Enabled);
		}
	}
}
