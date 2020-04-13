using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class UnrestrictedH
	{
		const string SECTION = "Unrestricted H（无限制 H）";

		const string DESCRIPTION_ENABLED =
			//"Changes the behavior and restrictions during H scene.";
			"改变H时的动作限制和地形限制";
		const string DESCRIPTION_ALL_H_POS =
			//"Removes restrictions for H positions and H points. " +
			//"Selecting lesbian positions with only 1 girl or " +
			//"group positions with only 2 people will break the H scene. " +
			//"Leaving the H scene will fix it.";
			"移除对H时的地形和位置限制\n" +
			"选择同性恋但却只有一个女孩或是选择多人体味但只有两个人时，则会造成错误\n" +
			"退出H场景即可修复这个问题";
		const string DESCRIPTION_VISIBLE_AUTO_H =
			"Forces the auto H toggle to be visible.\n?强制自动H切换为可见?";
		const string DESCRIPTION_REAL_TIME =
			//"Allows non-participants to move and act during H scene. " +
			//"This will not make them visible. " +
			//"This includes the merchant.";
			"允许非参与者在H场景中移动和做动作\n"+
			"这样她们就不会在H场景不可见\n" +
			"包括商人";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<bool> AllHPos { get; set; }
		internal static ConfigEntry<bool> VisibleAutoH { get; set; }
		internal static ConfigEntry<bool> RealTime { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#是否启用", true, DESCRIPTION_ENABLED);

			AllHPos = Config.Bind(SECTION, "任意地点H", true, DESCRIPTION_ALL_H_POS);
			VisibleAutoH = Config.Bind(SECTION, "自动H始终可见", true, DESCRIPTION_VISIBLE_AUTO_H);
			RealTime = Config.Bind(SECTION, "不冻结非参与者", true, DESCRIPTION_REAL_TIME);

			Subscription.Subscribe(typeof(UnrestrictedH), Enabled, patch: true);
		}
	}
}
