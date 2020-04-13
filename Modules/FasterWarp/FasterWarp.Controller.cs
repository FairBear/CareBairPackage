using AIChara;
using AIProject;
using Manager;
using System.Collections.Generic;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class FasterWarp
	{
		static readonly ADMSheet sheet = new ADMSheet(/*"Warp To"*/"传送到", null, Condition);

		public static void Update()
		{
			if (AltDialogMenu.IsSheet(sheet) || !Condition())
				return;

			RefreshSheets();
			AltDialogMenu.AddSheet(sheet);
		}

		public static bool Condition()
		{
			return
				MapUIContainer.IsInstance() &&
				MapUIContainer.Instance.MinimapUI.AllAreaMap.activeSelf;
		}

		public static void Warp(Transform transform)
		{
			PlayerActor player = Map.Instance.Player;
			Actor partner = player.Partner;

			if (partner?.NavMeshAgent.transform == transform)
				return;

			player.NavMeshAgent.Warp(transform.position);
			player.Rotation = transform.rotation;
			player.CameraControl.XAxisValue = transform.rotation.eulerAngles.y;
			player.CameraControl.YAxisValue = 0.6f;

			if (partner != null)
			{
				partner.NavMeshAgent.Warp(
					player.Position +
					player.Rotation *
					Manager.Resources.Instance.AgentProfile.GetOffsetInParty(
						partner.ChaControl.GetShapeBodyValue(0)
					)
				);

				partner.Rotation = transform.rotation;
			}
		}

		public static void RefreshSheets()
		{
			List<ADMSheet> list = new List<ADMSheet>();

			foreach (MiniMapControler.IconInfo info in MapUIContainer.Instance.MinimapUI.GetBaseIconInfos())
			{
				BasePoint basePoint = info.Point.GetComponent<BasePoint>();

				Map.Instance.GetBasePointOpenState(basePoint.ID, out bool flag);

				if (!flag)
					continue;

				list.Add(new ADMSheet(info.Name.Translate(), () => Warp(basePoint.WarpPoint)));
			}

			foreach (KeyValuePair<int, AgentActor> pair in Map.Instance.AgentTable)
			{
				AgentActor agent = pair.Value;

				list.Add(new ADMSheet($"{pair.Key}: {agent.CharaName}",
					() => Warp(agent.NavMeshAgent.transform)
				));
			}

			sheet.SetSheets(list);
		}

		public static void SetDialog()
		{
			RefreshSheets();
			AltDialogMenu.AddSheet(sheet);
		}
	}
}
