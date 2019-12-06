using AIProject;
using HarmonyLib;
using Manager;
using System.Collections.Generic;
using System.Reflection;

namespace CareBairPackage
{
	public static partial class HMacro
	{
		public static void GenerateList()
		{
			MethodInfo method = AccessTools.Method(typeof(HSceneSprite), "CheckMotionLimit");

			if (method == null)
				return;

			List<HScene.AnimationListInfo>[] lstAnimInfo = Traverse
				.Create(HSceneFlagCtrl.Instance.GetComponent<HScene>())
				.Field("lstAnimInfo")
				.GetValue<List<HScene.AnimationListInfo>[]>();

			HMacro.lstAnimInfo = new List<HScene.AnimationListInfo>[lstAnimInfo.Length];

			for (int i = 0; i < lstAnimInfo.Length; i++)
			{
				if (HMacro.lstAnimInfo[i] == null)
					HMacro.lstAnimInfo[i] = new List<HScene.AnimationListInfo>();

				foreach (HScene.AnimationListInfo animInfo in lstAnimInfo[i])
					if ((bool)method.Invoke(HSceneSprite.Instance, new[] { animInfo }))
						HMacro.lstAnimInfo[i].Add(animInfo);
			}
		}
	}
}
