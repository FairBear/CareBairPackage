using HarmonyLib;
using System;

namespace CareBairPackage
{
	static partial class Subscription
	{
		class SubscriptionBundle
		{
			public Type source;
			public SubscriptionHook update;
			public SubscriptionHook lateUpdate;
			public SubscriptionHook onGUI;
			public Harmony harmony;

			public SubscriptionBundle(Type source, SubscriptionHook update, SubscriptionHook lateUpdate, SubscriptionHook onGUI)
			{
				this.source = source;
				this.update = update;
				this.lateUpdate = lateUpdate;
				this.onGUI = onGUI;
			}
		}
	}
}
