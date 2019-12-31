using Manager;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class HOverhaul
	{
		static Rect rect = new Rect(Screen.width / 2f - 60f, 10f, 200f, 50f);
		static Rect innerRect = new Rect();

		static GUIStyle labelStyle;

		public static void OnGUI()
		{
			if (!WakeDisplay.Value ||
				!HSceneManager.isHScene ||
				HSceneManager.Instance.EventKind != HSceneManager.HEvent.Yobai)
				return;

			if (labelStyle == null)
				labelStyle = new GUIStyle(GUI.skin.label)
				{
					fontStyle = FontStyle.Bold,
					fontSize = 12
				};

			rect = GUI.Window(
				WindowID.Value,
				rect,
				Draw,
				$"Wake Up Chance: {wakeChance:F0}%",
				labelStyle
			);
		}

		static void Draw(int id)
		{
			GUILayout.BeginArea(innerRect);
			GUILayout.EndArea();
		}
	}
}
