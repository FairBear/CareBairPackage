using AIProject;
using HarmonyLib;
using Manager;
using System.Collections.Generic;

namespace CareBairPackage
{
	public static partial class HMacro
	{
		static List<HScene.AnimationListInfo>[] lstAnimInfo;

		public static void Update()
		{
			if (!HSceneManager.IsInstance())
				return;

			if (!HSceneManager.isHScene)
			{
				if (lstAnimInfo != null)
					lstAnimInfo = null;

				return;
			}

			int category = -1;

			if (CaressKey.Value.IsDown())
				category = 0;
			else if (ServiceKey.Value.IsDown())
				category = 1;
			else if (IntercourseKey.Value.IsDown())
				category = 2;
			else if (SpecialKey.Value.IsDown())
				category = 3;
			else if (LesbianKey.Value.IsDown())
				category = 4;
			else if (GroupKey.Value.IsDown())
				category = 5;

			if (category != -1)
			{
				if (HSceneFlagCtrl.Instance.selectAnimationListInfo == null)
				{
					if (lstAnimInfo == null)
						GenerateList();

					if (lstAnimInfo != null && category < lstAnimInfo.Length)
					{
						List<HScene.AnimationListInfo> list = lstAnimInfo[category];

						if (list != null && list.Count > 0)
						{
							HScene.AnimationListInfo next = list[UnityEngine.Random.Range(0, list.Count)];
							HSceneFlagCtrl.Instance.selectAnimationListInfo = next;

							MapUIContainer.AddNotify(/*$"Next Animation*/$"下一个动画: {next.nameAnimation}");
						}
						else
							MapUIContainer.AddNotify(/*"No animations available for this category."*/"这个类别没有可用的动画");
					}
					else
						MapUIContainer.AddNotify(/*"This category is not available during this scene."*/"这个类别的动画在此场景中不可用");
				}
				else
					MapUIContainer.AddNotify(/*"Next animation is already being loaded."*/"下一个动画已加载");
			}
		}
	}
}
