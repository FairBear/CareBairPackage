using HarmonyLib;
using Manager;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class HOverhaul
	{
		static float playerMeter;
		static float partnerMeter;
		static float bothMeter;
		static float insideChance;
		static float drinkChance;
		static float mouthChance;
		static float wakeRate;
		static float playerAt = -1f;
		static float partnerAt = -1f;

		static float wakeChance = 0f;
		static float feelM = 0f;
		static float feelF = 0f;

		[HarmonyPostfix, HarmonyPatch(typeof(HScene), "InitCoroutine")]
		public static void Postfix_HScene_InitCoroutine()
		{
			wakeChance = feelM = feelF = 0f;
		}

		[HarmonyPostfix, HarmonyPatch(typeof(HScene), "Update")]
		public static void Postfix_HScene_Update(HScene __instance, ref HSceneManager ___hSceneManager, ref int ___mode, ref int ___modeCtrl)
		{
			if (__instance.NowStateIsEnd)
				return;

			HSceneFlagCtrl flagCtrl = __instance.ctrlFlag;

			if (flagCtrl.selectAnimationListInfo != null)
				return;


			// Sleep Sex

			bool isSleepSex = ___hSceneManager.EventKind == HSceneManager.HEvent.Yobai;

			if (isSleepSex)
			{
				if (ChangeSleepSex.Value)
				{
					bool flagM = flagCtrl.feel_m > feelM;
					bool flagF = flagCtrl.feel_f > feelF;

					if (flagM)
						wakeChance += (flagCtrl.feel_m - feelM) * wakeRate * 100f;

					if (flagF)
						wakeChance += (flagCtrl.feel_f - feelF) * wakeRate * 100f;

					if (!flagM && !flagF)
						wakeChance -= WakeDecay.Value * Time.deltaTime;

					wakeChance = Mathf.Clamp(wakeChance, 0f, WakeChance.Value);

					feelM = flagCtrl.feel_m;
					feelF = flagCtrl.feel_f;
				}
				else
					wakeChance = WakeChance.Value;

				flagCtrl.YobaiBareRate = (int)wakeChance;
			}
			else
				wakeChance = flagCtrl.YobaiBareRate;


			// Partner Check

			bool partnerCan = flagCtrl.feel_f >= partnerMeter;

			partnerAt = GetMeterAt(partnerCan, partnerMeter, partnerAt);

			if (partnerCan && flagCtrl.feel_f >= partnerAt)
			{
				switch (___mode)
				{
					case 0:
					case 4:
					case 6:
						flagCtrl.feel_f = 1f;

						return;

					case 2:
					case 7:
						if (BothCan(flagCtrl))
						{
							flagCtrl.feel_f = flagCtrl.feel_m = 0.9f;
							flagCtrl.click = HSceneFlagCtrl.ClickKind.FinishSame;
						}
						else
							flagCtrl.feel_f = 1f;

						return;
				}
			}


			// Player Check

			bool playerCan = flagCtrl.feel_m >= playerMeter;

			playerAt = GetMeterAt(playerCan, playerMeter, playerAt);

			if (playerCan && flagCtrl.feel_m >= playerAt)
			{
				switch (___mode)
				{
					case 1:
						flagCtrl.feel_m = 0.9f;

						switch (___modeCtrl)
						{
							case 0:
								flagCtrl.click = HSceneFlagCtrl.ClickKind.FinishOutSide;

								return;

							case 1:
								if (mouthChance >= Random.value)
									if (drinkChance >= Random.value)
										flagCtrl.click = HSceneFlagCtrl.ClickKind.FinishDrink;
									else
										flagCtrl.click = HSceneFlagCtrl.ClickKind.FinishVomit;
								else
									flagCtrl.click = HSceneFlagCtrl.ClickKind.FinishOutSide;

								return;
						}

						break;

					case 2:
					case 7:
						flagCtrl.feel_m = 0.9f;

						if (BothCan(flagCtrl))
						{
							flagCtrl.feel_f = 0.9f;
							flagCtrl.click = HSceneFlagCtrl.ClickKind.FinishSame;
						}
						else if (flagCtrl.isInsert && insideChance >= Random.value)
							flagCtrl.click = HSceneFlagCtrl.ClickKind.FinishInSide;
						else
							flagCtrl.click = HSceneFlagCtrl.ClickKind.FinishOutSide;

						return;
				}
			}
		}
	}
}
