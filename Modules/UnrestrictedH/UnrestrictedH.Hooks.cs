using HarmonyLib;
using System;

namespace CareBairPackage
{
	public static partial class UnrestrictedH
	{
		[HarmonyPrefix, HarmonyPatch(typeof(HSceneSprite), "CheckAutoMotionLimit")]
		public static bool Prefix_HSceneSprite_CheckAutoMotionLimit(ref bool __result, HScene.AnimationListInfo lstAnimInfo)
		{
			// Unrestricted selection of all auto H positions.
			__result = true;

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(typeof(HSceneSprite), "CheckMotionLimit", new[] { typeof(HScene.AnimationListInfo) })]
		public static bool Prefix_HSceneSprite_CheckMotionLimit(ref bool __result, HScene.AnimationListInfo lstAnimInfo)
		{
			// Unrestricted selection of all H positions.
			__result = true;

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(typeof(HPointCtrl), "CheckMotionLimit", new[] { typeof(int), typeof(HScene.AnimationListInfo) })]
		public static bool Prefix_HPointCtrl_CheckMotionLimit(ref bool __result, int place, HScene.AnimationListInfo lstAnimInfo)
		{
			// Unrestricted selection of all H points.
			__result = true;

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(typeof(HSceneSprite), "MainCategoryOfLeaveItToYou")]
		public static bool Prefix_HSceneSprite_MainCategoryOfLeaveItToYou(HSceneSprite __instance, bool _isLeaveItToYou)
		{
			// Redirect to regular H category assessment.
			if (!_isLeaveItToYou)
				return true;

			__instance.MainCategoryOfLeaveItToYou(false);

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(typeof(HSceneSprite), "SetAnimationMenu")]
		public static bool Prefix_HSceneSprite_SetAnimationMenu(HSceneSprite __instance, ref bool __result)
		{
			// Activate all H categories.
			__result = true;

			__instance.categoryMain.SetActive(true, -1);

			return false;
		}

		[HarmonyPrefix, HarmonyPatch(typeof(HSceneSprite), "SetVisibleLeaveItToYou")]
		public static bool Prefix_HSceneSprite_SetVisibleLeaveItToYou(HSceneSprite __instance, bool _visible, bool _judgeLeaveItToYou = false)
		{
			// Allow auto H for non-auto H positions.
			__instance.tglLeaveItToYou.gameObject.SetActive(true);

			return false;
		}
	}
}
