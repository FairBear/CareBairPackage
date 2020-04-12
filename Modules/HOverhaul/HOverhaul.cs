using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class HOverhaul
	{
		const string SECTION = "H Overhaul（H 改革）";

		const string DESCRIPTION_ENABLED =
			//"Adds a threshold for all actors to orgasm automatically and " +
			//"an optional sleep sex overhaul.";
			"为所有的角色增加自动高潮阈值\n" +
			"外加一个全新的睡时H的功能";
		const string DESCRIPTION_ORGASM_PLAYER =
			/*"At the start of each position, " 
			"a random value between this and the max value will be chosen and " +
			"when the player's feel meter reaches the random value, " +
			"the player will orgasm."*/
			"在任意位置H时开启\n" +
			"最小值和最大值之间将有一个随机点\n" +
			"当玩家的快感槽到达这个点时就会高潮";
		const string DESCRIPTION_ORGASM_PARTNER =
			/*"At the start of each position, " 
			"a random value between this and the max value will be chosen and " +
			"when the player's feel meter reaches the random value, " +
			"the player will orgasm."*/
			"在任意位置H时开启\n" +
			"最小值和最大值之间将有一个随机点\n" +
			"当玩家的快感到达这个点时就会高潮";
		const string DESCRIPTION_ORGASM_BOTH =
			/*"If both meters are above this value and one of the actors orgasm, " +
			"all actors will orgasm whenever possible."*/
			"如果两个性快感槽都超过这个值并且一个角色高潮\n" +
			"那么所有的角色都会进入高潮";
		const string DESCRIPTION_INSIDE_CHANCE =
			/*"Chance whether the player finishes inside. " +
			"100% means they'll always finish inside."*/
			"玩家内射的概率\n" +
			"100%表示他们总是会内射";
		const string DESCRIPTION_DRINK_CHANCE =
			/*"Chance whether the partner drinks the semen. "  +
			"100% means they'll always drink."*/
			"伴侣吞咽下精液的概率\n" +
			"100%表示他们总是会喝下精液";
		const string DESCRIPTION_MOUTH_CHANCE =
			//"Chance whether the partner takes the semen in their mouth. " +
			//"100% means they will always take the semen in their mouth. " +
			//"This doesn't mean they'll drink it."
			"伴侣用嘴接住精液的概率.\n" +
			"100%表示他们总是会用嘴接住精液.\n" +
			"但这并不表示她（他？）们会喝下它";
		const string DESCRIPTION_WAKE_UP_CHANCE =
			//"Chance for the current partner to wake up after an orgasm. "  +
			//"100% means they will wake up immediately after an orgasm."
			"目前的性伴侣在一次高潮后苏醒的概率\n" +
			"100%表示他们在一次高潮后一定会苏醒";
		const string DESCRIPTION_WAKE_RATE =
			//"The amount increased to the wake up chance when any of the feel meters is increasing. "  +
			//"100% means the amount increased is the same as the feel meter's increase rate. " +
			//"The total chance cannot be above 'Wake Up Chance'. " +
			//"'Change Sleep Sex' needs to be enabled."
			"苏醒概率会随着快感条的增长而增长\n" +
			 "100%表示快感条增加多少，苏醒概率增加多少\n" +
			"注意，总的概率不会超过之前所制定的苏醒概率\n" +
			"\"快感值和苏醒概率有关\"要被开启，该功能才会启用";
		const string DESCRIPTION_WAKE_DECAY =
			/*"The amount decreased to the wake up chance every second when " +
			"none of the feel meters are increasing. "+
			"100% means the wake up chance will drop to 0% in 1 second. " +
			"'Change Sleep Sex' needs to be enabled.";*/
			"苏醒概率衰退：当任意快感条在一秒内没有增长时苏醒概率降低的值\n" +
			"100%表示假如一秒内没有快感增长，苏醒概率将降为0\n" +
			"\"快感值和苏醒概率有关\"要被开启，该功能才会启用";
		const string DESCRIPTION_CHANGE_SLEEP_SEX =
			//"Changes how sleep sex works. "  +
			//"The chance to wake up starts at 0%. "  +
			//"When any of the feel meters increase, the chance to wake up increases ('Wake Up Rate'). " +
			//"The chance decreases overtime ('Wake Decay Rate') when "  +
			//"none of the feel meters are increasing. " +
			//"The total chance cannot be above 'Wake Up Chance'.";
			"让快感值和苏醒概率有关\n" +
			"苏醒概率从0开始\n" +
			"当快感条增长时，苏醒的概率也随之上升（按苏醒概率来计算）\n" +
			"当没有快感增长时,苏醒的概率将会降低（按苏醒衰退概率来计算）\n" +
			"总概率不能高于苏醒概率";

		internal static ConfigEntry<bool> Enabled { get; set; }
		internal static ConfigEntry<int> WindowID { get; set; }
		internal static ConfigEntry<int> OrgasmPlayer { get; set; }
		internal static ConfigEntry<int> OrgasmPartner { get; set; }
		internal static ConfigEntry<int> OrgasmBoth { get; set; }
		internal static ConfigEntry<int> InsideChance { get; set; }
		internal static ConfigEntry<int> DrinkChance { get; set; }
		internal static ConfigEntry<int> MouthChance { get; set; }
		internal static ConfigEntry<int> WakeChance { get; set; }
		internal static ConfigEntry<int> WakeRate { get; set; }
		internal static ConfigEntry<int> WakeDecay { get; set; }
		internal static ConfigEntry<bool> WakeDisplay { get; set; }
		internal static ConfigEntry<bool> ChangeSleepSex { get; set; }

		public static void Awake(ConfigFile Config)
		{
			Enabled = Config.Bind(SECTION, "#1是否启用", false, DESCRIPTION_ENABLED);
			WindowID = Config.Bind(SECTION, "#2Window ID", 12087);

			OrgasmPlayer = Config.Bind(SECTION,/* "Minimum Orgasm Player"*/"01.玩家最低高潮阈值", 80, new ConfigDescription(DESCRIPTION_ORGASM_PLAYER, new AcceptableValueRange<int>(0, 100)));
			OrgasmPartner = Config.Bind(SECTION, /*"Minimum Orgasm Partner" */ "02.伴侣最低高潮阈值", 80, new ConfigDescription(DESCRIPTION_ORGASM_PARTNER, new AcceptableValueRange<int>(0, 100)));
			OrgasmBoth = Config.Bind(SECTION,/* "Minimum Orgasm Both" */ "03.两人最低高潮阈值", 70, new ConfigDescription(DESCRIPTION_ORGASM_BOTH, new AcceptableValueRange<int>(0, 100)));
			InsideChance = Config.Bind(SECTION,/* "Inside Chance"*/"04.内射机率", 80, new ConfigDescription(DESCRIPTION_INSIDE_CHANCE, new AcceptableValueRange<int>(0, 100)));
			DrinkChance = Config.Bind(SECTION,/* "Drink Chance" */ "05.吞精概率", 40, new ConfigDescription(DESCRIPTION_DRINK_CHANCE, new AcceptableValueRange<int>(0, 100)));
			MouthChance = Config.Bind(SECTION,/* "Mouth Chance" */ "06.嘴接精的概率", 40, new ConfigDescription(DESCRIPTION_MOUTH_CHANCE, new AcceptableValueRange<int>(0, 100)));
			WakeChance = Config.Bind(SECTION,/* "Wake Up Chance" */ "08.苏醒概率", 50, new ConfigDescription(DESCRIPTION_WAKE_UP_CHANCE, new AcceptableValueRange<int>(0, 100)));
			WakeRate = Config.Bind(SECTION, /*"Wake Up Rate" */ "09.快感和苏醒概率比值", 50, new ConfigDescription(DESCRIPTION_WAKE_RATE, new AcceptableValueRange<int>(0, 100)));
			WakeDecay = Config.Bind(SECTION, /*"Wake Decay Rate" */ "11.苏醒衰退概率", 5, new ConfigDescription(DESCRIPTION_WAKE_DECAY, new AcceptableValueRange<int>(0, 100))); ;
			WakeDisplay = Config.Bind(SECTION, /*"Display Wake Up Chance" */ "10.快感值降低时苏醒概率降低", true, DESCRIPTION_WAKE_DECAY);
			ChangeSleepSex = Config.Bind(SECTION, /*"Change Sleep Sex"*/"07.快感值和苏醒概率有关", false, DESCRIPTION_CHANGE_SLEEP_SEX);

			CareBairPackage.InitSetting(OrgasmPlayer, () => playerMeter = OrgasmPlayer.Value / 100f);
			CareBairPackage.InitSetting(OrgasmPartner, () => partnerMeter = OrgasmPartner.Value / 100f);
			CareBairPackage.InitSetting(OrgasmBoth, () => bothMeter = OrgasmBoth.Value / 100f);
			CareBairPackage.InitSetting(InsideChance, () => insideChance = InsideChance.Value / 100f);
			CareBairPackage.InitSetting(DrinkChance, () => drinkChance = DrinkChance.Value / 100f);
			CareBairPackage.InitSetting(MouthChance, () => mouthChance = MouthChance.Value / 100f);
			CareBairPackage.InitSetting(WakeRate, () => wakeRate = WakeRate.Value / 100f);

			Subscription.Subscribe(typeof(HOverhaul), Enabled, onGUI: OnGUI, patch: true);
		}
	}
}
