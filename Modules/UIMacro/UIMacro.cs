using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class UIMacro
	{
		const string SECTION = "UI Macro（UI快捷菜单）";

		const string DESCRIPTION_ENABLED =
			//"Allows you to open several UIs from the phone menu.";
			"允许你在手机菜单通过快捷菜单窗口打开其他界面";

		internal static ConfigEntry<bool> Enabled { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#是否启用", true, DESCRIPTION_ENABLED);

			Subscription.Subscribe(typeof(UIMacro), Enabled, Update, LateUpdate);
		}
	}
}
