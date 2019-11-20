using BepInEx.Configuration;
using BepInEx.Harmony;
using HarmonyLib;
using System;
using System.Collections.Generic;

namespace CareBairPackage
{
	static partial class Subscription
	{
		static readonly HashSet<SubscriptionBundle> updates = new HashSet<SubscriptionBundle>();
		static readonly HashSet<SubscriptionBundle> lateUpdates = new HashSet<SubscriptionBundle>();
		static readonly HashSet<SubscriptionBundle> onGUIs = new HashSet<SubscriptionBundle>();

		public delegate void SubscriptionHook();

		public static void Subscribe(Type source, ConfigEntry<bool> entry, SubscriptionHook update = null, SubscriptionHook lateUpdate = null, SubscriptionHook onGUI = null, bool patch = false)
		{
			SubscriptionBundle bundle = new SubscriptionBundle(source, update, lateUpdate, onGUI);

			CareBairPackage.InitSetting(entry, () =>
			{
				if (entry.Value)
				{
					if (update != null)
						updates.Add(bundle);

					if (lateUpdate != null)
						lateUpdates.Add(bundle);

					if (onGUI != null)
						onGUIs.Add(bundle);

					if (patch)
						bundle.harmony = HarmonyWrapper.PatchAll(source);
				}
				else
				{
					if (update != null)
						updates.Remove(bundle);

					if (lateUpdate != null)
						lateUpdates.Remove(bundle);

					if (onGUI != null)
						onGUIs.Remove(bundle);

					if (bundle.harmony != null)
					{
						bundle.harmony.UnpatchAll();

						bundle.harmony = null;
					}
				}
			});
		}

		static void Invoke(HashSet<SubscriptionBundle> bundles, Action<SubscriptionBundle> act)
		{
			HashSet<SubscriptionBundle> dump = new HashSet<SubscriptionBundle>();

			foreach (SubscriptionBundle bundle in bundles)
			{
				try
				{
					act(bundle);
				}
				catch (Exception err)
				{
					dump.Add(bundle);
					CareBairPackage.Log(bundle.source, $"{err.Source} {err.Message} {err.StackTrace}");
				}
			}

			foreach (SubscriptionBundle bundle in dump)
				bundles.Remove(bundle);
		}

		public static void Update() => Invoke(updates, bundle => bundle.update());
		public static void LateUpdate() => Invoke(lateUpdates, bundle => bundle.lateUpdate());
		public static void OnGUI() => Invoke(onGUIs, bundle => bundle.onGUI());
	}
}
