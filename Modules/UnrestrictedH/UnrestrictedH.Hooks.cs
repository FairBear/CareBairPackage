using AIProject;
using Correct;
using HarmonyLib;
using Manager;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class UnrestrictedH
	{
		[HarmonyPrefix, HarmonyPatch(typeof(HScene), "InitCoroutine")]
		public static bool Prefix_HScene_InitCoroutine()
		{
			HSceneManager manager = HSceneManager.Instance;

			if (RealTime.Value)
			{
				foreach (AgentActor actor in Map.Instance.AgentTable.Values)
					if (actor != null && actor != manager.females[0] && actor != manager.females[1])
						actor.EnableEntity();

				if (!manager.bMerchant)
				{
					MerchantActor merchant = Map.Instance.Merchant;
					merchant.Controller.enabled = true;
					merchant.AnimationMerchant.enabled = true;

					merchant.EnableEntity();
				}
			}

			return true;
		}

		[HarmonyPrefix, HarmonyPatch(typeof(HSceneSprite), "CheckAutoMotionLimit")]
		public static bool Prefix_HSceneSprite_CheckAutoMotionLimit(ref bool __result, HScene.AnimationListInfo lstAnimInfo)
		{
			if (!AllHPos.Value)
				return true;

			// Unrestricted selection of all auto H positions.
			__result = true;

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(typeof(HSceneSprite), "CheckMotionLimit", new[] { typeof(HScene.AnimationListInfo) })]
		public static bool Prefix_HSceneSprite_CheckMotionLimit(ref bool __result, HScene.AnimationListInfo lstAnimInfo)
		{
			if (!AllHPos.Value)
				return true;

			// Unrestricted selection of all H positions.
			__result = true;

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(typeof(HPointCtrl), "CheckMotionLimit", new[] { typeof(int), typeof(HScene.AnimationListInfo) })]
		public static bool Prefix_HPointCtrl_CheckMotionLimit(ref bool __result, int place, HScene.AnimationListInfo lstAnimInfo)
		{
			if (!AllHPos.Value)
				return true;

			// Unrestricted selection of all H points.
			__result = true;

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(typeof(HSceneSprite), "MainCategoryOfLeaveItToYou")]
		public static bool Prefix_HSceneSprite_MainCategoryOfLeaveItToYou(HSceneSprite __instance, bool _isLeaveItToYou)
		{
			if (!AllHPos.Value)
				return true;

			// Redirect to regular H category assessment.
			if (!_isLeaveItToYou)
				return true;

			__instance.MainCategoryOfLeaveItToYou(false);

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(typeof(HSceneSprite), "SetAnimationMenu")]
		public static bool Prefix_HSceneSprite_SetAnimationMenu(HSceneSprite __instance, ref bool __result)
		{
			if (!AllHPos.Value)
				return true;

			// Activate all H categories.
			__result = true;

			__instance.categoryMain.SetActive(true, -1);

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(typeof(HSceneSprite), "SetVisibleLeaveItToYou")]
		public static bool Prefix_HSceneSprite_SetVisibleLeaveItToYou(HSceneSprite __instance, bool _visible, bool _judgeLeaveItToYou = false)
		{
			if (!AllHPos.Value)
				return true;

			// Allow auto H for non-auto H positions.
			__instance.tglLeaveItToYou.gameObject.SetActive(true);

			return false;
		}
	}
}
