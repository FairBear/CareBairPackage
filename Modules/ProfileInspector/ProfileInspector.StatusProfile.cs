﻿using AIProject;
using Manager;

namespace CareBairPackage
{
	public static partial class ProfileInspector
	{
		public static void StatusProfile()
		{
			StatusProfile profile = Resources.Instance.StatusProfile;

			Batch<float>(
				profile,
				"StatusProfile",
				"_buffCook",
				"_buffBath",
				"_buffAnimal",
				"_buffSleep",
				"_buffGift",
				"_buffGimme",
				"_buffEat",
				"_buffPlay",
				"_buffH",
				"_cursedHBuff",
				"_buffLonely",
				"_buffLonelySuperSense",
				"_buffBreak",
				"_buffLocation",
				"_buffSearchTough",
				"_buffSearch",
				"_debuffMood",
				"_debuffMoodInBathDesire",
				"_buffImmoral",
				"_gWifeMotivationBuff",
				"_activeBuffMotivation",
				"_healthyPhysicalBorder",
				"_cursedPhysicalBuff",
				"_immoralBuff",
				"_cursedImmoralBuff",
				"_canClothChangeBorder",
				"_canDressBorder",
				"_defaultInstructionRate",
				"_instructionRateDebuff",
				"_defaultFollowRate",
				"_followRateBuff",
				"_girlsActionProb",
				"_lesbianRate",
				"_shallowSleepProb",
				"_callProbBaseRate",
				"_callProbPhaseRate",
				"_callLowerMoodProb",
				"_callUpperMoodProb",
				"_callSecondTimeProb",
				"_callOverTimeProb",
				"_callProbSuperSense",
				"_handSearchProbBuff",
				"_fishingSearchProbBuff",
				"_pickelSearchProbBuff",
				"_shovelSearchProbBuff",
				"_netSearchProbBuff",
				"_coldDefaultIncidence",
				"_coldLockDuration",
				"_heatStrokeDefaultIncidence",
				"_heatStrokeLockDuration",
				"_hurtDefaultIncidence",
				"_coldRateDebuffWeak",
				"_heatStrokeBuffGuts",
				"_wetRateInRain",
				"_wetRateInStorm",
				"_drySpeed",
				"_wetTemperatureRate",
				"_coldTemperatureValue",
				"_hotTemperatureValue",
				"_lesbianBorderDesire",
				"_shallowSleepHungerLowBorder",
				"_restoreRangeMinuteTime",
				"_potionImmoralAdd",
				"_diureticToiletAdd",
				"_pillSleepAdd"
			);

			Batch<int>(
				profile,
				"StatusProfile",
				"_lustImmoralBuff",
				"_firedBodyImmoralBuff",
				"_lesbianFriendlyRelationBorder",
				"_darknessReduceMaiden",
				"_reliabilityGWifeBuff",
				"_masturbationBorder",
				"_invitationBorder",
				"_revRapeBorder",
				"_lesbianBorder",
				"_holdingHandBorderReliability",
				"_approachBorderReliability",
				"_canGreetBorder",
				"_washFaceBorder",
				"_nightLightBorder",
				"_surpriseBorder",
				"_girlsActionBorder",
				"_talkRelationUpperBorder",
				"_lesbianSociabilityBuffBorder",
				"_chefCookSuccessBoost",
				"_coldBaseDuration",
				"_catCaptureProbBuff",
				"_starveWarinessValue",
				"_starveDarknessValue",
				"_lampEquipableBorder",
				"_soineReliabilityBorder"
			);
		}
	}
}
