using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class AgentInspector
	{
		const string SECTION = "Agent Inspector（交互对象检测）";

		const string DESCRIPTION_ENABLED =
			//"Get the current agent being inspected by the player. " +
			//"Disabling this may make other modules useless.";
			"获取当前玩家交互对象" +
			"禁用它可能会使其他功能失效";

		internal static ConfigEntry<bool> Enabled { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#是否启用", true, DESCRIPTION_ENABLED);

			Subscription.Subscribe(typeof(AgentInspector), Enabled, Update);
		}
	}
}
