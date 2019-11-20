using System.Collections.Generic;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class AltDialogMenu
	{
		public const float HOLD_DELAY = 0.4f;

		public const float ITEM_HEIGHT = 25f; // 29f
		public const float MARGIN_LEFT = 10f;
		public const float MARGIN_RIGHT = 10f;
		public const float MARGIN_TOP = 20f;
		public const float MARGIN_BOTTOM = 10f;
		public const float SPACE_LEFT = 20f;
		public const float DESCRIPTION_HEIGHT = 60f;
		internal static float itemWidth = 0f;
		internal static float sliderWidth = 0f;
		internal static float width;
		internal static float height;
		internal static float innerWidth;
		internal static float innerHeight;

		public static GUIStyle descriptionBoxStyle;
		public static GUIStyle selectedButtonStyle;

		private static Rect rect = new Rect();
		private static readonly List<ADMSheet> sheets = new List<ADMSheet>();
		private static ADMSheet currSheet;
		private static int maxLength = 0;
		private static int selected = 0;
		private static bool visible = true;
		private static float holdDelay = 0f;
		private static bool hold = false;

		public static void Update()
		{
			if (!Enabled.Value || sheets.Count == 0)
				return;

			for (int i = 0; i < sheets.Count; i++)
			{
				ADMSheet sheet = sheets[i];

				if (sheet.condition != null && !sheet.condition())
				{
					sheets.Remove(sheet);

					if (currSheet == sheet)
					{
						currSheet = null;
						selected = 0;
					}

					i--;
				}
			}

			if (ToggleKey.Value.IsDown())
			{
				if (visible && currSheet != null && sheets.Count > 1)
				{
					currSheet = null;
					selected = 0;
				}
				else
				{
					visible = !visible;
					rect.x = visible ? SPACE_LEFT : MARGIN_LEFT - width;
				}
			}
			else if (ScrollDownKey.Value.IsPressed())
			{
				if (!KeyHold())
					return;

				selected = Mathf.Min(selected + 1, currSheet.sheets.Count - 1);
			}
			else if (ScrollUpKey.Value.IsPressed())
			{
				if (!KeyHold())
					return;

				selected = Mathf.Max(selected - 1, 0);
			}
			else if (IncreaseKey.Value.IsPressed())
			{
				if (!KeyHold())
					return;

				if (currSheet != null)
					currSheet.sheets[selected].Invoke(1);
				else
				{
					currSheet = sheets[selected];
					selected = 0;
				}
			}
			else if (DecreaseKey.Value.IsPressed())
			{
				if (!KeyHold())
					return;

				if (currSheet != null)
					currSheet.sheets[selected].Invoke(-1);
				else
				{
					currSheet = sheets[selected];
					selected = 0;
				}
			}
			else
			{
				holdDelay = HOLD_DELAY;
				hold = false;
			}
		}

		public static void OnGUI()
		{
			if (!Enabled.Value || sheets.Count == 0)
				return;

			if (descriptionBoxStyle == null)
				descriptionBoxStyle = new GUIStyle(GUI.skin.box)
				{
					wordWrap = true,
					fontSize = 10
				};

			if (selectedButtonStyle == null)
				selectedButtonStyle = new GUIStyle(GUI.skin.button)
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

			rect = GUI.Window(WindowID.Value, rect, Draw, currSheet?.label ?? "");
		}

		public static bool Draw_Back()
		{
			if (sheets.Count <= 1 || currSheet == null)
				return false;

			if (GUILayout.Button($"[{ToggleKey.Value.MainKey.ToString()}] Back", selectedButtonStyle))
			{
				currSheet = null;
				selected = 0;
			}

			return true;
		}

		public static bool Draw_More()
		{
			return GUILayout.Button("...", selectedButtonStyle);
		}

		public static void Draw(int id)
		{
			GUILayout.BeginArea(new Rect(MARGIN_LEFT, MARGIN_TOP, innerWidth, innerHeight));
			{
				GUILayout.BeginHorizontal();
				{
					List<ADMSheet> list = currSheet?.sheets ?? sheets;

					GUILayout.BeginVertical(GUILayout.Width(itemWidth));
					{
						int trueMaxLength = list.Count;
						int maxLengthHalf = maxLength / 2;
						int maxLengthDiff = trueMaxLength - maxLength;
						int offset = 0;

						if (maxLength != list.Count && selected > maxLengthHalf)
							offset = Mathf.Min(maxLengthDiff, selected - maxLengthHalf);

						if (offset > 0)
						{
							if (Draw_More())
								selected = offset;
						}
						else if (!Draw_Back())
							GUILayout.Label("");

						for (int i = offset; i < maxLength + offset; i++)
						{
							ADMSheet sheet = list[i];

							if (sheet.Draw(i == selected, out int flag))
							{
								selected = i;

								if (flag != 0)
									sheet.Invoke(flag);
							}
						}

						if (maxLength + offset != trueMaxLength)
						{
							if (Draw_More())
								selected = maxLength + offset - 1;
						}
						else if (!Draw_Back())
							GUILayout.Label("");

						GUILayout.Label(
							list[selected].description ?? "",
							descriptionBoxStyle,
							GUILayout.Width(itemWidth),
							GUILayout.Height(DESCRIPTION_HEIGHT)
						);
					}
					GUILayout.EndVertical();
				}
				GUILayout.EndHorizontal();
			}
			GUILayout.EndArea();
		}

		public static bool KeyHold()
		{
			if (!hold)
				return hold = true;
			else
			{
				if (holdDelay > 0)
					holdDelay -= Time.unscaledDeltaTime;

				return holdDelay <= 0f;
			}
		}

		public static void RefreshWindow()
		{
			float extraHeight = MARGIN_TOP + MARGIN_BOTTOM + DESCRIPTION_HEIGHT + 3f;
			float heightLimit = Mathf.Min(Screen.height, WinHeight.Value);

			maxLength = Mathf.Max(4, (int)Mathf.Floor(
				(heightLimit - extraHeight) / ITEM_HEIGHT
			));

			if (currSheet != null)
				maxLength = Mathf.Min(currSheet.sheets.Count + 2, maxLength);

			itemWidth = WinWidth.Value;
			sliderWidth = itemWidth * 0.6f;
			width = itemWidth + MARGIN_LEFT + MARGIN_RIGHT;
			height = maxLength * ITEM_HEIGHT + extraHeight;
			innerWidth = width - MARGIN_LEFT - MARGIN_RIGHT;
			innerHeight = height - MARGIN_TOP - MARGIN_BOTTOM;
			maxLength -= 2;
			rect = new Rect(
				visible ? SPACE_LEFT : MARGIN_LEFT - width,
				(Screen.height - height) / 2,
				width,
				height
			);
		}

		public static bool IsSheet(ADMSheet sheet)
		{
			return currSheet == sheet;
		}

		public static void AddSheet(ADMSheet sheet, bool selectThis = true)
		{
			if (sheet == null || sheets.Contains(sheet))
				return;

			sheets.Add(sheet);

			if (selectThis)
				currSheet = sheet;

			selected = 0;
			visible = ShowImmediately.Value;

			RefreshWindow();
		}

		public delegate bool Condition();
	}
}
