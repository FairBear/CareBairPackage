using System;
using UnityEngine;

namespace CareBairPackage
{
	public static partial class ProfileInspector
	{
		const float LABEL_WIDTH = 240f;
		const float RESET_WIDTH = 50f;

		public class ProfileEntry
		{
			public readonly object target;
			public readonly string label;
			public readonly string property;
			public readonly object defaultValue;
			public readonly Type type;
			readonly Action act;
			public object value = null;
			string temp;

			public ProfileEntry(object target, string label, string property, string key, object defaultValue, Type type, Action act)
			{
				this.target = target;
				this.label = label;
				this.property = property;
				this.type = type;
				this.act = act;
				this.defaultValue = defaultValue;

				if (save.TryGetValue(key, out temp))
				{
					Parse(false);
				}
				else
					temp = (value = defaultValue).ToString();
			}

			public void Draw()
			{
				string prev = temp;

				GUILayout.BeginHorizontal();
				{
					GUILayout.Label(label, GUILayout.Width(LABEL_WIDTH));

					temp = GUILayout.TextField(temp);

					if (GUILayout.Button("Reset", GUILayout.Width(RESET_WIDTH)))
						temp = (value = defaultValue).ToString();
				}
				GUILayout.EndHorizontal();

				if (prev != temp)
					Parse();
			}

			public void Invoke()
			{
				if (act != null)
					act();
				else
					SetValue(target, property, value);
			}

			void Parse(bool invokeAndSave = true)
			{
				bool flag = false;
				object next = null;

				if (type == typeof(float))
				{
					if (flag = float.TryParse(temp, out float result))
						next = result;
				}
				else if (type == typeof(int))
				{
					if (flag = int.TryParse(temp, out int result))
						next = result;
				}

				if (flag && next != value)
				{
					value = next;

					if (invokeAndSave)
					{
						Invoke();
						Save();
					}
				}
			}
		}
	}
}
