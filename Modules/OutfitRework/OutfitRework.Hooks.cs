using AIChara;
using AIProject;
using HarmonyLib;
using Manager;
using System.Collections.Generic;
using UnityEngine;
using static AIProject.Definitions.Desire;

namespace CareBairPackage
{
	public static partial class OutfitRework
	{
		readonly static HashSet<ChaControl> partners = new HashSet<ChaControl>();

		[HarmonyPostfix, HarmonyPatch(typeof(HSceneManager), "HsceneInit", new[] { typeof(AgentActor[]) })]
		public static void Postfix_HSceneManager_HsceneInit(AgentActor[] agent)
		{
			foreach (AgentActor partner in agent)
				if (partner != null && !partners.Contains(partner.ChaControl))
					partners.Add(partner.ChaControl);
		}

		[HarmonyPostfix, HarmonyPatch(typeof(HSceneManager), "HsceneInit", new[] { typeof(MerchantActor), typeof(AgentActor) })]
		public static void Postfix_HSceneManager_HsceneInit(MerchantActor Merchant, AgentActor agent = null)
		{
			if (!partners.Contains(Merchant.ChaControl))
				partners.Add(Merchant.ChaControl);

			if (agent != null && !partners.Contains(agent.ChaControl))
				partners.Add(agent.ChaControl);
		}

		[HarmonyPrefix, HarmonyPatch(typeof(ChaControl), "ChangeNowCoordinate", new[] { typeof(ChaFileCoordinate), typeof(bool), typeof(bool) })]
		public static bool Prefix_ChaControl_ChangeNowCoordinate(ChaControl __instance, ChaFileCoordinate srcCoorde, bool reload = false, bool forceChange = true)
		{
			if (!Map.IsInstance())
				return true;

			if (partners.Contains(__instance))
			{
				if (Map.Instance.Player.CameraControl.Mode != CameraMode.H)
				{
					partners.Remove(__instance);

					if (srcCoorde == __instance.chaFile.coordinate &&
						Random.Range(0, 100) < KeepHOutfit.Value)
					{
						__instance.SetClothesStateAll(0);
						__instance.ChangeAccessory();

						return false;
					}
				}
			}
			else
			{
				AgentActor actor = null;

				foreach (KeyValuePair<int, AgentActor> agent in Map.Instance.AgentTable)
					if (agent.Value.ChaControl == __instance)
					{
						actor = agent.Value;
						break;
					}

				if (actor != null &&
					actor.Mode == ActionType.EndTaskDressOut &&
					Random.Range(0, 100) < KeepOutfit.Value)
					return false;

				/*
[Info   : Unity Log] [AAAAAAAAAAAAAAAAAAAA] 10 | EndTaskDressIn | SearchBath | SearchBath | Normal
[Info   : Unity Log] [AAAAAAAAAAAAAAAAAAAA] 11 | EndTaskDressOut | GotoDressOut | GotoDressOut | Normal
[Info   : Unity Log] [AAAAAAAAAAAAAAAAAAAA] 10 | EndTaskDressIn | SearchBath | SearchBath | Normal
[Info   : Unity Log] [AAAAAAAAAAAAAAAAAAAA] 11 | EndTaskDressOut | GotoDressOut | GotoDressOut | Normal
Debug.Log($"[AAAAAAAAAAAAAAAAAAAA] {actor.ActionID} | {actor.Mode.ToString()} | {actor.PrevActionMode.ToString()} | {actor.PrevMode.ToString()} | {actor.ReservedMode.ToString()}");
				 */
			}

			return true;
		}
	}
}
