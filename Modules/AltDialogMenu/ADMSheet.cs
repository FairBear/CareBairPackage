using System;
using System.Collections.Generic;
using UnityEngine;

namespace CareBairPackage
{
	public enum ADMSheetType
	{
		Button,
		Slider
	}

	public class ADMSheet
	{
		public string label;
		public object value;
		public object callback;
		public AltDialogMenu.Condition condition;
		public string description;
		public readonly ADMSheetType type;
		public List<ADMSheet> sheets;

		public void SetSheets(params ADMSheet[] sheets)
		{
			this.sheets = new List<ADMSheet>();

			if (sheets != null)
				foreach (ADMSheet sheet in sheets)
					this.sheets.Add(sheet);
		}

		public void SetSheets(List<ADMSheet> sheets)
		{
			this.sheets = sheets ?? new List<ADMSheet>();
		}

		// Button
		public ADMSheet(string label,
						Action callback = null,
						AltDialogMenu.Condition condition = null,
						string description = null,
						params ADMSheet[] sheets)
		{
			this.label = label;
			this.callback = callback;
			this.condition = condition;
			this.description = description;
			type = ADMSheetType.Button;

			SetSheets(sheets);
		}

		// Slider
		public ADMSheet(string label,
						int value,
						int min,
						int max,
						Action<int, ADMSheetSlider> callback = null,
						AltDialogMenu.Condition condition = null,
						string description = null,
						params ADMSheet[] sheets)
		{
			this.label = label;
			this.value = new ADMSheetSlider(value, min, max);
			this.callback = callback;
			this.condition = condition;
			this.description = description;
			type = ADMSheetType.Slider;

			SetSheets(sheets);
		}

		public void Invoke(int flag = 0)
		{
			switch (type)
			{
				case ADMSheetType.Button:
					if (callback != null)
						(callback as Action)();

					break;

				case ADMSheetType.Slider:
					ADMSheetSlider slider = value as ADMSheetSlider;

					if (callback != null)
						(callback as Action<int, ADMSheetSlider>)(flag, slider);
					else
						slider.value = Mathf.Clamp(slider.value + flag, slider.min, slider.max);

					break;
			}
		}

		public bool Draw(bool selected, out int next)
		{
			next = 0;

			switch (type)
			{
				case ADMSheetType.Slider:
					ADMSheetSlider slider = value as ADMSheetSlider;
					bool button;
					int result;

					GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
					{
						button = GUILayout.Button(
							label,
							selected ?
								AltDialogMenu.selectedButtonStyle :
								GUI.skin.button,
							GUILayout.Width(AltDialogMenu.sliderWidth)
						);

						result = (int)GUILayout.HorizontalSlider(
							slider.value,
							slider.min,
							slider.max,
							GUILayout.ExpandWidth(true)
						);
					}
					GUILayout.EndHorizontal();

					if (button || slider.value != result)
					{
						next = result - slider.value;

						return true;
					}

					break;

				default:
					return GUILayout.Button(
						label,
						selected ?
							AltDialogMenu.selectedButtonStyle :
							GUI.skin.button,
						GUILayout.Width(AltDialogMenu.itemWidth)
					);
			}

			return false;
		}
	}
}
