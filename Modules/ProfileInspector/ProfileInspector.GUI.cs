using System.Collections.Generic;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class ProfileInspector
	{
		const float MARGIN_TOP = 20f;
		const float MARGIN_BOTTOM = 10f;
		const float MARGIN_LEFT = 10f;
		const float MARGIN_RIGHT = 10f;
		const float WIDTH = 400f;
		const float HEIGHT = 400f;
		const float INNER_WIDTH = WIDTH - MARGIN_LEFT - MARGIN_RIGHT;
		const float INNER_HEIGHT = HEIGHT - MARGIN_TOP - MARGIN_BOTTOM;

		static Rect rect = new Rect(Screen.width - WIDTH, (Screen.height - HEIGHT) / 2, WIDTH, HEIGHT);
		static Rect innerRect = new Rect(MARGIN_LEFT, MARGIN_TOP, INNER_WIDTH, INNER_HEIGHT);
		static Rect dragRect = new Rect(0f, 0f, WIDTH, 20f);
		static Vector2 scroll = new Vector2();

		public static void OnGUI()
		{
			if (!Manager.Resources.IsInstance())
				return;

			if (entries == null)
			{
				entries = new Dictionary<string, ProfileEntry>();

				Load();
				AgentProfile();
				LocomotionProfile();
				PlayerProfile();
				StatusProfile();
			}

			if (!toggle)
				return;

			rect = Window.Draw(WindowID.Value, rect, Draw, "Profile Inspector");
		}

		public static void Draw(int id)
		{
			GUI.DragWindow(dragRect);
			GUILayout.BeginArea(innerRect);
			{
				GUILayout.BeginVertical();
				{
					scroll = GUILayout.BeginScrollView(scroll);
					{
						foreach (KeyValuePair<string, ProfileEntry> entry in entries)
							entry.Value.Draw();
					}
					GUILayout.EndScrollView();
				}
				GUILayout.EndVertical();
			}
			GUILayout.EndArea();
		}
	}
}
