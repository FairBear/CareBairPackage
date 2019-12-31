using UnityEngine;

namespace CareBairPackage
{
	static class Window
	{
		public static Rect Draw(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			clientRect = GUI.Window(id, clientRect, func, text);

			return new Rect(
					Mathf.Clamp(clientRect.x, 0f, Screen.width - clientRect.width),
					Mathf.Clamp(clientRect.y, 0f, Screen.height - clientRect.height),
					clientRect.width,
					clientRect.height
				);
		}
	}
}
