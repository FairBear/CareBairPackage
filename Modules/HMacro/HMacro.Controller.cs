using AIProject;
using HarmonyLib;
using Manager;
using System.Collections.Generic;

namespace CareBairPackage
{
	public static partial class HMacro
	{
		public static void Update()
		{
			if (!Enabled.Value || !HSceneManager.IsInstance() || !HSceneManager.isHScene)
				return;

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
					List<HScene.AnimationListInfo>[] _lstAnimInfo = Traverse
						.Create(HSceneFlagCtrl.Instance.GetComponent<HScene>())
						.Field("lstAnimInfo")
						.GetValue<List<HScene.AnimationListInfo>[]>();

					if (_lstAnimInfo != null && category < _lstAnimInfo.Length)
					{
						List<HScene.AnimationListInfo> list = _lstAnimInfo[category];

						if (list != null && list.Count > 0)
						{
							HScene.AnimationListInfo next = list[UnityEngine.Random.Range(0, list.Count)];
							HSceneFlagCtrl.Instance.selectAnimationListInfo = next;

							MapUIContainer.AddNotify($"Next Animation: {next.nameAnimation}");
						}
						else
							MapUIContainer.AddNotify("No animations available for this category.");
					}
					else
						MapUIContainer.AddNotify("This category is not available during this scene.");
				}
				else
					MapUIContainer.AddNotify("Next animation is already being loaded.");
			}
		}
	}
}
