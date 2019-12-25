using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CareBairPackage
{
	public static partial class PanningHelper
	{
		static bool drag = false;
		static bool canDrag = false;
		static CursorPoint lockPoint;

		public static bool MouseDown =>
			(LMB.Value && Input.GetMouseButtonDown(0)) ||
			(RMB.Value && Input.GetMouseButtonDown(1)) ||
			(MMB.Value && Input.GetMouseButtonDown(2));

		public static bool MouseHeld =>
			(LMB.Value && Input.GetMouseButton(0)) ||
			(RMB.Value && Input.GetMouseButton(1)) ||
			(MMB.Value && Input.GetMouseButton(2));

		public static void LateUpdate()
		{
			if (!Manager.Game.IsInstance() ||
				Manager.Game.Instance.WorldData == null ||
				GUIUtility.hotControl != 0 ||
				EventSystem.current.IsPointerOverGameObject() ||
				Time.timeScale == 0 ||
				Cursor.lockState == CursorLockMode.Locked)
				return;

			if (drag)
			{
				if (!MouseHeld)
				{
					drag = false;
					Cursor.visible = true;
					Cursor.lockState = CursorLockMode.None;
					return;
				}

				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Confined;

				SetCursorPos(lockPoint.x, lockPoint.y);
			}
			else if (Input.anyKey)
			{
				if (!canDrag || !MouseDown)
				{
					canDrag = false;
					return;
				}

				drag = true;

				GetCursorPos(out lockPoint);
			}
			else
				canDrag = true;
		}

		public static float Magnitude(CursorPoint a, CursorPoint b)
		{
			return Mathf.Pow(Mathf.Pow(a.x - b.x, 2) + Mathf.Pow(a.y - b.y, 2), 0.5f);
		}

		[DllImport("user32.dll")]
		public static extern bool SetCursorPos(int x, int y);

		[DllImport("user32.dll")]
		public static extern bool GetCursorPos(out CursorPoint pos);
	}
}
