using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class RunKey
	{
		const string SECTION = "Run Key（加速移动）";

		const string DESCRIPTION_ENABLED =
			//"Allows you to move faster with a toggle key.";
			"通过快捷键来开关加速移动功能";
		const string DESCRIPTION_RUN_FORCE =
			//"This is multiplied to the player character's forward force. " +
			//"Setting this to 0 will give no additional speed.";
			"这个值将乘到玩家角色的移动速度上\n" +
			"如果将此设置为0不会增加速度";
		const string DESCRIPTION_TOGGLE_KEY =
			//"Pressing this key will change the player's move speed. " +
			//"Speed depends on the 'Run Multiplier' value.";
			"按下这个键会改变玩家的移动速度\n" +
			"速度取决于\"奔跑乘数\"的值";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<float> RunMultiplier { get; set; }
		internal static ConfigEntry<KeyboardShortcut> ToggleKey { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#是否启用", true, DESCRIPTION_ENABLED);

			RunMultiplier = Config.Bind(SECTION, /*"Run Mutliplier"*/"01.奔跑乘数", 2f, DESCRIPTION_RUN_FORCE);
			ToggleKey = Config.Bind(SECTION, /*"Toggle Key"*/"02.切换按键", new KeyboardShortcut(UnityEngine.KeyCode.LeftAlt), DESCRIPTION_TOGGLE_KEY);

			Subscription.Subscribe(typeof(RunKey), Enabled, Update);
		}
	}
}
