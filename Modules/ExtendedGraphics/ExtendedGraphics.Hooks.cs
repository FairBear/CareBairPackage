using HarmonyLib;

namespace CareBairPackage
{
	public static partial class ExtendedGraphics
	{
		[HarmonyPrefix, HarmonyPatch(typeof(SimpleFade), "Init")]
		public static bool Prefix_SimpleFade_Init(SimpleFade __instance)
		{
			if (!boolState.Get("无过度动画", true))
				return true;

			__instance.ForceEnd();

			return false;
		}

		[HarmonyPostfix, HarmonyPatch(typeof(CrossFade), "FadeStart")]
		public static void Postfix_CrossFade_FadeStart(CrossFade __instance)
		{
			if (!boolState.Get("无过度动画", true))
				return;

			__instance.End();
		}
	}
}
