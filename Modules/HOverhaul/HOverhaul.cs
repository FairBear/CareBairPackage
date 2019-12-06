using BepInEx.Configuration;

namespace CareBairPackage
{
	public static partial class HOverhaul
	{
		const string SECTION = "H Overhaul";

		const string DESCRIPTION_ENABLED =
			"Adds a threshold for all actors to orgasm automatically and " +
			"an optional sleep sex overhaul.";
		const string DESCRIPTION_ORGASM_PLAYER =
			"At the start of each position, " +
			"a random value between this and the max value will be chosen and " +
			"when the player's feel meter reaches the random value, " +
			"the player will orgasm.";
		const string DESCRIPTION_ORGASM_PARTNER =
			"At the start of each position, " +
			"a random value between this and the max value will be chosen and " +
			"when the partner's feel meter reaches the random value, " +
			"the partner will orgasm.";
		const string DESCRIPTION_ORGASM_BOTH =
			"If both meters are above this value and one of the actors orgasm, " +
			"all actors will orgasm whenever possible.";
		const string DESCRIPTION_INSIDE_CHANCE =
			"Chance whether the player finishes inside. " +
			"100% means they'll always finish inside.";
		const string DESCRIPTION_DRINK_CHANCE =
			"Chance whether the partner drinks the semen. " +
			"100% means they'll always drink.";
		const string DESCRIPTION_MOUTH_CHANCE =
			"Chance whether the partner takes the semen in their mouth. " +
			"100% means they will always take the semen in their mouth. " +
			"This doesn't mean they'll drink it.";
		const string DESCRIPTION_WAKE_UP_CHANCE =
			"Chance for the current partner to wake up after an orgasm. " +
			"100% means they will wake up immediately after an orgasm.";
		const string DESCRIPTION_WAKE_RATE =
			"The amount increased to the wake up chance when any of the feel meters is increasing. " +
			"100% means the amount increased is the same as the feel meter's increase rate. " +
			"The total chance cannot be above 'Wake Up Chance'. " +
			"'Change Sleep Sex' needs to be enabled.";
		const string DESCRIPTION_WAKE_DECAY =
			"The amount decreased to the wake up chance every second when " +
			"none of the feel meters are increasing. " +
			"100% means the wake up chance will drop to 0% in 1 second. " +
			"'Change Sleep Sex' needs to be enabled.";
		const string DESCRIPTION_CHANGE_SLEEP_SEX =
			"Changes how sleep sex works. " +
			"The chance to wake up starts at 0%. " +
			"When any of the feel meters increase, the chance to wake up increases ('Wake Up Rate'). " +
			"The chance decreases overtime ('Wake Decay Rate') when " +
			"none of the feel meters are increasing. " +
			"The total chance cannot be above 'Wake Up Chance'.";

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
			Enabled = Config.Bind(SECTION, "__Enabled", false, DESCRIPTION_ENABLED);
			WindowID = Config.Bind(SECTION, "__Window ID", 12087);

			OrgasmPlayer = Config.Bind(SECTION, "Minimum Orgasm Player", 80, new ConfigDescription(DESCRIPTION_ORGASM_PLAYER, new AcceptableValueRange<int>(0, 100)));
			OrgasmPartner = Config.Bind(SECTION, "Minimum Orgasm Partner", 80, new ConfigDescription(DESCRIPTION_ORGASM_PARTNER, new AcceptableValueRange<int>(0, 100)));
			OrgasmBoth = Config.Bind(SECTION, "Minimum Orgasm Both", 70, new ConfigDescription(DESCRIPTION_ORGASM_BOTH, new AcceptableValueRange<int>(0, 100)));
			InsideChance = Config.Bind(SECTION, "Inside Chance", 80, new ConfigDescription(DESCRIPTION_INSIDE_CHANCE, new AcceptableValueRange<int>(0, 100)));
			DrinkChance = Config.Bind(SECTION, "Drink Chance", 40, new ConfigDescription(DESCRIPTION_DRINK_CHANCE, new AcceptableValueRange<int>(0, 100)));
			MouthChance = Config.Bind(SECTION, "Mouth Chance", 40, new ConfigDescription(DESCRIPTION_MOUTH_CHANCE, new AcceptableValueRange<int>(0, 100)));
			WakeChance = Config.Bind(SECTION, "Wake Up Chance", 50, new ConfigDescription(DESCRIPTION_WAKE_UP_CHANCE, new AcceptableValueRange<int>(0, 100)));
			WakeRate = Config.Bind(SECTION, "Wake Up Rate", 50, new ConfigDescription(DESCRIPTION_WAKE_RATE, new AcceptableValueRange<int>(0, 100)));
			WakeDecay = Config.Bind(SECTION, "Wake Decay Rate", 5, new ConfigDescription(DESCRIPTION_WAKE_DECAY, new AcceptableValueRange<int>(0, 100)));;
			WakeDisplay = Config.Bind(SECTION, "Display Wake Up Chance", true);
			ChangeSleepSex = Config.Bind(SECTION, "Change Sleep Sex", false, DESCRIPTION_CHANGE_SLEEP_SEX);

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
