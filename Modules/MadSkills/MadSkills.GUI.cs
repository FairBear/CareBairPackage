using AIChara;
using AIProject;
using AIProject.SaveData;
using AIProject.UI;
using HarmonyLib;
using Manager;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using XUnity.AutoTranslator.Plugin.Core;

namespace CareBairPackage
{
	public static partial class MadSkills
	{
		const float MARGIN_TOP = 20f;
		const float MARGIN_BOTTOM = 10f;
		const float MARGIN_LEFT = 10f;
		const float MARGIN_RIGHT = 10f;
		const float WIDTH = 480f;
		const float HEIGHT = 320f;
		const float INNER_WIDTH = WIDTH - MARGIN_LEFT - MARGIN_RIGHT;
		const float INNER_HEIGHT = HEIGHT - MARGIN_TOP - MARGIN_BOTTOM;

		static Rect rect = new Rect(Screen.width - WIDTH, (Screen.height - HEIGHT) / 2, WIDTH, HEIGHT);
		static Rect innerRect = new Rect(MARGIN_LEFT, MARGIN_TOP, INNER_WIDTH, INNER_HEIGHT);
		static Rect dragRect = new Rect(0f, 0f, WIDTH, 20f);

		static Vector2 scroll0 = new Vector2();
		static Vector2 scroll1 = new Vector2();

		static GUIStyle headerStyle = null;
		static GUIStyle addStyle = null;
		static GUIStyle removeStyle = null;

		static IntReactiveProperty selected;
		static AgentActor agent;

		public static void OnGUI()
		{
			if (!MapUIContainer.IsInstance())
				return;

			StatusUI ui = MapUIContainer.SystemMenuUI.StatusUI;

			if (!ui || !ui.EnabledInput)
			{
				if (selected != null)
					selected = null;

				return;
			}

			if (selected == null)
				selected = Traverse.Create(ui).Field("_selectedID").GetValue<IntReactiveProperty>();

			if (selected == null || selected.Value == 0)
				return;

			agent = Map.Instance.AgentTable[selected.Value - 1];

			if (agent.ChaControl.fileGameInfo.phase < 2)
				return;

			if (headerStyle == null)
			{
				headerStyle = new GUIStyle(GUI.skin.label)
				{
					fontStyle = FontStyle.Bold
				};

				addStyle = new GUIStyle(GUI.skin.button)
				{
					fontStyle = FontStyle.Bold,
					normal = {
						textColor = Color.green
					},
					focused = {
						textColor = Color.green
					},
					active = {
						textColor = Color.green
					},
					hover = {
						textColor = Color.green
					},
				};

				removeStyle = new GUIStyle(GUI.skin.button)
				{
					fontStyle = FontStyle.Bold,
					normal = {
						textColor = Color.red
					},
					focused = {
						textColor = Color.red
					},
					active = {
						textColor = Color.red
					},
					hover = {
						textColor = Color.red
					},
				};
			}

			rect = Window.Draw(WindowID.Value, rect, Draw, "Mad Skills");
		}

		static void Draw(int id)
		{
			GUI.DragWindow(dragRect);

			GUILayout.BeginArea(innerRect);
			{
				GUILayout.BeginHorizontal();
				{
					ChaFileGameInfo info = agent.ChaControl.fileGameInfo;

					if (info.phase >= 2)
						Draw_Agent(info);
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndArea();
		}

		static void Draw_Agent(ChaFileGameInfo info)
		{
			scroll0 = Draw_Agent_List(scroll0, "Normal Skills", 16, info.normalSkill);
			scroll1 = Draw_Agent_List(scroll1, "H Skills", 17, info.hSkill);
		}

		static Vector2 Draw_Agent_List(Vector2 scroll,
									   string label,
									   int category,
									   Dictionary<int, int> list)
		{
			GUILayout.BeginVertical();
			{
				GUILayout.Label(label, headerStyle);

				scroll = GUILayout.BeginScrollView(scroll);
				{
					List<StuffItem> items = Map.Instance.Player.PlayerData.ItemList;
					Dictionary<int, StuffItemInfo>.ValueCollection skills =
						Manager.Resources.Instance.GameInfo.GetItemTable(category).Values;

					foreach (StuffItemInfo skill in skills)
						Draw_List_Item(skill, list, items, category);
				}
				GUILayout.EndScrollView();
			}
			GUILayout.EndVertical();

			return scroll;
		}

		static void Draw_List_Item(StuffItemInfo skill,
								   Dictionary<int, int> list,
								   List<StuffItem> items,
								   int category)
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label($"[{skill.ID}] {skill.Name.Translate()}");

				if (list.ContainsValue(skill.ID))
				{
					if (GUILayout.Button("-", removeStyle, GUILayout.Width(24f)))
					{
						int key = list.FirstOrDefault(v => v.Value == skill.ID).Key;

						if (key < 5)
							list[key] = -1;
						else
							list.Remove(key);

						if (ConsumeItem.Value)
						{
							StuffItem item = items.Find(v =>
								v.CategoryID == category &&
								v.ID == skill.ID
							);

							if (item == null)
							{
								item = new StuffItem(skill.CategoryID, skill.ID, 0);
								items.Add(item);
							}

							item.Count++;
						}
					}
				}
				else
				{
					StuffItem item = items.Find(v =>
						v.CategoryID == category &&
						v.ID == skill.ID
					);

					if (item != null)
						if (GUILayout.Button("+", addStyle, GUILayout.Width(24f)))
						{
							if (list.ContainsValue(-1))
							{
								int key = list.FirstOrDefault(v => v.Value == -1).Key;
								list[key] = skill.ID;
							}
							else
								list[list.Count] = skill.ID;

							if (ConsumeItem.Value)
							{
								item.Count--;

								if (item.Count <= 0)
									items.Remove(item);
							}
						}
				}
			}
			GUILayout.EndHorizontal();
		}
	}
}
