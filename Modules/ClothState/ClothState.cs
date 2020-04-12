using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class ClothState
	{
		const string SECTION = "Cloth State（服装状态切换）";

		const string DESCRIPTION_ENABLED =
			//"Allows you to change the clothing state of the girl you are talking to.";
			"在对话中或者属性面板上，可以快速改变角色所穿衣物状态";

		internal static ConfigEntry<bool> Enabled { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#是否启用", true, DESCRIPTION_ENABLED);

			Subscription.Subscribe(typeof(ClothState), Enabled, Update);
		}
	}
}
