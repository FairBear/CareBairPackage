using HarmonyLib;
using System;

namespace CareBairPackage
{
	static partial class Subscription
	{
		public class SubscriptionBundle
		{
			public Type source;
			public SubscriptionHook update;
			public SubscriptionHook lateUpdate;
			public SubscriptionHook onGUI;
			public bool patch;
			public Harmony harmony;

			public SubscriptionBundle(Type source, SubscriptionHook update, SubscriptionHook lateUpdate, SubscriptionHook onGUI, bool patch)
			{
				this.source = source;
				this.update = update;
				this.lateUpdate = lateUpdate;
				this.onGUI = onGUI;
				this.patch = patch;
			}
		}
	}
}
