using UnityEngine;

namespace CareBairPackage
{
	public struct CursorPoint
	{
		public static implicit operator Vector2(CursorPoint point)
		{
			return new Vector2(point.x, point.y);
		}

		public int x;
		public int y;
	}
}
