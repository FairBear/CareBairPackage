using AIProject;
using BepInEx.Configuration;
using Manager;
using System.Linq;

namespace CareBairPackage
{
	public static partial class RunKey
	{
		static bool run = false;

		public static void Update()
		{
			if (!Map.IsInstance() || Map.Instance.Player == null)
				return;

			KeyboardShortcut hotkey = ToggleKey.Value;

			if (UnityEngine.Input.GetKeyDown(hotkey.MainKey) &&
				hotkey.Modifiers.All(key => UnityEngine.Input.GetKey(key)))
				run = !run;

			if (!run)
				return;

			float mult = RunMultiplier.Value;

			if (mult <= 0f)
				return;

			PlayerActor player = Map.Instance.Player;

			player.Locomotor.Move(player.Forward * mult);
		}
	}
}
