using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class PanningHelper
	{
		const string SECTION = "Panning Helper（视角移动相关功能设置）";

		const string DESCRIPTION_ENABLED =
			//"Locks the mouse position when dragging with the left, right, or middle mouse button.";
			"用鼠标左键、右键或中键拖动视角时隐藏并锁定鼠标位置";
		const string DESCRIPTION_LMB =
			//"Left Mouse Button";
			"鼠标左键";
		const string DESCRIPTION_RMB =
			//"Right Mouse Button";
			"鼠标右键";
		const string DESCRIPTION_MMB =
			//"Middle Mouse Button";
			"鼠标中键(按下滚轮)";


		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<bool> LMB { get; set; }
		internal static ConfigEntry<bool> RMB { get; set; }
		internal static ConfigEntry<bool> MMB { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#是否启用", true, DESCRIPTION_ENABLED);
			LMB = Config.Bind(SECTION, "左键", true, DESCRIPTION_LMB);
			RMB = Config.Bind(SECTION, "右键", true, DESCRIPTION_RMB);
			MMB = Config.Bind(SECTION, "中键", true, DESCRIPTION_MMB);

			Subscription.Subscribe(typeof(PanningHelper), Enabled, null, LateUpdate);
		}
	}
}
