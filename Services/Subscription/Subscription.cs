using BepInEx.Configuration;
using BepInEx.Harmony;
using System;
using System.Collections.Generic;

namespace CareBairPackage
{
	static partial class Subscription
	{
		static readonly HashSet<SubscriptionBundle> updates = new HashSet<SubscriptionBundle>();
		static readonly HashSet<SubscriptionBundle> lateUpdates = new HashSet<SubscriptionBundle>();
		static readonly HashSet<SubscriptionBundle> onGUIs = new HashSet<SubscriptionBundle>();
		static readonly HashSet<SubscriptionBundle> dump = new HashSet<SubscriptionBundle>();

		public delegate void SubscriptionHook();

		public static SubscriptionBundle Subscribe(Type source,
												   ConfigEntry<bool> entry,
												   SubscriptionHook update = null,
												   SubscriptionHook lateUpdate = null,
												   SubscriptionHook onGUI = null,
												   bool patch = false)
		{
			SubscriptionBundle bundle = new SubscriptionBundle(source, update, lateUpdate, onGUI, patch);

			CareBairPackage.InitSetting(entry, () =>
			{
				if (entry.Value)
					Subscribe(bundle);
				else
					Unsubscribe(bundle);
			});

			return bundle;
		}

		public static void Subscribe(SubscriptionBundle bundle)
		{
			if (bundle.update != null)
				updates.Add(bundle);

			if (bundle.lateUpdate != null)
				lateUpdates.Add(bundle);

			if (bundle.onGUI != null)
				onGUIs.Add(bundle);

			if (bundle.patch)
				bundle.harmony = HarmonyWrapper.PatchAll(bundle.source);
		}

		private static void UnsubscribeInternal(SubscriptionBundle bundle)
		{
			if (bundle.update != null)
				updates.Remove(bundle);

			if (bundle.lateUpdate != null)
				lateUpdates.Remove(bundle);

			if (bundle.onGUI != null)
				onGUIs.Remove(bundle);

			if (bundle.harmony != null)
			{
				bundle.harmony.UnpatchAll();

				bundle.harmony = null;
			}
		}

		public static void Unsubscribe(SubscriptionBundle bundle)
		{
			dump.Add(bundle);
		}

		static void Invoke(HashSet<SubscriptionBundle> bundles, Action<SubscriptionBundle> act)
		{
			if (dump.Count > 0)
			{
				foreach (SubscriptionBundle bundle in dump)
					UnsubscribeInternal(bundle);

				dump.Clear();
			}

			foreach (SubscriptionBundle bundle in bundles)
			{
				try
				{
					act(bundle);
				}
				catch (Exception err)
				{
					Unsubscribe(bundle);
					CareBairPackage.LogError(bundle.source, $"{err.Source} {err.Message} {err.StackTrace}");
				}
			}
		}

		public static void Update() => Invoke(updates, bundle => bundle.update());
		public static void LateUpdate() => Invoke(lateUpdates, bundle => bundle.lateUpdate());
		public static void OnGUI() => Invoke(onGUIs, bundle => bundle.onGUI());
	}
}
